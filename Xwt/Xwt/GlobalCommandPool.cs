// 
// GlobalCommandPool.cs
//  
// Author:
//       David Lechner <david@lechnology.com>
// 
// Copyright (c) 2013 David Lechner
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;

namespace Xwt
{
	public static class GlobalCommandPool
	{
		static Dictionary<string, Command> commands;

		static GlobalCommandPool ()
		{
			commands = new Dictionary<string, Command> ();
		}

		/// <summary>
		/// Gets the global instance of a command.
		/// </summary>
		/// <returns>The command.</returns>
		/// <param name="command">Command name.</param>
		public static Command GetGlobalInstance(this string commandName)
		{
			if (!commands.ContainsKey (commandName)) {
				commands.Add (commandName, new Command (commandName));
			}
			return commands[commandName];
		}

		/// <summary>
		/// Gets the global instance of a stock command.
		/// </summary>
		/// <returns>The global command instance.</returns>
		/// <param name="command">Stock command.</param>
		public static Command GetGlobalInstance(this StockCommand command)
		{
			var commandName = command.ToString ();
			if (!commands.ContainsKey (commandName)) {
				commands.Add (commandName, new Command (command));
			}
			return commands[commandName];
		}
	}
}
