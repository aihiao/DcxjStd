using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TiXml
{
	/**
		A TiXmlHandle is a class that wraps a node pointer with null checks; this is
		an incredibly useful thing. Note that TiXmlHandle is not part of the TinyXml
		DOM structure. It is a separate utility class.

		Take an example:
		@verbatim
		<Document>
			<Element attributeA = "valueA">
				<Child attributeB = "value1" />
				<Child attributeB = "value2" />
			</Element>
		<Document>
		@endverbatim

		Assuming you want the value of "attributeB" in the 2nd "Child" element, it's very 
		easy to write a *lot* of code that looks like:

		@verbatim
		TiXmlElement* root = document.FirstChildElement( "Document" );
		if ( root )
		{
			TiXmlElement* element = root.FirstChildElement( "Element" );
			if ( element )
			{
				TiXmlElement* child = element.FirstChildElement( "Child" );
				if ( child )
				{
					TiXmlElement* child2 = child.NextSiblingElement( "Child" );
					if ( child2 )
					{
						// Finally do something useful.
		@endverbatim

		And that doesn't even cover "else" cases. TiXmlHandle addresses the verbosity
		of such code. A TiXmlHandle checks for null	pointers so it is perfectly safe 
		and correct to use:

		@verbatim
		TiXmlHandle docHandle( &document );
		TiXmlElement* child2 = docHandle.FirstChild( "Document" ).FirstChild( "Element" ).Child( "Child", 1 ).ToElement();
		if ( child2 )
		{
			// do something useful
		@endverbatim

		Which is MUCH more concise and useful.

		It is also safe to copy handles - internally they are nothing more than node pointers.
		@verbatim
		TiXmlHandle handleCopy = handle;
		@endverbatim

		What they should not be used for is iteration:

		@verbatim
		int i=0; 
		while ( true )
		{
			TiXmlElement* child = docHandle.FirstChild( "Document" ).FirstChild( "Element" ).Child( "Child", i ).ToElement();
			if ( !child )
				break;
			// do something
			++i;
		}
		@endverbatim

		It seems reasonable, but it is in fact two embedded while loops. The Child method is 
		a linear walk to find the element, so this code would iterate much more than it needs 
		to. Instead, prefer:

		@verbatim
		TiXmlElement* child = docHandle.FirstChild( "Document" ).FirstChild( "Element" ).FirstChild( "Child" ).ToElement();

		for( child; child; child=child.NextSiblingElement() )
		{
			// do something
		}
		@endverbatim
	*/
	public class TiXmlHandle
	{
		private TiXmlNode node;

		TiXmlHandle() { }

		/// <summary>
		/// Create a handle from any node (at any depth of the tree.) This can be a null pointer.
		/// </summary>
		TiXmlHandle(TiXmlNode _node) { this.node = _node; }

		/// <summary>
		/// Copy constructor
		/// </summary>
		TiXmlHandle(TiXmlHandle copy) { this.node = copy.node; }

		/// <summary>
		/// Return a handle to the first child node.
		/// </summary>
		TiXmlHandle FirstChild()
		{
			if (node != null)
			{
				TiXmlNode child = node.FirstChild();
				if (child != null)
					return new TiXmlHandle(child);
			}
			return new TiXmlHandle();
		}

		/// <summary>
		/// Return a handle to the first child node with the given name.
		/// </summary>
		TiXmlHandle FirstChild(string value)
		{
			if (node != null)
			{
				TiXmlNode child = node.FirstChild(value);
				if (child != null)
					return new TiXmlHandle(child);
			}
			return new TiXmlHandle();
		}

		/// <summary>
		/// Return a handle to the first child element.
		/// </summary>
		TiXmlHandle FirstChildElement()
		{
			if (node != null)
			{
				TiXmlElement child = node.FirstChildElement();
				if (child != null)
					return new TiXmlHandle(child);
			}
			return new TiXmlHandle();
		}

		/// <summary>
		/// Return a handle to the first child element with the given name.
		/// </summary>
		TiXmlHandle FirstChildElement(string value)
		{
			if (node != null)
			{
				TiXmlElement child = node.FirstChildElement(value);
				if (child != null)
					return new TiXmlHandle(child);
			}
			return new TiXmlHandle();
		}

		/// <summary>
		/// Return a handle to the "index" child with the given name. The first child is 0, the second 1, etc.
		/// </summary>
		TiXmlHandle Child(string value, int count)
		{
			if (node != null)
			{
				int i;
				TiXmlNode child = node.FirstChild(value);
				for (i = 0; child != null && i < count; child = child.NextSibling(value), ++i)
				{
					// nothing
				}
				if (child != null)
					return new TiXmlHandle(child);
			}
			return new TiXmlHandle();
		}

		/// <summary>
		/// Return a handle to the "index" child. The first child is 0, the second 1, etc.
		/// </summary>
		TiXmlHandle Child(int count)
		{
			if (node != null)
			{
				int i;
				TiXmlNode child = node.FirstChild();
				for (i = 0; child != null && i < count; child = child.NextSibling(), ++i)
				{
					// nothing
				}
				if (child != null)
					return new TiXmlHandle(child);
			}
			return new TiXmlHandle();
		}

		/// <summary>
		/// Return a handle to the "index" child element with the given name. 
		/// The first child element is 0, the second 1, etc. Note that only TiXmlElements are indexed: other types are not counted.
		/// </summary>
		TiXmlHandle ChildElement(string value, int count)
		{
			if (node != null)
			{
				int i;
				TiXmlElement child = node.FirstChildElement(value);
				for (i = 0; child != null && i < count; child = child.NextSiblingElement(value), ++i)
				{
					// nothing
				}
				if (child != null)
					return new TiXmlHandle(child);
			}
			return new TiXmlHandle();
		}

		/// <summary>
		/// Return a handle to the "index" child element. 
		/// The first child element is 0, the second 1, etc. Note that only TiXmlElements
		/// are indexed: other types are not counted.
		/// </summary>
		TiXmlHandle ChildElement(int count)
		{
			if (node != null)
			{
				int i;
				TiXmlElement child = node.FirstChildElement();
				for (i = 0; child != null && i < count; child = child.NextSiblingElement(), ++i)
				{
					// nothing
				}
				if (child != null)
					return new TiXmlHandle(child);
			}
			return new TiXmlHandle();
		}

		/// <summary>
		/// Return the handle as a TiXmlNode. This may return null.
		/// </summary>
		TiXmlNode ToNode() { return node; }

		/// <summary>
		/// Return the handle as a TiXmlElement. This may return null.
		/// </summary>
		TiXmlElement ToElement() { return ((node != null && node.ToElement() != null) ? node.ToElement() : null); }

		/// <summary>
		/// Return the handle as a TiXmlText. This may return null.
		/// </summary>
		TiXmlText ToText() { return ((node != null && node.ToText() != null) ? node.ToText() : null); }

		/// <summary>
		/// Return the handle as a TiXmlUnknown. This may return null.
		/// </summary>
		TiXmlUnknown ToUnknown() { return ((node != null && node.ToUnknown() != null) ? node.ToUnknown() : null); }
	};
}