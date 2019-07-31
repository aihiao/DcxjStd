using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TiXml
{
	/// <summary>
	/// Always the top level node. A document binds together all the XML pieces. It can be saved, loaded, and printed to the screen.
	///	The 'value' of a document node is the xml file name.
	/// </summary>
	public class TiXmlDocument : TiXmlNode
	{
		private bool error;
		private ErrorType errorId;
		private string errorDesc;
		private int tabsize;
		private TiXmlCursor errorLocation;
		private bool useMicrosoftBOM;		// the UTF-8 BOM were found when read. Note this, and try to write.

		/// <summary>
		/// Create an empty document, that has no name.
		/// </summary>
		public TiXmlDocument()
			: base(TiXmlNode.NodeType.DOCUMENT)
		{
			tabsize = 4;
			useMicrosoftBOM = false;
			ClearError();
		}

		/// <summary>
		/// Create a document with a name. The name of the document is also the filename of the xml.
		/// </summary>
		public TiXmlDocument(string documentName)
			: base(TiXmlNode.NodeType.DOCUMENT)
		{
			tabsize = 4;
			useMicrosoftBOM = false;
			value = documentName;
			ClearError();
		}

		public TiXmlDocument(TiXmlDocument copy)
			: base(TiXmlNode.NodeType.DOCUMENT)
		{
			copy.CopyTo(this);
		}

#if UNUSED
	/** Load a file using the current document value.
		Returns true if successful. Will delete any existing
		document data before loading.
	*/
	public bool LoadFile(TiXmlEncoding encoding /*= TiXmlEncoding.TIXML_ENCODING_UNKNOWN*/)
	{
		// See STL_STRING_BUG below.
		//StringToBuffer buf( value );

		return LoadFile(Value(), encoding);
	}
	/// Save a file using the current document value. Returns true if successful.
	public bool SaveFile()
	{
		// See STL_STRING_BUG below.
		//	StringToBuffer buf( value );
		//
		//	if ( buf.buffer && SaveFile( buf.buffer ) )
		//		return true;
		//
		//	return false;
		return SaveFile(Value());
	}

	/// Load a file using the given filename. Returns true if successful.
	public bool LoadFile(string filename, TiXmlEncoding encoding /*= TIXML_DEFAULT_ENCODING*/)
	{
		// There was a really terrifying little bug here. The code:
		//		value = filename
		// in the STL case, cause the assignment method of the PG_string to
		// be called. What is strange, is that the PG_string had the same
		// address as it's c_str() method, and so bad things happen. Looks
		// like a bug in the Microsoft STL implementation.
		// Add an extra string to avoid the crash.
		value = filename;

		// reading in binary mode so that tinyxml can normalize the EOL
		FILE file = TiXmlFOpen(value.c_str(), "rb");

		if (file != null)
		{
			bool result = LoadFile(file, encoding);
			fclose(file);
			return result;
		}
		else
		{
			SetError(ErrorType.TIXML_ERROR_OPENING_FILE, null, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
			return false;
		}
	}
	/// Save a file using the given filename. Returns true if successful.
	public bool SaveFile(string filename)
	{
		// The old c stuff lives on...
		FILE fp = TiXmlFOpen(filename, "w");
		if (fp != null)
		{
			bool result = SaveFile(fp);
			fclose(fp);
			return result;
		}
		return false;
	}
	/** Load a file using the given FILE*. Returns true if successful. Note that this method
		doesn't stream - the entire object pointed at by the FILE*
		will be interpreted as an XML file. TinyXML doesn't stream in XML from the current
		file location. Streaming may be added in the future.
	*/
	public bool LoadFile(FILE file, int encoding/* = TIXML_DEFAULT_ENCODING*/)
	{
		if (file == null)
		{
			SetError(ErrorType.TIXML_ERROR_OPENING_FILE, null, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
			return false;
		}

		// Delete the existing data:
		Clear();
		location.Clear();

		// Get the file size, so we can pre-allocate the string. HUGE speed impact.
		long length = 0;
		fseek(file, 0, SEEK_END);
		length = ftell(file);
		fseek(file, 0, SEEK_SET);

		// Strange case, but good to handle up front.
		if (length <= 0)
		{
			SetError(ErrorType.TIXML_ERROR_DOCUMENT_EMPTY, null, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
			return false;
		}

		// If we have a file, assume it is all one big XML file, and read it in.
		// The document parser may decide the document ends sooner than the entire file, however.
		string data;
		data.reserve(length);

		// Subtle bug here. TinyXml did use fgets. But from the XML spec:
		// 2.11 End-of-Line Handling
		// <snip>
		// <quote>
		// ...the XML processor MUST behave as if it normalized all line breaks in external 
		// parsed entities (including the document entity) on input, before parsing, by translating 
		// both the two-character sequence #xD #xA and any #xD that is not followed by #xA to 
		// a single #xA character.
		// </quote>
		//
		// It is not clear fgets does that, and certainly isn't clear it works cross platform. 
		// Generally, you expect fgets to translate from the convention of the OS to the c/unix
		// convention, and not work generally.

		/*
		while( fgets( buf, sizeof(buf), file ) )
		{
			data += buf;
		}
		*/

		char* buf = new char[length + 1];
		buf[0] = 0;

		if (fread(buf, length, 1, file) != 1)
		{
			delete[] buf;
			SetError(TIXML_ERROR_OPENING_FILE, 0, 0, TIXML_ENCODING_UNKNOWN);
			return false;
		}

		const char* lastPos = buf;
		const char* p = buf;

		buf[length] = 0;
		while (*p)
		{
			assert(p < (buf + length));
			if (*p == 0xa)
			{
				// Newline character. No special rules for this. Append all the characters
				// since the last string, and include the newline.
				data.append(lastPos, (p - lastPos + 1));	// append, include the newline
				++p;									// move past the newline
				lastPos = p;							// and point to the new buffer (may be 0)
				assert(p <= (buf + length));
			}
			else if (*p == 0xd)
			{
				// Carriage return. Append what we have so far, then
				// handle moving forward in the buffer.
				if ((p - lastPos) > 0)
				{
					data.append(lastPos, p - lastPos);	// do not add the CR
				}
				data += (char)0xa;						// a proper newline

				if (*(p + 1) == 0xa)
				{
					// Carriage return - new line sequence
					p += 2;
					lastPos = p;
					assert(p <= (buf + length));
				}
				else
				{
					// it was followed by something else...that is presumably characters again.
					++p;
					lastPos = p;
					assert(p <= (buf + length));
				}
			}
			else
			{
				++p;
			}
		}
		// Handle any left over characters.
		if (p - lastPos)
		{
			data.append(lastPos, p - lastPos);
		}
		delete[] buf;
		buf = 0;

		Parse(data.c_str(), 0, encoding);

		if (Error())
			return false;
		else
			return true;
	}
	/// Save a file using the given FILE*. Returns true if successful.
	public bool SaveFile(FILE fp)
	{
		if (useMicrosoftBOM)
		{
			const sbyte TIXML_UTF_LEAD_0 = 0xefU;
			const sbyte TIXML_UTF_LEAD_1 = 0xbbU;
			const sbyte TIXML_UTF_LEAD_2 = 0xbfU;

			fputc(TIXML_UTF_LEAD_0, fp);
			fputc(TIXML_UTF_LEAD_1, fp);
			fputc(TIXML_UTF_LEAD_2, fp);
		}
		Print(fp, 0);
		return (ferror(fp) == 0);
	}
#endif

		public bool Parse(string xmlString)
		{
			Parse(xmlString, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
			return Error() == false;
		}

		/// <summary>
		/// Parse the given null terminated block of xml data. Passing in an encoding to this
		/// method (either TIXML_ENCODING_LEGACY or TIXML_ENCODING_UTF8 will force TinyXml
		/// to use that encoding, regardless of what TinyXml might otherwise try to detect.
		/// </summary>
		public override int Parse(string p, int index, TiXmlParsingData prevData/* = null*/, int encoding/* = TIXML_DEFAULT_ENCODING*/)
		{
			ClearError();

			// Parse away, at the document level. Since a document
			// contains nothing but other tags, most of what happens
			// here is skipping white space.
			//if ( !p || !*p )
			if (p == null || index < 0 || index >= p.Length)
			{
				SetError(ErrorType.TIXML_ERROR_DOCUMENT_EMPTY, null, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
				return 0;
			}

			// Note that, for a document, this needs to come
			// before the while space skip, so that parsing
			// starts from the pointer we are given.
			location.Clear();
			if (prevData != null)
			{
				location.row = prevData.cursor.row;
				location.col = prevData.cursor.col;
			}
			else
			{
				location.row = 0;
				location.col = 0;
			}
			TiXmlParsingData data = new TiXmlParsingData(p, index, TabSize(), location.row, location.col);
			location = data.Cursor();
#if UNUSED
			if (encoding == TiXmlEncoding.TIXML_ENCODING_UNKNOWN)
			{
				// Check for the Microsoft UTF-8 lead bytes.
				const unsigned char* pU = (const unsigned char*)p;
				if (	*(pU+0) && *(pU+0) == TIXML_UTF_LEAD_0
					 && *(pU+1) && *(pU+1) == TIXML_UTF_LEAD_1
					 && *(pU+2) && *(pU+2) == TIXML_UTF_LEAD_2 )
				{
					encoding = TIXML_ENCODING_UTF8;
					useMicrosoftBOM = true;
				}
			}
#endif
			index = SkipWhiteSpace(p, index, encoding);
			//if ( !p )
			if (index < 0)
			{
				SetError(ErrorType.TIXML_ERROR_DOCUMENT_EMPTY, null, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
				return -1;
			}

			while (index >= 0 && index < p.Length)
			{
				TiXmlNode node = Identify(p, index, encoding);
				if (node != null)
				{
					index = node.Parse(p, index, data, encoding);
					LinkEndChild(node);
				}
				else
				{
					break;
				}

				// Did we get encoding info?
				if (encoding == TiXmlEncoding.TIXML_ENCODING_UNKNOWN && node.ToDeclaration() != null)
				{
					TiXmlDeclaration dec = node.ToDeclaration();
					string enc = dec.Encoding();
					//assert( enc );

					if (enc.Length == 0 /**enc == 0 */)
						encoding = TiXmlEncoding.TIXML_ENCODING_UTF8;
					else if (StringEqual(enc, 0, "UTF-8", true, TiXmlEncoding.TIXML_ENCODING_UNKNOWN))
						encoding = TiXmlEncoding.TIXML_ENCODING_UTF8;
					else if (StringEqual(enc, 0, "UTF8", true, TiXmlEncoding.TIXML_ENCODING_UNKNOWN))
						encoding = TiXmlEncoding.TIXML_ENCODING_UTF8;	// incorrect, but be nice
					else
						encoding = TiXmlEncoding.TIXML_ENCODING_LEGACY;
				}

				index = SkipWhiteSpace(p, index, encoding);
			}

			// Was this empty?
			if (firstChild == null)
			{
				SetError(ErrorType.TIXML_ERROR_DOCUMENT_EMPTY, null, 0, null, encoding);
				return INVALID_STRING_INDEX;
			}

			// All is well.
			return index;
		}


		/// <summary>
		/// Get the root element -- the only top level element -- of the document.
		/// In well formed XML, there should only be one. TinyXml is tolerant of
		/// multiple elements at the document level.
		/// </summary>
		public TiXmlElement RootElement() { return FirstChildElement(); }

		/// <summary>
		/// If an error occurs, Error will be set to true. Also,
		/// - The ErrorId() will contain the integer identifier of the error (not generally useful)
		/// - The ErrorDesc() method will return the name of the error. (very useful)
		/// - The ErrorRow() and ErrorCol() will return the location of the error (if known)
		/// </summary>
		public bool Error() { return error; }

		/// <summary>
		/// Contains a textual (english) description of the error if one occurs.
		/// </summary>
		public string ErrorDesc() { return errorDesc; }

		/// <summary>
		/// Generally, you probably want the error string ( ErrorDesc() ). But if you
		/// prefer the ErrorId, this function will fetch it.
		/// </summary>
		public ErrorType ErrorId() { return errorId; }

		/// <summary>
		/// Returns the location (if known) of the error. The first column is column 1, 
		/// and the first row is row 1. A value of 0 means the row and column wasn't applicable
		/// (memory errors, for example, have no row/column) or the parser lost the error. (An
		/// error in the error reporting, in that case.)
		/// </summary>
		/// <seealso cref="SetTabSize, Row, Column"/>
		public int ErrorRow() { return errorLocation.row + 1; }

		/// <summary>
		/// The column where the error occured. See ErrorRow()
		/// </summary>
		public int ErrorCol() { return errorLocation.col + 1; }

		/** SetTabSize() allows the error reporting functions (ErrorRow() and ErrorCol())
			to report the correct values for row and column. It does not change the output
			or input in any way.
		
			By calling this method, with a tab size
			greater than 0, the row and column of each node and attribute is stored
			when the file is loaded. Very useful for tracking the DOM back in to
			the source file.

			The tab size is required for calculating the location of nodes. If not
			set, the default of 4 is used. The tabsize is set per document. Setting
			the tabsize to 0 disables row/column tracking.

			Note that row and column tracking is not supported when using operator>>.

			The tab size needs to be enabled before the parse or load. Correct usage:
			@verbatim
			TiXmlDocument doc;
			doc.SetTabSize( 8 );
			doc.Load( "myfile.xml" );
			@endverbatim

			@sa Row, Column
		*/
		public void SetTabSize(int _tabsize) { tabsize = _tabsize; }

		public int TabSize() { return tabsize; }

		/// <summary>
		/// If you have handled the error, it can be reset with this call. The error
		/// state is automatically cleared if you Parse a new XML block.
		/// </summary>
		public void ClearError()
		{
			error = false;
			errorId = 0;
			errorDesc = "";
			errorLocation.row = errorLocation.col = 0;
			//errorLocation.last = 0; 
		}

		/// <summary>
		/// Print this Document to a FILE stream.
		/// </summary>
		public override void Print(StringBuilder cfile, int depth)
		{
			//assert( cfile );
			for (TiXmlNode node = FirstChild(); node != null; node = node.NextSibling())
			{
				node.Print(cfile, depth);
				//fprintf(cfile, "\n");
				cfile.Append("\n");
			}
		}

		// [internal use]
		public void SetError(ErrorType err, string pError, int index, TiXmlParsingData data, int encoding)
		{
			// The first error in a chain is more accurate - don't set again!
			if (error)
				return;

			//assert( err > 0 && err < TIXML_ERROR_STRING_COUNT );
			error = true;
			errorId = err;
			errorDesc = errorString[(int)errorId];

			errorLocation.Clear();
			if (pError != null && data != null)
			{
				data.Stamp(pError, index, encoding);
				errorLocation = data.Cursor();
			}
		}

		/// <summary>
		/// Cast to a more defined type. Will return null not of the requested type.
		/// </summary>
		public override TiXmlDocument ToDocument() { return this; }

		/// <summary>
		/// Walk the XML tree visiting this node and all of its children. 
		/// </summary>
		public override bool Accept(TiXmlVisitor visitor)
		{
			if (visitor.VisitEnter(this))
			{
				for (TiXmlNode node = FirstChild(); node != null; node = node.NextSibling())
				{
					if (!node.Accept(visitor))
						break;
				}
			}
			return visitor.VisitExit(this);
		}

		// [internal use]
		public override TiXmlNode Clone()
		{
			TiXmlDocument clone = new TiXmlDocument();
			if (clone == null)
				return null;

			CopyTo(clone);
			return clone;
		}

		private void CopyTo(TiXmlDocument target)
		{
			base.CopyTo(target);

			target.error = error;
			target.errorId = errorId;
			target.errorDesc = errorDesc;
			target.tabsize = tabsize;
			target.errorLocation = errorLocation.Clone();
			target.useMicrosoftBOM = useMicrosoftBOM;

			TiXmlNode node = null;
			for (node = firstChild; node != null; node = node.NextSibling())
			{
				target.LinkEndChild(node.Clone());
			}
		}
	};
}