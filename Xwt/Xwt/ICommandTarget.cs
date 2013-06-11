// 
// ICommandTarget.cs
//  
// Author:
//       David Lechner <david@lechnology.com>
// 
// Copyright (c) 2011 Xamarin Inc
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

namespace Xwt
{
	/// <summary>
	/// Represents and object that can be the target of a <see cref="Command"/>
	/// </summary>
	/// <remarks>
	/// In order for accelerator key bindings to work for <see cref="Command"/>s,
	/// the Command must be connected to a target.
	/// </remarks>
	/// <example>
	/// // Window implements ICommandTarget.
	/// var MainWindow = new Window();
	/// 
	/// var Command command = new Command();
	/// 
	/// // connecting the command to the target can be done two ways.
	/// command.Target = MainWindow;
	/// // and
	/// MainWindow.Commands.Add(command);
	/// // both have the same effect, so only one of the above statements is required
	/// 
	/// </example>
	public interface ICommandTarget
	{
		/// <summary>
		/// Collection of <see cref="Command"/>s that target this object.
		/// </summary>
		CommandCollection Commands { get; }
	}
}
