using System;
using System.Collections.Generic;
using System.Text;

namespace TiXml
{
	/// <summary>
	///	A class used to manage a group of attributes.
	///	It is only used internally, both by the ELEMENT and the DECLARATION.
	///	
	///	The set can be changed transparent to the Element and Declaration
	///	classes that use it, but NOT transparent to the Attribute
	///	which has to implement a next() and previous() method. Which makes
	///	it a bit problematic and prevents the use of STL.
	///
	///	This version is implemented with circular lists because:
	///		- I like circular lists
	///		- it demonstrates some independence from the (typical) doubly linked list.
	/// </summary>
	public class TiXmlAttributeSet
	{
		private TiXmlAttribute sentinel = new TiXmlAttribute();

		public TiXmlAttributeSet()
		{
			sentinel.next = sentinel;
			sentinel.prev = sentinel;
		}

		public void Add(TiXmlAttribute addMe)
		{
			//assert( !Find( addMe.Name() ) );	// Shouldn't be multiply adding to the set.
			addMe.next = sentinel;
			addMe.prev = sentinel.prev;

			sentinel.prev.next = addMe;
			sentinel.prev = addMe;
		}

		public void Remove(TiXmlAttribute removeMe)
		{
			TiXmlAttribute node;

			for (node = sentinel.next; node != sentinel; node = node.next)
			{
				if (node == removeMe)
				{
					node.prev.next = node.next;
					node.next.prev = node.prev;
					node.next = null;
					node.prev = null;
					return;
				}
			}
			//assert( 0 );		// we tried to remove a non-linked attribute.
		}

		public TiXmlAttribute First() { return (sentinel.next == sentinel) ? null : sentinel.next; }
		public TiXmlAttribute Last() { return (sentinel.prev == sentinel) ? null : sentinel.prev; }

		public TiXmlAttribute Find(string _name)
		{
			for (TiXmlAttribute node = sentinel.next; node != sentinel; node = node.next)
			{
				if (string.Compare(node.Name(), _name) == 0)
					return node;
			}
			return null;
		}
	};
}