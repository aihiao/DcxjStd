using System;
using System.Collections.Generic;
using System.Text;

namespace TiXml
{
	/// <summary>
	/// An XML comment.
	/// </summary>
	public class TiXmlComment : TiXmlNode
	{
		/// <summary>
		/// Constructs an empty comment.
		/// </summary>
		public TiXmlComment() : base(TiXmlNode.NodeType.COMMENT) { }

		/// <summary>
		/// Construct a comment from text.
		/// </summary>
		public TiXmlComment(string _value) : base(TiXmlNode.NodeType.COMMENT) { SetValue(_value); }

		public TiXmlComment(TiXmlComment copy) : base(TiXmlNode.NodeType.COMMENT) { copy.CopyTo(this); }

		/// <summary>
		/// Returns a copy of this Comment.
		/// </summary>
		public override TiXmlNode Clone()
		{
			TiXmlComment clone = new TiXmlComment();

			if (clone == null)
				return null;

			CopyTo(clone);
			return clone;
		}

		/// <summary>
		/// Write this Comment to a FILE stream.
		/// </summary>
		public override void Print(StringBuilder cfile, int depth)
		{
			//assert(cfile);
			//for (int i = 0; i < depth; i++)
			//	fprintf(cfile, "    ");
			//fprintf(cfile, "<!--%s-.", value.c_str());
			for (int i = 0; i < depth; i++)
				cfile.Append("    ");
			cfile.Append("<!--");
			cfile.Append(value);
			cfile.Append("-.");
		}

		/*	Attribute parsing starts: at the ! of the !--
							 returns: next char past '>'
		*/
		public override int Parse(string p, int index, TiXmlParsingData data, int encoding)
		{
			TiXmlDocument document = GetDocument();
			value = "";

			index = SkipWhiteSpace(p, index, encoding);

			if (data != null)
			{
				data.Stamp(p, index, encoding);
				location = data.Cursor();
			}

			const string startTag = "<!--";
			const string endTag = "-->";

			if (!StringEqual(p, index, startTag, false, encoding))
			{
				document.SetError(ErrorType.TIXML_ERROR_PARSING_COMMENT, p, index, data, encoding);
				return INVALID_STRING_INDEX;
			}
			index += startTag.Length;

			// [ 1475201 ] TinyXML parses entities in comments
			// Oops - ReadText doesn't work, because we don't want to parse the entities.
			// p = ReadText( p, &value, false, endTag, false, encoding );
			//
			// from the XML spec:
			/*
			 [Definition: Comments may appear anywhere in a document outside other markup; in addition, 
						  they may appear within the document type declaration at places allowed by the grammar. 
						  They are not part of the document's character data; an XML processor MAY, but need not, 
						  make it possible for an application to retrieve the text of comments. For compatibility, 
						  the string "--" (double-hyphen) MUST NOT occur within comments.] Parameter entity 
						  references MUST NOT be recognized within comments.

						  An example of a comment:

						  <!-- declarations for <head> & <body> -.
			*/

			value = "";
			StringBuilder _value = new StringBuilder();
			// Keep all the white space.
			//while (	p && *p && !StringEqual( p, endTag, false, encoding ) )
			while (index >= 0 && index < p.Length && !StringEqual(p, index, endTag, false, encoding))
			{
				_value.Append(p, index, 1);
				++index;
			}

			if (index >= 0 && index < p.Length)
				index += endTag.Length;
			value = _value.ToString();
			return index;
		}

		/// <summary>
		/// Cast to a more defined type. Will return null not of the requested type.
		/// </summary>
		public override TiXmlComment ToComment() { return this; }

		/// <summary>
		/// Walk the XML tree visiting this node and all of its children. 
		/// </summary>
		public override bool Accept(TiXmlVisitor visitor)
		{
			return visitor.Visit(this);
		}

		protected void CopyTo(TiXmlComment target)
		{
			base.CopyTo(target);
		}
	};
}