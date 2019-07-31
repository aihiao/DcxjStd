using System;
using System.Collections.Generic;
using System.Text;

namespace TiXml
{

	/// <summary>
	/// An attribute is a name-value pair. Elements have an arbitrary number of attributes, each with a unique name.
	/// 
	/// The attributes are not TiXmlNodes, since they are not part of the tinyXML document object model. There are other suggested ways to look at this problem.
	/// </summary>
	public class TiXmlAttribute : TiXmlBase
	{
		public TiXmlAttribute prev = null;
		public TiXmlAttribute next = null;
		private TiXmlDocument document = null;	// A pointer back to a document, for error reporting.
		private string name = null;
		private string value = null;

		/// <summary>
		/// Construct an empty attribute.
		/// </summary>
		public TiXmlAttribute() { }

		/// <summary>
		/// Construct an attribute with a name and value.
		/// </summary>
		public TiXmlAttribute(string _name, string _value)
		{
			name = _name;
			value = _value;
		}

		/// <summary>
		/// Return the name of this attribute.
		/// </summary>
		public string Name() { return name; }

		/// <summary>
		/// < Return the value of this attribute.
		/// </summary>
		public string Value() { return value; }

		/// <summary>
		/// Return the value of this attribute, converted to an integer.
		/// </summary>
		public int IntValue()
		{
			int result = 0;
			int.TryParse(value, out result);
			return result;
		}

		/// <summary>
		/// Return the value of this attribute, converted to a double.
		/// </summary>
		public double DoubleValue()
		{
			double result = 0;
			double.TryParse(value, out result);
			return result;
		}

		/// <summary>
		/// QueryIntValue examines the value string. It is an alternative to the IntValue() method with richer error checking.
		/// If the value is an integer, it is stored in 'value' and the call returns TIXML_SUCCESS. If it is not an integer, it returns TIXML_WRONG_TYPE.
		/// 
		/// A specialized but useful call. Note that for success it returns 0, which is the opposite of almost all other TinyXml calls.
		/// </summary>
		public TiXmlQueryResult QueryIntValue(ref int _value)
		{
			//if (TIXML_SSCANF(value.c_str(), "%d", ival) == 1)
			if (int.TryParse(value, out _value))
				return TiXmlQueryResult.TIXML_SUCCESS;
			return TiXmlQueryResult.TIXML_WRONG_TYPE;
		}

		/// <summary>
		/// QueryDoubleValue examines the value string. See QueryIntValue().
		/// </summary>
		public TiXmlQueryResult QueryDoubleValue(ref double _value)
		{
			//if (TIXML_SSCANF(value.c_str(), "%lf", dval) == 1)
			if (double.TryParse(value, out _value))
				return TiXmlQueryResult.TIXML_SUCCESS;
			return TiXmlQueryResult.TIXML_WRONG_TYPE;
		}

		/// <summary>
		/// Set the name of this attribute.
		/// </summary>
		public void SetName(string _name) { name = _name; }

		/// <summary>
		/// Set the value.
		/// </summary>
		public void SetValue(string _value) { value = _value; }

		/// <summary>
		/// Set the value from an integer.
		/// </summary>
		public void SetIntValue(int _value)
		{
			//char buf [64];
			//#if TIXML_SNPRINTF		
			//	TIXML_SNPRINTF(buf, sizeof(buf), "%d", _value);
			//#else
			//	sprintf (buf, "%d", _value);
			//#endif
			//SetValue (buf);
			SetValue(_value.ToString());
		}

		/// <summary>
		/// Set the value from a double.
		/// </summary>
		public void SetDoubleValue(double _value)
		{
			//char buf [256];
			//#if TIXML_SNPRINTF		
			//	TIXML_SNPRINTF( buf, sizeof(buf), "%lf", _value);
			//#else
			//	sprintf (buf, "%lf", _value);
			//#endif
			//SetValue (buf);
			SetValue(_value.ToString());
		}

		/// <summary>
		/// Get the next sibling attribute in the DOM. Returns null at end.
		/// </summary>
		public TiXmlAttribute Next()
		{
			// We are using knowledge of the sentinel. The sentinel
			// have a value or name.
			if (string.IsNullOrEmpty(next.value) && string.IsNullOrEmpty(next.name))
				return null;
			return next;
		}

