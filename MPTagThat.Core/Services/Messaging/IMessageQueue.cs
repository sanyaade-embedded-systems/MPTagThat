﻿#region Copyright (C) 2009-2011 Team MediaPortal
// Copyright (C) 2009-2011 Team MediaPortal
// http://www.team-mediaportal.com
// 
// MPTagThat is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// MPTagThat is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with MPTagThat. If not, see <http://www.gnu.org/licenses/>.
#endregion
#region

using System.Collections.Generic;

#endregion

namespace MPTagThat.Core
{
  public delegate void MessageReceivedHandler(QueueMessage message);

  public interface IMessageQueue
  {
    /// <summary>
    ///   Gets a value indicating whether this queue has subscribers.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this queue has subscribers; otherwise, <c>false</c>.
    /// </value>
    bool HasSubscribers { get; }

    /// <summary>
    ///   Gets the message filters.
    /// </summary>
    /// <value>The message filters.</value>
    IList<IMessageFilter> Filters { get; }

    event MessageReceivedHandler OnMessageReceive;

    /// <summary>
    ///   Sends the specified message.
    /// </summary>
    /// <param name = "message">The message.</param>
    void Send(QueueMessage message);
  }
}