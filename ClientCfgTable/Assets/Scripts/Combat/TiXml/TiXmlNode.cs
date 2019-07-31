using System;
using System.Collections.Generic;

namespace TiXml
{
	/// <summary>
	/// The parent class for everything in the Document Object Model. (Except for attributes).
	/// Nodes have siblings, a parent, and children. A node can be in a document, or stand on its own. 
	/// The type of a TiXmlNode can be queried, and it can be cast to its more defined type.
	/// </summary>
	public abstract class TiXmlNode : TiXmlBase
	{
		protected TiXmlNode parent = null;
		protected NodeType type = NodeType.UNKNOWN;
		protected string value = "";

		protected TiXmlNode firstChild = null;
		protected TiXmlNode lastChild = null;

		protected TiXmlNode prev = null;
		protected TiXmlNode next = null;

		/// <summary>
		/// The types of XML nodes supported by TinyXml. (All the unsupported types are picked up by UNKNOWN.)
		/// </summary>
		public enum NodeType
		{
			DOCUMENT,
			ELEMENT,
			COMMENT,
			UNKNOWN,
			TEXT,
			DECLARATION,
			TYPECOUNT
		};

		/// <summary>
		/// The meaning of 'value' changes for the specific type of
		/// TiXmlNode.
		/// @verbatim
		/// Document:	filename of the xml file
		/// Element:	name of the element
		/// Comment:	the comment text
		/// Unknown:	the tag contents
		/// Text:		the text string
		/// @endverbatim
		/// 
		/// The subclasses will wrap this function.
		/// </summary>	
		public string Value() { return value; }

		/// <summary>
		/// Changes the value of the node. Defined as:
		/// @verbatim
		/// Document:	filename of the xml file
		/// Element:	name of the element
		/// Comment:	the comment text
		/// Unknown:	the tag contents
		/// Text:		the text string
		/// @endverbatim
		/// </summary>	
		public void SetValue(string _value) { value = _value; }

		/// <summary>
		/// Delete all the children of this node. Does not affect 'this'.
		/// </summary>
		public void Clear()
		{
			TiXmlNode node = firstChild;
			//TiXmlNode temp = null;

			while (node != null)
			{
				//temp = node;
				node = node.next;
				//delete temp;
			}

			firstChild = null;
			lastChild = null;
		}

		/// <summary>
		/// One step up the DOM.
		/// </summary>
		public TiXmlNode Parent() { return parent; }

		/// <summary>
		/// The first child of this node. Will be null if there are no children.
		/// </summary>
		public TiXmlNode FirstChild() { return firstChild; }

		/// <summary>
		/// The first child of this node with the matching 'value'. Will be null if none found.
		/// </summary>
		public TiXmlNode FirstChild(string _value)
		{
			for (TiXmlNode node = firstChild; node != null; node = node.next)
			{
				if (node.Value().CompareTo(_value) == 0)
					return node;
			}
			return null;
		}

		/// <summary>
		/// The last child of this node. Will be null if there are no children.
		/// </summary>
		public TiXmlNode LastChild() { return lastChild; }

		/// <summary>
		/// The last child of this node matching 'value'. Will be null if there are no children.
		/// </summary>
		public TiXmlNode LastChild(string _value)
		{
			for (TiXmlNode node = lastChild; node != null; node = node.prev)
			{
				if (node.Value().CompareTo(_value) == 0)
					return node;
			}
			return null;
		}

		/// <summary>
		/// An alternate way to walk the children of a node.
		/// One way to iterate over nodes is:
		/// @verbatim
		/// 	for( child = parent.FirstChild(); child; child = child.NextSibling() )
		/// @endverbatim
		/// 
		/// IterateChildren does the same thing with the syntax:
		/// @verbatim
		/// 	child = 0;
		/// 	while( child = parent.IterateChildren( child ) )
		/// @endverbatim
		/// 
		/// IterateChildren takes the previous child as input and finds
		/// the next one. If the previous child is null, it returns the
		/// first. IterateChildren will return null when done.
		/// </summary>	
		public TiXmlNode IterateChildren(TiXmlNode previous)
		{
			if (previous == null)
			{
				return FirstChild();
			}
			else
			{
				//assert( previous.parent == this );
				return previous.NextSibling();
			}
		}

		/// <summary>
		/// This flavor of IterateChildren searches for children with a particular 'value'
		/// </summary>
		public TiXmlNode IterateChildren(string _value, TiXmlNode previous)
		{
			if (previous == null)
			{
				return FirstChild(_value);
			}
			else
			{
				//assert( previous.parent == this );
				return previous.NextSibling(_value);
			}
		}