		/// <summary>
		/// Get the previous sibling attribute in the DOM. Returns null at beginning.
		/// </summary>
		public TiXmlAttribute Previous()
		{
			// We are using knowledge of the sentinel. The sentinel
			// have a value or name.
			if (string.IsNullOrEmpty(prev.value) && string.IsNullOrEmpty(prev.name))
				return null;
			return prev;
		}

		/// <summary>
		/// Attribute parsing starts: first letter of the name
		/// </summary>
		/// <returns>the next char after the value end quote</returns>
		public override int Parse(string p, int index, TiXmlParsingData data, int encoding)
		{
			index = SkipWhiteSpace(p, index, encoding);
			if (index < 0 || index >= p.Length) return -1;

			//	int tabsize = 4;
			//	if ( document )
			//		tabsize = document.TabSize();

			if (data != null)
			{
				data.Stamp(p, index, encoding);
				location = data.Cursor();
			}
			// Read the name, the '=' and the value.		
			int pErr = index;
			index = ReadName(p, index, ref name, encoding);
			//if ( !p || !*p )
			if (index < 0 || index >= p.Length)
			{
				if (document != null)
					document.SetError(ErrorType.TIXML_ERROR_READING_ATTRIBUTES, p, pErr, data, encoding);
				return 0;
			}
			index = SkipWhiteSpace(p, index, encoding);
			//if ( !p || !*p || *p != '=' )
			if (index < 0 || index >= p.Length || p[index] != '=')
			{
				if (document != null)
					document.SetError(ErrorType.TIXML_ERROR_READING_ATTRIBUTES, p, index, data, encoding);
				return 0;
			}

			++index;	// skip '='
			index = SkipWhiteSpace(p, index, encoding);
			if (index < 0 || index >= p.Length)
			{
				if (document != null)
					document.SetError(ErrorType.TIXML_ERROR_READING_ATTRIBUTES, p, index, data, encoding);
				return 0;
			}

			string end;
			const char SINGLE_QUOTE = '\'';
			const char DOUBLE_QUOTE = '\"';

			StringBuilder _value = new StringBuilder();

			if (p[index] == SINGLE_QUOTE)
			{
				++index;
				end = "\'";		// single quote in string
				index = ReadText(p, index, _value, false, end, false, encoding);
			}
			else if (p[index] == DOUBLE_QUOTE)
			{
				++index;
				end = "\"";		// double quote in string
				index = ReadText(p, index, _value, false, end, false, encoding);
			}
			else
			{
				// All attribute values should be in single or double quotes.
				// But this is such a common error that the parser will try
				// its best, even without them.
				value = "";
				while (index >= 0 && index < p.Length											// existence
					&& !IsWhiteSpace(p[index]) && p[index] != '\n' && p[index] != '\r'	// whitespace
					&& p[index] != '/' && p[index] != '>')							// tag end
				{
					if (p[index] == SINGLE_QUOTE || p[index] == DOUBLE_QUOTE)
					{
						// [ 1451649 ] Attribute values with trailing quotes not handled correctly
						// We did not have an opening quote but seem to have a 
						// closing one. Give up and throw an error.
						if (document != null)
							document.SetError(ErrorType.TIXML_ERROR_READING_ATTRIBUTES, p, index, data, encoding);
						return INVALID_STRING_INDEX;
					}
					//value += p[index];
					_value.Append(p[index]);
					++index;
				}
			}
			value = _value.ToString();
			return index;
		}

		/// <summary>
		/// Prints this Attribute to a FILE stream.
		/// </summary>
		public override void Print(StringBuilder str, int depth/*, FILE cfile*/)
		{
			StringBuilder n = new StringBuilder();
			StringBuilder v = new StringBuilder();

			EncodeString(name, n);
			EncodeString(value, v);

			if (value.IndexOf('\"') == -1)
			{
				//if (cfile)
				//{
				//	fprintf(cfile, "%s=\"%s\"", n.c_str(), v.c_str());
				//}
				if (str != null)
				{
					str.Append(n);
					str.Append("=\"");
					str.Append(v);
					str.Append("\"");
				}
			}
			else
			{
				//if (cfile)
				//{
				//	fprintf(cfile, "%s='%s'", n.c_str(), v.c_str());
				//}
				if (str != null)
				{
					str.Append(n);
					str.Append("='");
					str.Append(v);
					str.Append("'");
				}
			}
		}

		/// <summary>
		/// [internal use]
		/// Set the document pointer so the attribute can report errors.
		/// </summary>
		public void SetDocument(TiXmlDocument doc) { document = doc; }
	};
}