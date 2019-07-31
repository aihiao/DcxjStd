using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TiXml
{
	/// <summary>
	/// Any tag that tinyXml doesn't recognize is saved as an
	/// unknown. It is a tag of text, but should not be modified.
	/// It will be written back to the XML, unchanged, when the file
	/// is saved.
	/// 
	/// DTD tags get thrown into TiXmlUnknowns.
	/// </summary>
	public class TiXmlUnknown : TiXmlNode
	{
		public TiXmlUnknown() : base(TiXmlNode.NodeType.UNKNOWN) { }

		public TiXmlUnknown(TiXmlUnknown copy) : base(TiXmlNode.NodeType.UNKNOWN) { copy.CopyTo(this); }

		/// <summary>
		/// Creates a copy of this Unknown and returns it.
		/// </summary>
		public override TiXmlNode Clone()
		{
			TiXmlUnknown clone = new TiXmlUnknown();

			if (clone == null)
				return null;

			CopyTo(clone);
			return clone;
		}

		/// <summary>
		/// Print this Unknown to a FILE stream.
		/// </summary>
		public override void Print(StringBuilder cfile, int depth)
		{
			//for (int i = 0; i < depth; i++)
			//	fprintf(cfile, "    ");
			//fprintf(cfile, "<%s>", value.c_str());
			for (int i = 0; i < depth; i++)
				cfile.Append("    ");
			cfile.Append("<"); cfile.Append(value); cfile.Append(">");
		}

		public override int Parse(string p, int index, TiXmlParsingData data, int encoding)
		{
			TiXmlDocument document = GetDocument();
			index = SkipWhiteSpace(p, index, encoding);

			if (data != null)
			{
				data.Stamp(p, index, encoding);
				location = data.Cursor();
			}
			//if ( !p || !*p || *p != '<' )
			if (index < 0 || index >= p.Length || p[index] != '<')
			{
				if (document != null)
					document.SetError(ErrorType.TIXML_ERROR_PARSING_UNKNOWN, p, index, data, encoding);
				return 0;
			}
			++index;
			value = "";
			StringBuilder _value = new StringBuilder();

			//while ( p && *p && *p != '>' )
			while (index >= 0 && index < p.Length && p[index] != '>')
			{
				//value += p[index];
				_value.Append(p[index]);
				++index;
			}

			if (index < 0)
			{
				if (document != null)
					document.SetError(ErrorType.TIXML_ERROR_PARSING_UNKNOWN, null, 0, null, encoding);
				return INVALID_STRING_INDEX;
			}
			value = _value.ToString();
			if (p[index] == '>')
				return index + 1;
			return index;
		}

		/// <summary>
		/// Cast to a more defined type. Will return null not of the requested type.
		/// </summary>
		public override TiXmlUnknown ToUnknown() { return this; }

		/// <summary>
		/// Walk the XML tree visiting this node and all of its children. 
		/// </summary>
		public override bool Accept(TiXmlVisitor visitor)
		{
			return visitor.Visit(this);
		}

		protected void CopyTo(TiXmlUnknown target)
		{
			base.CopyTo(target);
		}
	};
}