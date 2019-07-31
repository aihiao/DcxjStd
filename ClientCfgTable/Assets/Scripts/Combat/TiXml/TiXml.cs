/*
www.sourceforge.net/projects/tinyxml
Original code (2.0 and earlier )copyright (c) 2000-2006 Lee Thomason (www.grinninglizard.com)

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any
damages arising from the use of this software.

Permission is granted to anyone to use this software for any
purpose, including commercial applications, and to alter it and
redistribute it freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must
not claim that you wrote the original software. If you use this
software in a product, an acknowledgment in the product documentation
would be appreciated but is not required.

2. Altered source versions must be plainly marked as such, and
must not be misrepresented as being the original software.

3. This notice may not be removed or altered from any source
distribution.
*/

using System;
using System.Collections.Generic;
using System.Text;


namespace TiXml
{
	//const int TIXML_MAJOR_VERSION = 2;
	//const int TIXML_MINOR_VERSION = 5;
	//const int TIXML_PATCH_VERSION = 3;

	// Only used by Attribute::Query functions
	public enum TiXmlQueryResult
	{
		TIXML_SUCCESS,
		TIXML_NO_ATTRIBUTE,
		TIXML_WRONG_TYPE
	};

	// Used by the parsing routines.
	public static class TiXmlEncoding
	{
		public const int TIXML_ENCODING_UNKNOWN = 0;
		public const int TIXML_ENCODING_UTF8 = 0;
		public const int TIXML_ENCODING_LEGACY = 0;
	};

#if UNUSED
const TiXmlEncoding TIXML_DEFAULT_ENCODING = TIXML_ENCODING_UNKNOWN;
#endif
}
