using System;
using System.Collections.Generic;
using System.Text;

namespace TiXml
{
	/// <summary>
	/// The element is a container class. It has a value, the element name,
	/// and can contain other elements, text, comments, and unknowns.
	/// Elements also contain an arbitrary number of attributes.
	/// </summary>
	public class TiXmlElement : TiXmlNode
	{
		private TiXmlAttributeSet attributeSet = new TiXmlAttributeSet();

		/// <summary>
		/// Construct an element.
		/// </summary>
		public TiXmlElement(string _value)
			: base(TiXmlNode.NodeType.ELEMENT)
		{
			//firstChild = lastChild = null;
			value = _value;
		}

		public TiXmlElement(TiXmlElement copy)
			: base(TiXmlNode.NodeType.ELEMENT)
		{
			//firstChild = lastChild = null;
			copy.CopyTo(this);
		}

		/// <summary>
		/// Given an attribute name, Attribute() returns the value for the attribute of that name, or null if none exists.
		/// </summary>
		public string Attribute(string name)
		{
			TiXmlAttribute node = attributeSet.Find(name);
			if (node != null)
				return node.Value();
			return null;
		}
#if UNUSED
		/// <summary>
		/// Given an attribute name, Attribute() returns the value for the attribute of that name, or null if none exists.
		/// If the attribute exists and can be converted to an integer,	the integer value will be put in the return 'i', if 'i'	is non-null.
		/// </summary>
		public string Attribute(string name, out int i)
		{
			string s = Attribute(name);
			//if ( i )
			{
				if (s != null)
				{
					i = int.Parse(s);
				}
				else
				{
					i = 0;
				}
			}
			return s;
		}

		/// <summary>
		/// Given an attribute name, Attribute() returns the value for the attribute of that name, or null if none exists.
		/// If the attribute exists and can be converted to an double, the double value will be put in the return 'd', if 'd' is non-null.
		/// </summary>
		public string Attribute(string name, out double d)
		{
			string s = Attribute(name);
			//if ( d )
			{
				if (s != null)
				{
					d = double.Parse(s);
				}
				else
				{
					d = 0;
				}
			}
			return s;
		}
#endif
		/// <summary>
		/// QueryIntAttribute examines the attribute - it is an alternative to the Attribute() method with richer error checking.
		/// If the attribute is an integer, it is stored in 'value' and the call returns TIXML_SUCCESS. If it is not an integer, 
		/// it returns TIXML_WRONG_TYPE. If the attribute does not exist, then TIXML_NO_ATTRIBUTE is returned.
		/// </summary>
		public TiXmlQueryResult QueryIntAttribute(string name, ref int ival)
		{
			TiXmlAttribute node = attributeSet.Find(name);
			if (node == null)
				return TiXmlQueryResult.TIXML_NO_ATTRIBUTE;
			return node.QueryIntValue(ref ival);
		}

		/// <summary>
		/// QueryDoubleAttribute examines the attribute
		/// </summary>
		/// <see cref="QueryIntAttribute()"/>
		public TiXmlQueryResult QueryDoubleAttribute(string name, ref double dval)
		{
			TiXmlAttribute node = attributeSet.Find(name);
			if (node == null)
				return TiXmlQueryResult.TIXML_NO_ATTRIBUTE;
			return node.QueryDoubleValue(ref dval);
		}

		/// <summary>
		/// QueryFloatAttribute examines the attribute
		/// </summary>
		/// <see cref="QueryIntAttribute()"/>
		public TiXmlQueryResult QueryFloatAttribute(string name, ref float _value)
		{
			double d = 0;
			TiXmlQueryResult result = QueryDoubleAttribute(name, ref d);
			if (result == TiXmlQueryResult.TIXML_SUCCESS)
			{
				_value = (float)d;
			}
			return result;
		}

