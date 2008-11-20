#region Copyright (C) 2005-2007 Team MediaPortal

/* 
 *	Copyright (C) 2005-2007 Team MediaPortal
 *	http://www.team-mediaportal.com
 *
 *  This Program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2, or (at your option)
 *  any later version.
 *   
 *  This Program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *   
 *  You should have received a copy of the GNU General Public License
 *  along with GNU Make; see the file COPYING.  If not, write to
 *  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA. 
 *  http://www.gnu.org/copyleft/gpl.html
 *
 */

#endregion

using System;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MPTagThat.Core
{
  /// <summary>
  /// Generates string section and id from combo string 
  /// </summary>
  public class StringId
  {
    string _section;
    string _id;

    public StringId(string section, string id)
    {
      _section = section;
      _id = id;
    }

    public StringId(string skinLabel)
    {
      // Parse string example @mytv#10
      Regex label = new Regex("@(?<section>[a-z]+):(?<id>[a-z][0-9]+)");

      Match combineString = label.Match(skinLabel);

      if (combineString.Success)
      {
        _section = combineString.Groups["section"].Value;
        _id = combineString.Groups["id"].Value;
      }
      else
      {
        ServiceScope.Get<ILogger>().Error("String Manager - Invalid string Id: {0}", skinLabel);
        _section = "system";
        _id = "NotFound";
      }
    }

    public string Section
    {
      get { return _section; }
    }

    public string Id
    {
      get { return _id; }
    }
  }
}