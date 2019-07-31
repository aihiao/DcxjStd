using System;
using System.Collections.Generic;
using System.Text;

namespace TiXml
{
	/// <summary>
	/// TiXmlBase is a base class for every class in TinyXml.
	///	It does little except to establish that TinyXml classes	can be printed and provide some utility functions.
	///	In XML, the document and elements can contain other elements and other types of nodes.
	///	A Document can contain:	Element	(container or leaf)
	///							Comment (leaf)
	///							Unknown (leaf)
	///							Declaration( leaf )

	///	An Element can contain:	Element (container or leaf)
	///							Text	(leaf)
	///							Attributes (not on tree)
	///							Comment (leaf)
	///							Unknown (leaf)

	///	A Declaration contains: Attributes (not on tree)
	/// </summary>
	public abstract class TiXmlBase
	{
		public const int INVALID_STRING_INDEX = -1;

		public enum ErrorType
		{
			TIXML_NO_ERROR = 0,
			TIXML_ERROR,
			TIXML_ERROR_OPENING_FILE,
			TIXML_ERROR_OUT_OF_MEMORY,
			TIXML_ERROR_PARSING_ELEMENT,
			TIXML_ERROR_FAILED_TO_READ_ELEMENT_NAME,
			TIXML_ERROR_READING_ELEMENT_VALUE,
			TIXML_ERROR_READING_ATTRIBUTES,
			TIXML_ERROR_PARSING_EMPTY,
			TIXML_ERROR_READING_END_TAG,
			TIXML_ERROR_PARSING_UNKNOWN,
			TIXML_ERROR_PARSING_COMMENT,
			TIXML_ERROR_PARSING_DECLARATION,
			TIXML_ERROR_DOCUMENT_EMPTY,
			TIXML_ERROR_EMBEDDED_NULL,
			TIXML_ERROR_PARSING_CDATA,
			TIXML_ERROR_DOCUMENT_TOP_ONLY,

			TIXML_ERROR_STRING_COUNT
		};

		protected static string[] errorString = new string[]
	{
		"No error",
		"Error",
		"Failed to open file",
		"Memory allocation failed.",
		"Error parsing Element.",
		"Failed to read Element name",
		"Error reading Element value.",
		"Error reading Attributes.",
		"Error: empty tag.",
		"Error reading end tag.",
		"Error parsing Unknown.",
		"Error parsing Comment.",
		"Error parsing Declaration.",
		"Error document empty.",
		"Error null (0) or unexpected EOF found in input stream.",
		"Error parsing CDATA.",
		"Error when TiXmlDocument added to document, because TiXmlDocument can only be at the root.",
	};

		protected TiXmlCursor location = new TiXmlCursor();

		/// <summary>
		/// Field containing a generic user pointer
		/// </summary>
		protected object userData = null;

		private struct Entity
		{
			public Entity(string str, char chr) { this.str = str; this.chr = chr; }

			public string str;
			public char chr;
		};

		private static Entity[] entity = new Entity[]
	{
		new Entity( "&amp;",  '&' ),
		new Entity( "&lt;",   '<' ),
		new Entity( "&gt;",   '>' ),
		new Entity( "&quot;", '\"' ),
		new Entity( "&apos;", '\'' )
	};

		private static bool condenseWhiteSpace = true;

		/// <summary>
		/// All TinyXml classes can print themselves to a filestream or the string class (TiXmlString in non-STL mode, PG_string in STL mode.) 
		/// Either or both cfile and str can be null. This is a formatted print, and will insert tabs and newlines.
		/// (For an unformatted stream, use the << operator.)
		/// </summary>
		public abstract void Print(StringBuilder cfile, int depth);

		/// <summary>
		///	The world does not agree on whether white space should be kept or
		///	not. In order to make everyone happy, these global, static functions
		///	are provided to set whether or not TinyXml will condense all white space
		///	into a single space or not. The default is to condense. Note changing this
		///	value is not thread safe.
		/// </summary>
		public static void SetCondenseWhiteSpace(bool condense) { condenseWhiteSpace = condense; }

