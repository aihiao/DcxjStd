using System;
using System.Collections.Generic;
using System.Text;

namespace TiXml
{
	/// <summary>
	/// XML text. A text node can have 2 ways to output the next. "normal" output 
	/// and CDATA. It will default to the mode it was parsed from the XML file and
	/// you generally want to leave it alone, but you can change the output mode with 
	/// SetCDATA() and query it with CDATA().
	/// </summary>
	public class TiXmlText : TiXmlNode
	{
		private bool cdata;			// true if this should be input and output as a CDATA style text element

		/// <summary>
		/// Constructor for text element. By default, it is treated as 
		/// normal, encoded text. If you want it be output as a CDATA text
		/// element, set the parameter _cdata to 'true'
		/// </summary>
		public TiXmlText(string initValue)
			: base(TiXmlNode.NodeType.TEXT)
		{
			SetValue(initValue);
			cdata = false;
		}

		public TiXmlText(TiXmlText copy) : base(TiXmlNode.NodeType.TEXT) { copy.CopyTo(this); }

		/// <summary>
		/// Write this text object to a FILE stream.
		/// </summary>
		public override void Print(StringBuilder cfile, int depth)
		{
			//assert( cfile );
			if (cdata)
			{
				//int i;
				//fprintf(cfile, "\n");
				//for (i = 0; i < depth; i++)
				//{
				//	fprintf(cfile, "    ");
				//}
				//fprintf(cfile, "<![CDATA[%s]]>\n", value.c_str());	// unformatted output

				cfile.Append("\n");
				for (int i = 0; i < depth; i++)
				{
					cfile.Append("    ");
				}
				cfile.Append("<![CDATA["); cfile.Append(value); cfile.Append("]]>\n");
			}
			else
			{
				//string buffer;
				//EncodeString(value, &buffer);
				//fprintf(cfile, "%s", buffer.c_str());
				EncodeString(value, cfile);
			}
		}

		/// <summary>
		/// Queries whether this represents text using a CDATA section.
		/// </summary>
		public bool CDATA() { return cdata; }

		/// <summary>
		/// Turns on or off a CDATA representation of text.
		/// </summary>
		public void SetCDATA(bool _cdata) { cdata = _cdata; }

		public override int Parse(string p, int index, TiXmlParsingData data, int encoding)
		{
			value = "";
			TiXmlDocument document = GetDocument();

			if (data != null)
			{
				data.Stamp(p, index, encoding);
				location = data.Cursor();
			}

			const string startTag = "<![CDATA[";
			const string endTag = "]]>";

			if (cdata || StringEqual(p, index, startTag, false, encoding))
			{
				StringBuilder _value = new StringBuilder();

				cdata = true;

				if (!StringEqual(p, index, startTag, false, encoding))
				{
					document.SetError(ErrorType.TIXML_ERROR_PARSING_CDATA, p, index, data, encoding);
					return INVALID_STRING_INDEX;
				}
				index += startTag.Length;

				// Keep all the white space, ignore the encoding, etc.
				while (index >= 0 && index < p.Length && !StringEqual(p, index, endTag, false, encoding))
				{
					//value += p[index];
					_value.Append(p[index]);
					++index;
				}

				value = _value.ToString();

				StringBuilder dummy = _value;
				index = ReadText(p, index, dummy, false, endTag, false, encoding);
				return index;
			}
			else
			{
				StringBuilder _value = new StringBuilder();
				bool ignoreWhite = true;

				string end = "<";
				index = ReadText(p, index, _value, ignoreWhite, end, false, encoding);
				value = _value.ToString();
				if (index >= 0)
					return index - 1;	// don't truncate the '<'
				return INVALID_STRING_INDEX;
			}
		}

		/// <summary>
		/// Cast to a more defined type. Will return null not of the requested type.
		/// </summary>
		public override TiXmlText ToText() { return this; }

		/// <summary>
		/// Walk the XML tree visiting this node and all of its children. 
		/// </summary>
		public override bool Accept(TiXmlVisitor visitor)
		{
			return visitor.Visit(this);
		}

		/// <summary>
		/// [internal use] Creates a new Element and returns it.
		/// </summary>
		public override TiXmlNode Clone()
		{
			TiXmlText clone = null;
			clone = new TiXmlText("");

			if (clone == null)
				return null;

			CopyTo(clone);
			return clone;
		}

		protected void CopyTo(TiXmlText target)
		{
			base.CopyTo(target);
			target.cdata = cdata;
		}

		/// <summary>
		/// returns true if all white space and new lines
		/// </summary>
		/// <returns></returns>
		public bool Blank()
		{
			for (int i = 0; i < value.Length; i++)
				if (!IsWhiteSpace(value[i]))
					return false;
			return true;
		}
	};
}