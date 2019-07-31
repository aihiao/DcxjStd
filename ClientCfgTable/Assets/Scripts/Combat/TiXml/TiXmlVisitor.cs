using System;
using System.Collections.Generic;

namespace TiXml
{
	/// <summary>
	/// If you call the Accept() method, it requires being passed a TiXmlVisitor
	/// class to handle callbacks. For nodes that contain other nodes (Document, Element)
	/// you will get called with a VisitEnter/VisitExit pair. Nodes that are always leaves
	/// are simple called with Visit().
	///
	/// If you return 'true' from a Visit method, recursive parsing will continue. If you return
	/// false, <b>no children of this node or its siblings</b> will be Visited.
	///
	/// All flavors of Visit methods have a default implementation that returns 'true' (continue 
	/// visiting). You need to only override methods that are interesting to you.
	///
	/// Generally Accept() is called on the TiXmlDocument, although all nodes suppert Visiting.
	///
	/// You should never change the document from a callback.
	/// </summary>
	/// <seealso cref="TiXmlNode::Accept()"/>
	public class TiXmlVisitor
	{
		/// <summary>
		/// Visit a document.
		/// </summary>
		public virtual bool VisitEnter(TiXmlDocument doc) { return true; }

		/// <summary>
		/// Visit a document.
		/// </summary>
		public virtual bool VisitExit(TiXmlDocument doc) { return true; }

		/// <summary>
		/// Visit an element.
		/// </summary>
		public virtual bool VisitEnter(TiXmlElement element, TiXmlAttribute firstAttribute) { return true; }

		/// <summary>
		/// Visit an element.
		/// </summary>
		public virtual bool VisitExit(TiXmlElement element) { return true; }

		/// <summary>
		/// Visit a declaration
		/// </summary>
		public virtual bool Visit(TiXmlDeclaration declaration) { return true; }

		/// <summary>
		/// Visit a text node
		/// </summary>
		public virtual bool Visit(TiXmlText text) { return true; }

		/// <summary>
		/// Visit a comment node
		/// </summary>
		public virtual bool Visit(TiXmlComment comment) { return true; }

		/// <summary>
		/// Visit an unknown node
		/// </summary>
		public virtual bool Visit(TiXmlUnknown unknown) { return true; }
	};
}