		/// <summary>
		/// Return the current white space setting.
		/// </summary>
		public static bool IsWhiteSpaceCondensed() { return condenseWhiteSpace; }

		/// <summary>
		/// Return the position, in the original source file, of this node or attribute.
		/// The row and column are 1-based. (That is the first row and first column is
		/// 1,1). If the returns values are 0 or less, then the parser does not have
		/// a row and column value.
		/// 
		/// Generally, the row and column value will be set when the TiXmlDocument::Load(),
		/// TiXmlDocument::LoadFile(), or any TiXmlNode::Parse() is called. It will NOT be set
		/// when the DOM was created from operator>>.
		/// 
		/// The values reflect the initial load. Once the DOM is modified programmatically
		/// (by adding or changing nodes and attributes) the new values will NOT update to
		/// reflect changes in the document.
		/// 
		/// There is a minor performance cost to computing the row and column. Computation
		/// can be disabled if TiXmlDocument::SetTabSize() is called with 0 as the value.
		/// </summary>
		/// <see cref="TiXmlDocument::SetTabSize()"/>
		/// 
		public int Row() { return location.row + 1; }
		public int Column() { return location.col + 1; }

		/// <summary>
		/// Set a pointer to arbitrary user data.
		/// </summary>
		public void SetUserData(object user) { userData = user; }

		/// <summary>
		/// Get a pointer to arbitrary user data.
		/// </summary>
		public object GetUserData() { return userData; }

#if UNUSED
	// Table that returns, for a given lead byte, the total number of bytes
	// in the UTF-8 sequence.
	static const int utf8ByteTable[256];
#endif

		public abstract int Parse(string p, int index, TiXmlParsingData data, int encoding /*= TIXML_ENCODING_UNKNOWN*/ );

		/// <summary>
		/// Expands entities in a string. Note this should not contain the tag's '<', '>', etc, or they will be transformed into entities!
		/// </summary>
		public static void EncodeString(string str, StringBuilder outString)
		{
			int i = 0;

			while (i < str.Length)
			{
				char c = str[i];

				if (c == '&'
					&& i < ((int)str.Length - 2)
					&& str[i + 1] == '#'
					&& str[i + 2] == 'x')
				{
					// Hexadecimal character reference.
					// Pass through unchanged.
					// &#xA9;	-- copyright symbol, for example.
					//
					// The -1 is a bug fix from Rob Laveaux. It keeps
					// an overflow from happening if there is no ';'.
					// There are actually 2 ways to exit this loop -
					// while fails (error case) and break (semicolon found).
					// However, there is no mechanism (currently) for
					// this function to return an error.
					while (i < str.Length)
					{
						//outString.append( str.c_str() + i, 1 );
						outString.Append(str[i]);
						++i;
						if (str[i] == ';')
							break;
					}
				}
				else if (c == '&')
				{
					//outString.append( entity[0].str, entity[0].strLength );
					outString.Append(entity[0].str);
					++i;
				}
				else if (c == '<')
				{
					//outString.append( entity[1].str, entity[1].strLength );
					outString.Append(entity[1].str);
					++i;
				}
				else if (c == '>')
				{
					//outString.append( entity[2].str, entity[2].strLength );
					outString.Append(entity[2].str);
					++i;
				}
				else if (c == '\"')
				{
					//outString.append( entity[3].str, entity[3].strLength );
					outString.Append(entity[3].str);
					++i;
				}
				else if (c == '\'')
				{
					//outString.append( entity[4].str, entity[4].strLength );
					outString.Append(entity[4].str);
					++i;
				}

				else if (c < 32)
				{
#if UNUSED
#if SUPPORT_CHAR_SMALLER_THAN_32
				// Easy pass at non-alpha/numeric/symbol
				// Below 32 is symbolic.
				char buf[ 32 ];
			
#if TIXML_SNPRINTF
				TIXML_SNPRINTF( buf, sizeof(buf), "&#x%02X;", (unsigned) ( c & 0xff ) );
#else
				sprintf( buf, "&#x%02X;", (unsigned) ( c & 0xff ) );
#endif		

				//*ME:	warning C4267: convert 'size_t' to 'int'
				//*ME:	Int-Cast to make compiler happy ...
				outString->append( buf, (int)strlen( buf ) );
				++i;
#else
					//asset
#endif
#endif
				}
				else
				{
					//char realc = (char) c;
					//outString.append( &realc, 1 );
					//*outString += (char) c;	// somewhat more efficient function call.
					outString.Append(c);
					++i;
				}
			}
		}

