using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TiXml
{
	/** In correct XML the declaration is the first entry in the file.
		@verbatim
			<?xml version="1.0" standalone="yes"?>
		@endverbatim

		TinyXml will happily read or write files without a declaration,
		however. There are 3 possible attributes to the declaration:
		version, encoding, and standalone.

		Note: In this version of the code, the attributes are
		handled as special cases, not generic attributes, simply
		because there can only be at most 3 and they are always the same.
	*/
	public class TiXmlDeclaration : TiXmlNode
	{
		private string version = "";
		private string encoding = "";
		private string standalone = "";

		/// <summary>
		/// Construct an empty declaration.
		/// </summary>
		public TiXmlDeclaration() : base(TiXmlNode.NodeType.DECLARATION) { }

		/// <summary>
		/// Construct
		/// </summary>
		public TiXmlDeclaration(string _version, string _encoding, string _standalone)
			: base(TiXmlNode.NodeType.DECLARATION)
		{
			version = _version;
			encoding = _encoding;
			standalone = _standalone;
		}

		public TiXmlDeclaration(TiXmlDeclaration copy)
			: base(TiXmlNode.NodeType.DECLARATION)
		{
			copy.CopyTo(this);
		}

		/// <summary>
		/// Version. Will return an empty string if none was found.
		/// </summary>
		public string Version() { return version; }

		/// <summary>
		/// Encoding. Will return an empty string if none was found.
		/// </summary>
		public string Encoding() { return encoding; }

		/// <summary>
		/// Is this a standalone document?
		/// </summary>
		public string Standalone() { return standalone; }

		/// <summary>
		/// Creates a copy of this Declaration and returns it.
		/// </summary>
		public override TiXmlNode Clone()
		{
			TiXmlDeclaration clone = new TiXmlDeclaration();

			if (clone == null)
				return null;

			CopyTo(clone);
			return clone;
		}

		/// <summary>
		/// Print this declaration to a FILE stream.
		/// </summary>
		public override void Print(StringBuilder str, int depth/*, FILE cfile*/)
		{
			//if (cfile != null) fprintf(cfile, "<?xml ");
			if (str != null) str.Append("<?xml ");

			if (!string.IsNullOrEmpty(version))
			{
				//if (cfile != null) fprintf(cfile, "version=\"%s\" ", version.c_str());
				if (str != null) { str.Append("version=\""); str.Append(version); str.Append("\" "); }
			}
			if (!string.IsNullOrEmpty(encoding))
			{
				//if (cfile != null) fprintf(cfile, "encoding=\"%s\" ", encoding.c_str());
				if (str != null) { str.Append("encoding=\""); str.Append(encoding); str.Append("\" "); }
			}
			if (!string.IsNullOrEmpty(standalone))
			{
				//if (cfile != null) fprintf(cfile, "standalone=\"%s\" ", standalone.c_str());
				if (str != null) { str.Append("standalone=\""); str.Append(standalone); str.Append("\" "); }
			}
			//if (cfile != null) fprintf(cfile, "?>");
			if (str != null) str.Append("?>");
		}

		public override int Parse(string p, int index, TiXmlParsingData data, int _encoding)
		{
			index = SkipWhiteSpace(p, index, _encoding);
			// Find the beginning, find the end, and look for
			// the stuff in-between.
			TiXmlDocument document = GetDocument();
			if (index < 0 || index >= p.Length || !StringEqual( p, index, "<?xml", true, _encoding ))
			{
				if (document != null)
					document.SetError(ErrorType.TIXML_ERROR_PARSING_DECLARATION, null, 0, null, _encoding);
				return INVALID_STRING_INDEX;
			}
			if (data != null)
			{
				data.Stamp(p, index, _encoding);
				location = data.Cursor();
			}
			p += 5;

			version = "";
			encoding = "";
			standalone = "";

			while (index >= 0 && index < p.Length)
			{
				if (p[index] == '>')
				{
					++index;
					return index;
				}

				index = SkipWhiteSpace(p, index, _encoding);
				if (StringEqual(p, index, "version", true, _encoding))
				{
					TiXmlAttribute attrib = new TiXmlAttribute();
					index = attrib.Parse(p, index, data, _encoding);
					version = attrib.Value();
				}
				else if (StringEqual(p, index, "encoding", true, _encoding))
				{
					TiXmlAttribute attrib = new TiXmlAttribute();
					index = attrib.Parse(p, index, data, _encoding);
					encoding = attrib.Value();
				}
				else if (StringEqual(p, index, "standalone", true, _encoding))
				{
					TiXmlAttribute attrib = new TiXmlAttribute();
					index = attrib.Parse(p, index, data, _encoding);
					standalone = attrib.Value();
				}
				else
				{
					// Read over whatever it is.
					while (index >= 0 && index < p.Length && p[index] != '>' && !IsWhiteSpace(p[index]))
						++index;
				}
			}
			return INVALID_STRING_INDEX;
		}

		/// <summary>
		/// Cast to a more defined type. Will return null not of the requested type.
		/// </summary>
		public override TiXmlDeclaration ToDeclaration() { return this; }

		/// <summary>
		/// Walk the XML tree visiting this node and all of its children. 
		/// </summary>
		public override bool Accept(TiXmlVisitor visitor)
		{
			return visitor.Visit(this);
		}

		protected void CopyTo(TiXmlDeclaration target)
		{
			base.CopyTo(target);

			target.version = version;
			target.encoding = encoding;
			target.standalone = standalone;
		}
	};
}