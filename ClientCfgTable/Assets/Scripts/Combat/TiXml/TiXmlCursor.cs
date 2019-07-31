using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiXml
{
	/// <summary>
	/// Internal structure for tracking location of items in the XML file.
	/// </summary>
	public struct TiXmlCursor
	{
		//public TiXmlCursor() { row = col = -1;/*Clear();*/ }
		public void Clear() { row = col = -1; }

		public TiXmlCursor Clone()
		{
			TiXmlCursor cursor = new TiXmlCursor();
			cursor.row = this.row;
			cursor.col = this.col;
			return cursor;
		}

		public int row;	// 0 based.
		public int col;	// 0 based.
	};
}