		protected static int SkipWhiteSpace(string p, int index, int encoding)
		{
			if (p == null || index < 0 || index >= p.Length)
			{
				return INVALID_STRING_INDEX;
			}
#if UNUSED
		if ( encoding == TIXML_ENCODING_UTF8 )
		{
			while ( *p )
			{
				const unsigned char* pU = (const unsigned char*)p;
			
				// Skip the stupid Microsoft UTF-8 Byte order marks
				if (	*(pU+0)==TIXML_UTF_LEAD_0
					 && *(pU+1)==TIXML_UTF_LEAD_1 
					 && *(pU+2)==TIXML_UTF_LEAD_2 )
				{
					p += 3;
					continue;
				}
				else if(*(pU+0)==TIXML_UTF_LEAD_0
					 && *(pU+1)==0xbfU
					 && *(pU+2)==0xbeU )
				{
					p += 3;
					continue;
				}
				else if(*(pU+0)==TIXML_UTF_LEAD_0
					 && *(pU+1)==0xbfU
					 && *(pU+2)==0xbfU )
				{
					p += 3;
					continue;
				}

				if ( IsWhiteSpace( *p ) || *p == '\n' || *p =='\r' )		// Still using old rules for white space.
					++p;
				else
					break;
			}
		}
		else
#endif
			{
				while (index < p.Length && IsWhiteSpace(p[index])/* || *p == '\n' || *p =='\r' */)
					++index;
			}

			return index;
		}

		protected static bool IsWhiteSpace(char c)
		{
			//return ( isspace( (unsigned char) c ) || c == '\n' || c == '\r' ); 
			return char.IsWhiteSpace(c) || c == '\n' || c == '\r';
		}

#if UNUSED
	static bool IsWhiteSpace( int c )
	{
		if ( c < 256 )
			return IsWhiteSpace( (char) c );
		return false;	// Again, only truly correct for English/Latin...but usually works.
	}
#endif

		/// <summary>
		/// Reads an XML name into the string provided. Returns	a pointer just past the last character of the name,	or 0 if the function has an error.
		/// </summary>
		protected static int ReadName(string p, int index, ref string name, int encoding)
		{
			// Oddly, not supported on some compilers,
			//name.clear();
			// So use this:
			name = "";
			//assert( p );

			// Names start with letters or underscores.
			// Of course, in unicode, tinyxml has no idea what a letter *is*. The
			// algorithm is generous.
			//
			// After that, they can be letters, underscores, numbers,
			// hyphens, or colons. (Colons are valid ony for namespaces,
			// but tinyxml can't tell namespaces from names.)
			if (index >= 0 && index < p.Length && (IsAlpha(p[index], encoding) || p[index] == '_'))
			{
				//const char* start = p;
				int start = index;
				while (index < p.Length && (IsAlphaNum(p[index], encoding) || p[index] == '_' || p[index] == '-' || p[index] == '.' || p[index] == ':'))
				{
					//(*name) += *p; // expensive
					++index;
				}
				if (index - start > 0)
				{
					//name.assign( start, p-start );
					name = p.Substring(start, index - start);
				}
				return index;
			}
			return INVALID_STRING_INDEX;
		}

