using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiXml
{
	public class TiXmlParsingData
	{
		public TiXmlCursor cursor = new TiXmlCursor();
		private string xml;
		private int stamp;
		private int tabsize;

		/// <summary>
		/// Only used by the document!
		/// </summary>
		public TiXmlParsingData(string text, int start, int _tabsize, int row, int col)
		{
			//assert( start );
			xml = text;
			stamp = start;
			tabsize = _tabsize;
			cursor.row = row;
			cursor.col = col;
		}

		public void Stamp(string text, int now, int encoding)
		{
			//assert( now );

			// Do nothing if the tabsize is 0.
			if (tabsize < 1)
			{
				return;
			}

			// Get the current row, column.
			int row = cursor.row;
			int col = cursor.col;
			//const char* p = stamp;
			int p = stamp;
			//assert( p );

			while (p < now)
			{
				// Treat p as unsigned, so we have a happy compiler.
				char pU = xml[p];
				//const unsigned char* pU = (const unsigned char*)p;

				// Code contributed by Fletcher Dunn: (modified by lee)
				switch (pU)
				{
#if UNUSED
				case 0:
					// We *should* never get here, but in case we do, don't
					// advance past the terminating null character, ever
					return;
#endif
					case '\r':
						// bump down to the next line
						++row;
						col = 0;
						// Eat the character
						++p;

						// Check for \r\n sequence, and treat this as a single character
						if (xml[p] == '\n')
						{
							++p;
						}
						break;

					case '\n':
						// bump down to the next line
						++row;
						col = 0;

						// Eat the character
						++p;

						// Check for \n\r sequence, and treat this as a single
						// character.  (Yes, this bizarre thing does occur still
						// on some arcane platforms...)
						if (xml[p] == '\r')
						{
							++p;
						}
						break;

					case '\t':
						// Eat the character
						++p;

						// Skip to next tab stop
						col = (col / tabsize + 1) * tabsize;
						break;
#if UNUSED
				case TIXML_UTF_LEAD_0:
					if ( encoding == TIXML_ENCODING_UTF8 )
					{
						if ( *(p+1) && *(p+2) )
						{
							// In these cases, don't advance the column. These are
							// 0-width spaces.
							if ( *(pU+1)==TIXML_UTF_LEAD_1 && *(pU+2)==TIXML_UTF_LEAD_2 )
								p += 3;	
							else if ( *(pU+1)==0xbfU && *(pU+2)==0xbeU )
								p += 3;	
							else if ( *(pU+1)==0xbfU && *(pU+2)==0xbfU )
								p += 3;	
							else
								{ p +=3; ++col; }	// A normal character.
						}
					}
					else
					{
						++p;
						++col;
					}
					break;
#endif
					default:
#if UNUSED
					if ( encoding ==  TiXmlEncoding.TIXML_ENCODING_UTF8 )
					{
						// Eat the 1 to 4 byte utf8 character.
						int step = TiXmlBase::utf8ByteTable[*((const unsigned char*)p)];
						if ( step == 0 )
							step = 1;		// Error case from bad encoding, but handle gracefully.
						p += step;

						// Just advance one column, of course.
						++col;
					}
					else
#endif
						{
							++p;
							++col;
						}
						break;
				}
			}
			cursor.row = row;
			cursor.col = col;
			//assert(cursor.row >= -1);
			//assert(cursor.col >= -1);
			stamp = p;
			//assert(stamp);
		}

		public TiXmlCursor Cursor()
		{
			// 返回Clone, 防止在转换到Java时造成引用赋值
			return cursor.Clone();
		}
	};
}