		/// <summary>
		/// Sets an attribute of name to a given value. The attribute will be created if it does not exist, or changed if it does.
		/// </summary>
		public void SetAttribute(string cname, string cvalue)
		{
			TiXmlAttribute node = attributeSet.Find(cname);
			if (node != null)
			{
				node.SetValue(cvalue);
				return;
			}

			TiXmlAttribute attrib = new TiXmlAttribute(cname, cvalue);
			if (attrib != null)
			{
				attributeSet.Add(attrib);
			}
			else
			{
				TiXmlDocument document = GetDocument();
				if (document != null)
					document.SetError(ErrorType.TIXML_ERROR_OUT_OF_MEMORY, null, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
			}
		}

		/// <summary>
		/// Sets an attribute of name to a given value. The attribute will be created if it does not exist, or changed if it does.
		/// </summary>
		public void SetAttribute(string name, int value)
		{
			//char buf[64];
			//#if TIXML_SNPRINTF
			//	TIXML_SNPRINTF( buf, sizeof(buf), "%d", val );
			//#else
			//	sprintf( buf, "%d", val );
			//#endif
			//SetAttribute( name, buf );
			SetAttribute(name, value.ToString());
		}

		/// <summary>
		/// Sets an attribute of name to a given value. The attribute will be created if it does not exist, or changed if it does.
		/// </summary>
		public void SetDoubleAttribute(string name, double value)
		{
			//char buf[256];
			//#if TIXML_SNPRINTF
			//	TIXML_SNPRINTF( buf, sizeof(buf), "%f", val );
			//#else
			//	sprintf( buf, "%f", val );
			//#endif
			//SetAttribute( name, buf );
			SetAttribute(name, value.ToString());
		}

		/// <summary>
		/// Deletes an attribute with the given name.
		/// </summary>
		public void RemoveAttribute(string name)
		{
			TiXmlAttribute node = attributeSet.Find(name);
			if (node != null)
			{
				attributeSet.Remove(node);
				//delete node;
			}
		}

		/// <summary>
		/// Access the first attribute in this element.
		/// </summary>
		public TiXmlAttribute FirstAttribute() { return attributeSet.First(); }

		/// <summary>
		/// Access the last attribute in this element.
		/// </summary>
		public TiXmlAttribute LastAttribute() { return attributeSet.Last(); }

		/** Convenience function for easy access to the text inside an element. Although easy
			and concise, GetText() is limited compared to getting the TiXmlText child
			and accessing it directly.
	
			If the first child of 'this' is a TiXmlText, the GetText()
			returns the character string of the Text node, else null is returned.

			This is a convenient method for getting the text of simple contained text:
			@verbatim
			<foo>This is text</foo>
			const char* str = fooElement.GetText();
			@endverbatim

			'str' will be a pointer to "This is text". 
		
			Note that this function can be misleading. If the element foo was created from
			this XML:
			@verbatim
			<foo><b>This is text</b></foo> 
			@endverbatim

			then the value of str would be null. The first child node isn't a text node, it is
			another element. From this XML:
			@verbatim
			<foo>This is <b>text</b></foo> 
			@endverbatim
			GetText() will return "This is ".

			WARNING: GetText() accesses a child node - don't become confused with the 
					 similarly named TiXmlHandle::Text() and TiXmlNode::ToText() which are 
					 safe type casts on the referenced node.
		*/
		public string GetText()
		{
			TiXmlNode child = this.FirstChild();
			if (child != null)
			{
				TiXmlText childText = child.ToText();
				if (childText != null)
				{
					return childText.Value();
				}
			}
			return null;
		}

		/// <summary>
		/// Creates a new Element and returns it - the returned element is a copy.
		/// </summary>
		public override TiXmlNode Clone()
		{
			TiXmlElement clone = new TiXmlElement(Value());
			if (clone == null)
				return null;

			CopyTo(clone);
			return clone;
		}

		/// <summary>
		/// Print the Element to a FILE stream.
		/// </summary>
		public override void Print(StringBuilder cfile, int depth)
		{
			//assert( cfile );
			for (int i = 0; i < depth; i++)
			{
				//fprintf( cfile, "    " );
				cfile.Append("    ");
			}

			cfile.Append("<"); cfile.Append(value);
			//fprintf( cfile, "<%s", value.c_str() );

			for (TiXmlAttribute attrib = attributeSet.First(); attrib != null; attrib = attrib.Next())
			{
				cfile.Append(" ");
				//fprintf(cfile, " ");			
				attrib.Print(cfile, depth);
			}

			// There are 3 different formatting approaches:
			// 1) An element without children is printed as a <foo /> node
			// 2) An element with only a text child is printed as <foo> text </foo>
			// 3) An element with children is printed on multiple lines.
			if (firstChild == null)
			{
				//fprintf(cfile, " />");
				cfile.Append(" />");
			}
			else if (firstChild == lastChild && firstChild.ToText() != null)
			{
				//fprintf(cfile, ">");
				cfile.Append(">");
				firstChild.Print(cfile, depth + 1);
				//fprintf(cfile, "</%s>", value.c_str());
				cfile.Append("</"); cfile.Append(value); cfile.Append(">");
			}
			else
			{
				//fprintf(cfile, ">");
				cfile.Append(">");

				for (TiXmlNode node = firstChild; node != null; node = node.NextSibling())
				{
					if (node.ToText() == null)
					{
						//fprintf(cfile, "\n");
						cfile.Append("\n");
					}
					node.Print(cfile, depth + 1);
				}
				//fprintf(cfile, "\n");
				cfile.Append("\n");
				for (int i = 0; i < depth; ++i)
				{
					//fprintf(cfile, "    ");
					cfile.Append("    ");
				}
				//fprintf(cfile, "</%s>", value.c_str());
				cfile.Append("</"); cfile.Append(value); cfile.Append(">");
			}

		}

		/*	Attribtue parsing starts: next char past '<'
							 returns: next char past '>'
		*/
		public override int Parse(string p, int index, TiXmlParsingData data, int encoding)
		{
			index = SkipWhiteSpace(p, index, encoding);
			TiXmlDocument document = GetDocument();

			if (p == null || index < 0 || index >= p.Length)
			{
				if (document != null)
					document.SetError(ErrorType.TIXML_ERROR_PARSING_ELEMENT, null, 0, null, encoding);
				return 0;
			}

			if (data != null)
			{
				data.Stamp(p, index, encoding);
				location = data.Cursor();
			}

			if (p[index] != '<')
			{
				if (document != null)
					document.SetError(ErrorType.TIXML_ERROR_PARSING_ELEMENT, p, index, data, encoding);
				return 0;
			}

			index = SkipWhiteSpace(p, index + 1, encoding);

			// Read the name.
			int pErr = index;

			index = ReadName(p, index, ref value, encoding);
			if (index < 0 || index >= p.Length)
			{
				if (document != null)
					document.SetError(ErrorType.TIXML_ERROR_FAILED_TO_READ_ELEMENT_NAME, p, pErr, data, encoding);
				return INVALID_STRING_INDEX;
			}

			string endTag = "</";
			endTag += value;
			endTag += ">";

			// Check for and read attributes. Also look for an empty
			// tag or an end tag.
			while (index >= 0 && index < p.Length)
			{
				pErr = index;
				index = SkipWhiteSpace(p, index, encoding);
				if (index < 0 || index >= p.Length)
				{
					if (document != null)
						document.SetError(ErrorType.TIXML_ERROR_READING_ATTRIBUTES, p, pErr, data, encoding);
					return -1;
				}
				if (p[index] == '/')
				{
					++index;
					// Empty tag.
					if (p[index] != '>')
					{
						if (document != null)
							document.SetError(ErrorType.TIXML_ERROR_PARSING_EMPTY, p, index, data, encoding);
						return 0;
					}
					return index + 1;
				}
				else if (p[index] == '>')
				{
					// Done with attributes (if there were any.)
					// Read the value -- which can include other
					// elements -- read the end tag, and return.
					++index;
					index = ReadValue(p, index, data, encoding);		// Note this is an Element method, and will set the error if one happens.
					if (index < 0 || index >= p.Length)
					{	// We were looking for the end tag, but found nothing.
						// Fix for [ 1663758 ] Failure to report error on bad XML
						if (document != null)
							document.SetError(ErrorType.TIXML_ERROR_READING_END_TAG, p, index, data, encoding);
						return INVALID_STRING_INDEX;
					}

					// We should find the end tag now
					if (StringEqual(p, index, endTag, false, encoding))
					{
						index += endTag.Length;
						return index;
					}
					else
					{
						if (document != null)
							document.SetError(ErrorType.TIXML_ERROR_READING_END_TAG, p, index, data, encoding);
						return INVALID_STRING_INDEX;
					}
				}
				else
				{
					// Try to read an attribute:
					TiXmlAttribute attrib = new TiXmlAttribute();
					if (attrib == null)
					{
						if (document != null)
							document.SetError(ErrorType.TIXML_ERROR_OUT_OF_MEMORY, p, pErr, data, encoding);
						return -1;
					}

					attrib.SetDocument(document);
					pErr = index;
					index = attrib.Parse(p, index, data, encoding);

					if (index < 0 || index >= p.Length)
					{
						if (document != null)
							document.SetError(ErrorType.TIXML_ERROR_PARSING_ELEMENT, p, pErr, data, encoding);
						//delete attrib;
						return INVALID_STRING_INDEX;
					}

					// Handle the strange case of double attributes:
					TiXmlAttribute node = attributeSet.Find(attrib.Name());
					if (node != null)
					{
						node.SetValue(attrib.Value());
						//delete attrib;
						return INVALID_STRING_INDEX;
					}

					attributeSet.Add(attrib);
				}
			}
			return index;
		}

		/// <summary>
		/// Cast to a more defined type. Will return null not of the requested type.
		/// </summary>
		public override TiXmlElement ToElement() { return this; }

		/// <summary>
		/// Walk the XML tree visiting this node and all of its children. 
		/// </summary>
		public override bool Accept(TiXmlVisitor visitor)
		{
			if (visitor.VisitEnter(this, attributeSet.First()))
			{
				for (TiXmlNode node = FirstChild(); node != null; node = node.NextSibling())
				{
					if (!node.Accept(visitor))
						break;
				}
			}
			return visitor.VisitExit(this);
		}

		protected void CopyTo(TiXmlElement target)
		{
			// superclass:
			base.CopyTo(target);

			// Element class: 
			// Clone the attributes, then clone the children.
			TiXmlAttribute attribute = null;
			for (attribute = attributeSet.First(); attribute != null; attribute = attribute.Next())
			{
				target.SetAttribute(attribute.Name(), attribute.Value());
			}

			TiXmlNode node = null;
			for (node = firstChild; node != null; node = node.NextSibling())
			{
				target.LinkEndChild(node.Clone());
			}
		}

		/// <summary>
		/// Like clear, but initializes 'this' object as well
		/// </summary>
		protected void ClearThis()
		{
			Clear();
			while (attributeSet.First() != null)
			{
				TiXmlAttribute node = attributeSet.First();
				attributeSet.Remove(node);
				//delete node;
			}
		}

		/// <summary>
		/// [internal use]
		/// Reads the "value" of the element -- another element, or text. 
		/// This should terminate with the current end tag.
		/// </summary>
		protected int ReadValue(string p, int index, TiXmlParsingData data, int encoding)
		{
			TiXmlDocument document = GetDocument();

			// Read in text and elements in any order.
			int pWithWhiteSpace = index;
			index = SkipWhiteSpace(p, index, encoding);

			while (index >= 0 && index < p.Length)
			{
				if (p[index] != '<')
				{
					// Take what we have, make a text element.
					TiXmlText textNode = new TiXmlText("");

					if (textNode == null)
					{
						if (document == null)
							document.SetError(ErrorType.TIXML_ERROR_OUT_OF_MEMORY, null, 0, null, encoding);
						return INVALID_STRING_INDEX;
					}

					if (IsWhiteSpaceCondensed())
					{
						index = textNode.Parse(p, index, data, encoding);
					}
					else
					{
						// Special case: we want to keep the white space
						// so that leading spaces aren't removed.
						index = textNode.Parse(p, pWithWhiteSpace, data, encoding);
					}

					if (!textNode.Blank())
						LinkEndChild(textNode);
					//else
					//	delete textNode;
				}
				else
				{
					// We hit a '<'
					// Have we hit a new element or an end tag? This could also be
					// a TiXmlText in the "CDATA" style.
					if (StringEqual(p, index, "</", false, encoding))
					{
						return index;
					}
					else
					{
						TiXmlNode node = Identify(p, index, encoding);
						if (node != null)
						{
							index = node.Parse(p, index, data, encoding);
							LinkEndChild(node);
						}
						else
						{
							return -1;
						}
					}
				}
				pWithWhiteSpace = index;
				index = SkipWhiteSpace(p, index, encoding);
			}

			if (index < 0)
			{
				if (document != null)
					document.SetError(ErrorType.TIXML_ERROR_READING_ELEMENT_VALUE, null, 0, null, encoding);
			}
			return index;
		}
	};
}