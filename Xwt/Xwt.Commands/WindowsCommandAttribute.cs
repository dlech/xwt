//
// WindowsCommandAttribute.cs
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
using Xwt;

namespace Xwt.Commands
{
	/// <summary>
	/// Specifies the name of a WPF command.
	/// </summary>
	/// <remarks>
	/// Names are from System.Windows.Input.MediaCommands, ApplicationCommands, NavigationCommands,
	/// ComponentCommands, or EditingCommands, including the namespace and a property.
	/// <example>[WindowsCommand("System.Windows.Input.ApplicationCommands.New")]</example>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class WindowsCommandAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Xwt.Commands.WindowsCommandAttribute"/> class.
		/// </summary>
		/// <param name="selector">Selector.</param>
		public WindowsCommandAttribute (string command)
		{
			Command = command;
		}

		public string Command { get; private set; }
	}
}