		/// <summary>
		/// Reads text. Returns a pointer past the given end tag.
		/// Wickedly complex options, but it keeps the (sensitive) code in one place.
		/// </summary>
		/// <param name="p">where to start</param>
		/// <param name="index"></param>
		/// <param name="text">the string read</param>
		/// <param name="trimWhiteSpace">whether to keep the white space</param>
		/// <param name="endTag"> what ends this text</param>
		/// <param name="caseInsensitive">whether to ignore case in the end tag</param>
		/// <param name="encoding">the current encoding</param>
		protected static int ReadText(string p, int index, StringBuilder text, bool trimWhiteSpace, string endTag, bool caseInsensitive, int encoding)
		{
			text.Length = 0; // Unity版本的StringBuilder没有Clear函数
			if (!trimWhiteSpace			// certain tags always keep whitespace
				|| !condenseWhiteSpace)	// if true, whitespace is always kept
			{
				// Keep all the white space.
				while (index >= 0 && index < p.Length && !StringEqual(p, index, endTag, caseInsensitive, encoding))
				{
					//int len;
					//char cArr[4] = { 0, 0, 0, 0 };
					//p = GetChar( p, cArr, &len, encoding );
					//text->append( cArr, len );				
					char cArr = char.MinValue;
					index = GetChar(p, index, ref cArr, encoding);
					text.Append(cArr);
				}
			}
			else
			{
				bool whitespace = false;

				// Remove leading white space:
				index = SkipWhiteSpace(p, index, encoding);
				while (index >= 0 && index < p.Length && !StringEqual(p, index, endTag, caseInsensitive, encoding))
				{
					if (p[index] == '\r' || p[index] == '\n')
					{
						whitespace = true;
						++index;
					}
					else if (IsWhiteSpace(p[index]))
					{
						whitespace = true;
						++index;
					}
					else
					{
						// If we've found whitespace, add it before the
						// new character. Any whitespace just becomes a space.
						if (whitespace)
						{
							//(*text) += ' ';
							text.Append(' ');
							whitespace = false;
						}
						//int len;
						//char cArr[4] = { 0, 0, 0, 0 };
						//p = GetChar( p, cArr, &len, encoding );
						//if ( len == 1 )
						//	(*text) += cArr[0];	// more efficient
						//else
						//	text->append( cArr, len );
						char cArr = char.MinValue;
						index = GetChar(p, index, ref cArr, encoding);
						text.Append(cArr);
					}
				}
			}
			if (index >= 0)
				index += endTag.Length;
			return index;
		}

		/// <summary>
		/// If an entity has been found, transform it into a character.
		/// </summary>
		protected static int GetEntity(string p, int index, ref char value/*, int* length*/, int encoding)
		{
			// Presume an entity, and pull it out.
#if UNUSED
#if SUPPORT_CHAR_SMALLER_THAN_32
		//TIXML_STRING ent;
		//int i;
		//*length = 0;
		//if ( *(p+1) && *(p+1) == '#' && *(p+2) )
		if (index >= 0 && index + 1 < p.Length && p[index + 1] == '#' && index + 2 < p.Length)
		{
			long ucs = 0;
			int delta = 0;
			int mult = 1;

			if (p[index + 2] == 'x')
			{
				// Hexadecimal.
				//if ( !*(p+3) ) return 0;
				if (index + 3 >= p.Length) return INVALID_STRING_INDEX;

				int q = index + 3;
				//q = strchr( q, ';' );
				q = p.IndexOf(';', q);

				//if ( !q || !*q ) return 0;
				if (q == -1) return INVALID_STRING_INDEX;

				delta = q - index;
				--q;

				while (p[q] != 'x')
				{
					if (p[q] >= '0' && p[q] <= '9')
						ucs += mult * (p[q] - '0');
					else if (p[q] >= 'a' && p[q] <= 'f')
						ucs += mult * (p[q] - 'a' + 10);
					else if (p[q] >= 'A' && p[q] <= 'F')
						ucs += mult * (p[q] - 'A' + 10);
					else
						return INVALID_STRING_INDEX;
					mult *= 16;
					--q;
				}
			}
			else
			{
				// Decimal.
				//if ( !*(p+2) ) return 0;
				if (index + 2 >= p.Length) return INVALID_STRING_INDEX;

				int q = index + 2;
				q = p.IndexOf(';', q);

				if (q == -1) return INVALID_STRING_INDEX;

				delta = q - index;
				--q;

				while (p[q] != '#')
				{
					if (p[q] >= '0' && p[q] <= '9')
						ucs += mult * (p[q] - '0');
					else
						return 0;
					mult *= 10;
					--q;
				}
			}

			if ( encoding == TIXML_ENCODING_UTF8 )
			{
				// convert the UCS to UTF-8
				ConvertUTF32ToUTF8( ucs, value, length );
			}
			else
			{
				value = (char)ucs;
				//*length = 1;
			}
			return index + delta + 1;
		}
#endif
#endif
			// Now try to match it.
			for (int i = 0; i < entity.Length; ++i)
			{
				if (StringEqual(p, index, entity[i].str, false, TiXmlEncoding.TIXML_ENCODING_UNKNOWN))
				{
					//assert( strlen( entity[i].str ) == entity[i].strLength );
					value = entity[i].chr;
					//*length = 1;
					return index + entity[i].str.Length;
				}
			}

			// So it wasn't an entity, its unrecognized, or something like that.
			value = p[index];	// Don't put back the last one, since we return it!
			//*length = 1;	// Leave unrecognized entities - this doesn't really work.
			// Just writes strange XML.
			return index + 1;
		}


