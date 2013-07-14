// 
// ModifierKeys.cs
//  
// Author:
//       Lluis Sanchez <lluis@xamarin.com>
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
	/// Modifier keys.
	/// </summary>
	[Flags]
	public enum ModifierKeys
	{
		/// <summary>
		/// No modifier keys.
		/// </summary>
		None = 0,

		/// <summary>
		/// The 'alternate' modifier key. Also called the 'option' key on Mac.
		/// </summary>
		Alt = 0x1 << 0,

		/// <summary>
		/// The primary modifier key. The 'command' key on Mac or
		/// the 'control' key on other platforms.
		/// </summary>
		Primary = 0x1 << 1,

		/// <summary>
		/// The secondary modifier key. The 'control' key on Mac,
		/// the 'windows' key on Windows or the 'meta' or 'super' key on other platforms.
		/// </summary>
		Secondary = 0x1 << 2,

		/// <summary>
		/// The 'shift' modifer key.
		/// </summary>
		Shift = 0x1 << 3
	}
}