		/// <summary>
		/// Add a new node related to this. Adds a child past the LastChild.
		/// Returns a pointer to the new object or NULL if an error occurred.
		/// </summary>
		public TiXmlNode InsertEndChild(TiXmlNode addThis)
		{
			if (addThis.Type() == TiXmlNode.NodeType.DOCUMENT)
			{
				if (GetDocument() != null)
					GetDocument().SetError(ErrorType.TIXML_ERROR_DOCUMENT_TOP_ONLY, null, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
				return null;
			}
			TiXmlNode node = addThis.Clone();
			if (node == null)
				return null;
			return LinkEndChild(node);
		}

		/// <summary>
		/// Add a new node related to this. Adds a child past the LastChild.
		/// 
		/// NOTE: the node to be added is passed by pointer, and will be
		/// henceforth owned (and deleted) by tinyXml. This method is efficient
		/// and avoids an extra copy, but should be used with care as it
		/// uses a different memory model than the other insert functions.
		/// </summary>	
		/// <seealso cref="InsertEndChild"/>
		public TiXmlNode LinkEndChild(TiXmlNode node)
		{
			//assert( node.parent == 0 || node.parent == this );
			//assert( node.GetDocument() == 0 || node.GetDocument() == this.GetDocument() );

			if (node.Type() == TiXmlNode.NodeType.DOCUMENT)
			{
				//delete node;
				if (GetDocument() != null)
					GetDocument().SetError(ErrorType.TIXML_ERROR_DOCUMENT_TOP_ONLY, null, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
				return null;
			}

			node.parent = this;

			node.prev = lastChild;
			node.next = null;

			if (lastChild != null)
				lastChild.next = node;
			else
				firstChild = node;			// it was an empty list.

			lastChild = node;
			return node;
		}

		/// <summary>
		/// Add a new node related to this. Adds a child before the specified child.
		/// Returns a pointer to the new object or NULL if an error occurred.
		/// </summary>
		public TiXmlNode InsertBeforeChild(TiXmlNode beforeThis, TiXmlNode addThis)
		{
			if (beforeThis == null || beforeThis.parent != this)
			{
				return null;
			}
			if (addThis.Type() == TiXmlNode.NodeType.DOCUMENT)
			{
				if (GetDocument() != null)
					GetDocument().SetError(ErrorType.TIXML_ERROR_DOCUMENT_TOP_ONLY, null, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
				return null;
			}

			TiXmlNode node = addThis.Clone();
			if (node == null)
				return null;
			node.parent = this;

			node.next = beforeThis;
			node.prev = beforeThis.prev;
			if (beforeThis.prev != null)
			{
				beforeThis.prev.next = node;
			}
			else
			{
				//assert( firstChild == beforeThis );
				firstChild = node;
			}
			beforeThis.prev = node;
			return node;
		}

		/// <summary>
		/// Add a new node related to this. Adds a child after the specified child.
		/// Returns a pointer to the new object or NULL if an error occurred.
		/// </summary>
		public TiXmlNode InsertAfterChild(TiXmlNode afterThis, TiXmlNode addThis)
		{
			if (afterThis == null || afterThis.parent != this)
			{
				return null;
			}
			if (addThis.Type() == TiXmlNode.NodeType.DOCUMENT)
			{
				if (GetDocument() != null)
					GetDocument().SetError(ErrorType.TIXML_ERROR_DOCUMENT_TOP_ONLY, null, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
				return null;
			}

			TiXmlNode node = addThis.Clone();
			if (node == null)
				return null;
			node.parent = this;

			node.prev = afterThis;
			node.next = afterThis.next;
			if (afterThis.next != null)
			{
				afterThis.next.prev = node;
			}
			else
			{
				//assert( lastChild == afterThis );
				lastChild = node;
			}
			afterThis.next = node;
			return node;
		}

		/// <summary>
		/// Replace a child of this node.
		/// Returns a pointer to the new object or NULL if an error occurred.
		/// </summary>
		public TiXmlNode ReplaceChild(TiXmlNode replaceThis, TiXmlNode withThis)
		{
			if (replaceThis.parent != this)
				return null;

			TiXmlNode node = withThis.Clone();
			if (node == null)
				return null;

			node.next = replaceThis.next;
			node.prev = replaceThis.prev;

			if (replaceThis.next != null)
				replaceThis.next.prev = node;
			else
				lastChild = node;

			if (replaceThis.prev != null)
				replaceThis.prev.next = node;
			else
				firstChild = node;

			//delete replaceThis;
			node.parent = this;
			return node;
		}

		/// <summary>
		/// Delete a child of this node.
		/// </summary>
		public bool RemoveChild(TiXmlNode removeThis)
		{
			if (removeThis.parent != this)
			{
				//assert(0);
				return false;
			}

			if (removeThis.next != null)
				removeThis.next.prev = removeThis.prev;
			else
				lastChild = removeThis.prev;

			if (removeThis.prev != null)
				removeThis.prev.next = removeThis.next;
			else
				firstChild = removeThis.next;

			//delete removeThis;
			return true;
		}

		/// <summary>
		/// Navigate to a sibling node.
		/// </summary>
		public TiXmlNode PreviousSibling() { return prev; }

		/// <summary>
		/// Navigate to a sibling node.
		/// </summary>
		public TiXmlNode PreviousSibling(string _value)
		{
			TiXmlNode node;
			for (node = prev; node != null; node = node.prev)
			{
				if (node.Value().CompareTo(_value) == 0)
					return node;
			}
			return null;
		}

		/// <summary>
		/// Navigate to a sibling node.
		/// </summary>
		public TiXmlNode NextSibling() { return next; }

		/// <summary>
		/// Navigate to a sibling node with the given 'value'.
		/// </summary>
		public TiXmlNode NextSibling(string _value)
		{
			TiXmlNode node;
			for (node = next; node != null; node = node.next)
			{
				if (node.Value().CompareTo(_value) == 0)
					return node;
			}
			return null;
		}

		/// <summary>
		/// Convenience function to get through elements. Calls NextSibling and ToElement. 
		/// Will skip all non-Element nodes. Returns 0 if there is not another element.
		/// </summary>
		public TiXmlElement NextSiblingElement()
		{
			TiXmlNode node;

			for (node = NextSibling(); node != null; node = node.NextSibling())
			{
				if (node.ToElement() != null)
					return node.ToElement();
			}
			return null;
		}

		/// <summary>
		/// Convenience function to get through elements. Calls NextSibling and ToElement. 
		/// Will skip all non-Element nodes. Returns 0 if there is not another element.
		/// </summary>
		public TiXmlElement NextSiblingElement(string _value)
		{
			TiXmlNode node;

			for (node = NextSibling(_value); node != null; node = node.NextSibling(_value))
			{
				if (node.ToElement() != null)
					return node.ToElement();
			}
			return null;
		}

		/// <summary>
		/// Convenience function to get through elements.
		/// </summary>
		public TiXmlElement FirstChildElement()
		{
			TiXmlNode node;

			for (node = FirstChild(); node != null; node = node.NextSibling())
			{
				if (node.ToElement() != null)
					return node.ToElement();
			}
			return null;
		}

		/// <summary>
		/// Convenience function to get through elements.
		/// </summary>
		public TiXmlElement FirstChildElement(string _value)
		{
			TiXmlNode node;

			for (node = FirstChild(_value); node != null; node = node.NextSibling(_value))
			{
				if (node.ToElement() != null)
					return node.ToElement();
			}
			return null;
		}

		/// <summary>
		/// Query the type (as an enumerated value, above) of this node.
		/// The possible types are: DOCUMENT, ELEMENT, COMMENT,
		/// 						UNKNOWN, TEXT, and DECLARATION.
		/// </summary>
		public NodeType Type() { return type; }

		/// <summary>
		/// Return a pointer to the Document this node lives in.
		/// Returns null if not in a document.
		/// </summary>
		public TiXmlDocument GetDocument()
		{
			TiXmlNode node;

			for (node = this; node != null; node = node.parent)
			{
				if (node.ToDocument() != null)
					return node.ToDocument();
			}
			return null;
		}

		/// <summary>
		/// Returns true if this node has no children.
		/// </summary>
		public bool NoChildren() { return firstChild == null; }

		/// <summary>
		/// Cast to a more defined type. Will return null if not of the requested type.
		/// </summary>
		public virtual TiXmlDocument ToDocument() { return null; }

		/// <summary>
		/// Cast to a more defined type. Will return null if not of the requested type.
		/// </summary>
		/// <returns></returns>
		public virtual TiXmlElement ToElement() { return null; }

		/// <summary>
		/// Cast to a more defined type. Will return null if not of the requested type.
		/// </summary>
		public virtual TiXmlComment ToComment() { return null; }

		/// <summary>
		/// Cast to a more defined type. Will return null if not of the requested type.
		/// </summary>
		public virtual TiXmlUnknown ToUnknown() { return null; }

		/// <summary>
		/// Cast to a more defined type. Will return null if not of the requested type.
		/// </summary>
		public virtual TiXmlText ToText() { return null; }

		/// <summary>
		/// Cast to a more defined type. Will return null if not of the requested type.
		/// </summary>
		public virtual TiXmlDeclaration ToDeclaration() { return null; }

		/// <summary>
		/// Create an exact duplicate of this node and return it. The memory must be deleted by the caller. 
		/// </summary>
		public abstract TiXmlNode Clone();

		/// <summary>
		/// Accept a hierchical visit the nodes in the TinyXML DOM. Every node in the 
		/// XML tree will be conditionally visited and the host will be called back
		/// via the TiXmlVisitor interface.
		/// 
		/// This is essentially a SAX interface for TinyXML. (Note however it doesn't re-parse
		/// the XML for the callbacks, so the performance of TinyXML is unchanged by using this
		/// interface versus any other.)
		/// 
		/// The interface has been based on ideas from:
		/// 
		/// - http://www.saxproject.org/
		/// - http://c2.com/cgi/wiki?HierarchicalVisitorPattern 
		/// 
		/// Which are both good references for "visiting".
		/// s
		/// An example of using Accept():
		/// @verbatim
		/// TiXmlPrinter printer;
		/// tinyxmlDoc.Accept( &printer );
		/// const char* xmlcstr = printer.CStr();
		/// @endverbatim
		/// </summary>
		public abstract bool Accept(TiXmlVisitor visitor);

		protected TiXmlNode(NodeType _type) { type = _type; }

		/// <summary>
		/// Copy to the allocated object. Shared functionality between Clone, Copy constructor, and the assignment operator.
		/// </summary>
		protected void CopyTo(TiXmlNode target)
		{
			target.SetValue(value);
			target.userData = userData;
		}

		/// <summary>
		/// Figure out what is at *p, and parse it. Returns null if it is not an xml node.
		/// </summary>
		protected TiXmlNode Identify(string p, int index, int encoding)
		{
			TiXmlNode returnNode = null;

			index = SkipWhiteSpace(p, index, encoding);
			if (p == null || index < 0 || index >= p.Length || p[index] != '<')
			{
				return null;
			}

			TiXmlDocument doc = GetDocument();
			index = SkipWhiteSpace(p, index, encoding);

			if (index < 0 || index >= p.Length)
			{
				return null;
			}

			// What is this thing? 
			// - Elements start with a letter or underscore, but xml is reserved.
			// - Comments: <!--
			// - Decleration: <?xml
			// - Everthing else is unknown to tinyxml.
			//

			const string xmlHeader = "<?xml";
			const string commentHeader = "<!--";
			const string dtdHeader = "<!";
			const string cdataHeader = "<![CDATA[";

			if ( StringEqual( p, index, xmlHeader, true, encoding ) )
			{
#if UNUSED
			TIXML_LOG( "XML parsing Declaration\n" );
#endif
				returnNode = new TiXmlDeclaration();
			}
			else if (StringEqual(p, index, commentHeader, false, encoding))
			{
#if UNUSED
			TIXML_LOG( "XML parsing Comment\n" );
#endif
				returnNode = new TiXmlComment();
			}
			else if (StringEqual(p, index, cdataHeader, false, encoding))
			{
#if UNUSED
			TIXML_LOG( "XML parsing CDATA\n" );
#endif
				TiXmlText text = new TiXmlText("");
				text.SetCDATA(true);
				returnNode = text;
			}
			else if (StringEqual(p, index, dtdHeader, false, encoding))
			{
#if UNUSED
			TIXML_LOG( "XML parsing Unknown(1)\n" );
#endif
				returnNode = new TiXmlUnknown();
			}
			else if (IsAlpha(p[index + 1], encoding) || p[index + 1] == '_')
			{
#if UNUSED
			TIXML_LOG( "XML parsing Element\n" );
#endif
				returnNode = new TiXmlElement("");
			}
			else
			{
#if UNUSED
			TIXML_LOG( "XML parsing Unknown(2)\n" );
#endif
				returnNode = new TiXmlUnknown();
			}

			if (returnNode != null)
			{
				// Set the parent, so it can report errors
				returnNode.parent = this;
			}
			else
			{
				if (doc != null)
					doc.SetError(ErrorType.TIXML_ERROR_OUT_OF_MEMORY, null, 0, null, TiXmlEncoding.TIXML_ENCODING_UNKNOWN);
			}
			return returnNode;
		}
	};
}