		/// <summary>
		/// Get a character, while interpreting entities.
		/// The length can be from 0 to 4 bytes.	/// </summary>
		protected static int GetChar(string p, int index, ref char _value/*, int* length*/, int encoding)
		{
#if UNUSED
		assert( p );
		if ( encoding == TIXML_ENCODING_UTF8 )
		{
			*length = utf8ByteTable[ *((const unsigned char*)p) ];
			assert( *length >= 0 && *length < 5 );
		}
		else

		{
			*length = 1;
		}

		if ( *length == 1 )
#endif
			{
				if (p[index] == '&')
					return GetEntity(p, index, ref _value, encoding);
				_value = p[index];
				return index + 1;
			}
#if UNUSED
		else if ( *length )
		{
			//strncpy( _value, p, *length );	// lots of compilers don't like this function (unsafe),
												// and the null terminator isn't needed
			for( int i=0; p[i] && i<*length; ++i ) {
				_value[i] = p[i];
			}
			return p + (*length);
		}
		else
		{
			// Not valid text.
			return 0;
		}
#endif
		}


		/// <summary>
		/// Return true if the next characters in the stream are any of the endTag sequences.
		/// Ignore case only works for english, and should only be relied on when comparing
		/// to English words: StringEqual( p, "version", true ) is fine.
		/// </summary>
		public static bool StringEqual(string a, int aStartIndex, string tag, bool ignoreCase, int encoding)
		{
			// 验证StartIndex的有效性
			if (a.Length - aStartIndex < tag.Length)
				return false;

			// 对比每个字符
			if (ignoreCase)
			{
				int i = 0;
				while (i < a.Length - aStartIndex && i < tag.Length && ToLower(a[i + aStartIndex]) == ToLower(tag[i]))
					i++;
				return i == tag.Length;
			}
			else
			{
				int i = 0;
				while (i < a.Length - aStartIndex && i < tag.Length && a[i + aStartIndex] == tag[i])
					i++;
				return i == tag.Length;
			}
		}

		private static char ToLower(char a)
		{
			return a < 128 ? char.ToLowerInvariant(a) : a;
		}

		/// <summary>
		/// None of these methods are reliable for any language except English.
		/// Good for approximation, not great for accuracy.
		/// </summary>
		protected static bool IsAlpha( /*unsigned */char anyByte, int encoding)
		{
			return char.IsLetter(anyByte);
		}

		protected static bool IsAlphaNum( /*unsigned */char anyByte, int encoding)
		{
			return char.IsLetterOrDigit(anyByte);
		}

#if UNUSED
	static int ToLower( int v, TiXmlEncoding encoding )
	{

		if ( encoding == TIXML_ENCODING_UTF8 )
		{
			if ( v < 128 ) return tolower( v );
			return v;
		}
		else

		{
			return tolower( v );
		}
	}

	static void ConvertUTF32ToUTF8( unsigned long input, char* output, int* length );
#endif
	};
}