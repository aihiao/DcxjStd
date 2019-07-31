using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiXml
{
	/// <summary>
	/// Print to memory functionality. The TiXmlPrinter is useful when you need to:
	/// 
	/// -# Print to memory (especially in non-STL mode)
	/// -# Control formatting (line endings, etc.)
	/// 
	/// When constructed, the TiXmlPrinter is in its default "pretty printing" mode.
	/// Before calling Accept() you can call methods to control the printing
	/// of the XML document. After TiXmlNode::Accept() is called, the printed document can
	/// be accessed via the CStr(), Str(), and Size() methods.
	/// 
	/// TiXmlPrinter uses the Visitor API.
	/// @verbatim
	/// TiXmlPrinter printer;
	/// printer.SetIndent( "\t" );
	/// 
	/// doc.Accept( &printer );
	/// fprintf( stdout, "%s", printer.CStr() );
	/// @endverbatim
	/// </summary>
	public class TiXmlPrinter : TiXmlVisitor
	{
		private int depth = 0;
		private bool simpleTextPrint = false;
		private StringBuilder buffer = new StringBuilder();
		private string indent = "    ";
		private string lineBreak = "\n";

		public override bool VisitEnter(TiXmlDocument doc)
		{
			return true;
		}

		public override bool VisitExit(TiXmlDocument doc)
		{
			return true;
		}

		public override bool VisitEnter(TiXmlElement element, TiXmlAttribute firstAttribute)
		{
			DoIndent();
			buffer.Append("<");
			buffer.Append(element.Value());

			for (TiXmlAttribute attrib = firstAttribute; attrib != null; attrib = attrib.Next())
			{
				buffer.Append(" ");
				attrib.Print(buffer, 0);
			}

			if (element.FirstChild() == null)
			{
				buffer.Append(" />");
				DoLineBreak();
			}
			else
			{
				buffer.Append(">");
				if (element.FirstChild().ToText() != null
					&& element.LastChild() == element.FirstChild()
					&& element.FirstChild().ToText().CDATA() == false)
				{
					simpleTextPrint = true;
					// no DoLineBreak()!
				}
				else
				{
					DoLineBreak();
				}
			}
			++depth;
			return true;
		}

		public override bool VisitExit(TiXmlElement element)
		{
			--depth;
			if (element.FirstChild() == null)
			{
				// nothing.
			}
			else
			{
				if (simpleTextPrint)
				{
					simpleTextPrint = false;
				}
				else
				{
					DoIndent();
				}
				buffer.Append("</");
				buffer.Append(element.Value());
				buffer.Append(">");
				DoLineBreak();
			}
			return true;
		}

		public override bool Visit(TiXmlDeclaration declaration)
		{
			DoIndent();
			declaration.Print(buffer, 0);
			DoLineBreak();
			return true;
		}
		public override bool Visit(TiXmlText text)
		{
			if (text.CDATA())
			{
				DoIndent();
				buffer.Append("<![CDATA[");
				buffer.Append(text.Value());
				buffer.Append("]]>");
				DoLineBreak();
			}
			else if (simpleTextPrint)
			{
				StringBuilder str = new StringBuilder();
				TiXmlBase.EncodeString(text.Value(), str);
				buffer.Append(str);
			}
			else
			{
				DoIndent();
				StringBuilder str = new StringBuilder();
				TiXmlBase.EncodeString(text.Value(), str);
				buffer.Append(str);
				DoLineBreak();
			}
			return true;
		}
		public override bool Visit(TiXmlComment comment)
		{
			DoIndent();
			buffer.Append("<!--");
			buffer.Append(comment.Value());
			buffer.Append("-.");
			DoLineBreak();
			return true;
		}
		public override bool Visit(TiXmlUnknown unknown)
		{
			DoIndent();
			buffer.Append("<");
			buffer.Append(unknown.Value());
			buffer.Append(">");
			DoLineBreak();
			return true;
		}

		/// <summary>
		/// Set the indent characters for printing. By default 4 spaces
		/// but tab (\t) is also useful, or null/empty string for no indentation.
		/// </summary>
		public void SetIndent(string _indent) { indent = _indent != null ? _indent : ""; }

		/// <summary>
		/// Query the indention string.
		/// </summary>
		public string Indent() { return indent; }

		/// <summary>
		/// Set the line breaking string. By default set to newline (\n). 
		/// Some operating systems prefer other characters, or can be
		/// set to the null/empty string for no indentation.
		/// </summary>
		/// <param name="_lineBreak"></param>
		public void SetLineBreak(string _lineBreak) { lineBreak = _lineBreak != null ? _lineBreak : ""; }

		/// <summary>
		/// Query the current line breaking string.
		/// </summary>
		public string LineBreak() { return lineBreak; }

		/// <summary>
		/// Switch over to "stream printing" which is the most dense formatting without 
		/// linebreaks. Common when the XML is needed for network transmission.
		/// </summary>
		public void SetStreamPrinting()
		{
			indent = "";
			lineBreak = "";
		}

		/// <summary>
		/// Return the result.
		/// </summary>
		public string CStr() { return buffer.ToString(); }

		/// <summary>
		/// Return the length of the result string.
		/// </summary>
		public int Size() { return buffer.Length; }

		private void DoIndent()
		{
			for (int i = 0; i < depth; ++i)
				buffer.Append(indent);
		}
		private void DoLineBreak()
		{
			buffer.Append(lineBreak);
		}
	};
}