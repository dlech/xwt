//
// OperatingSystemType.cs
//
// Author:
//       Lluis Sanchez <lluis@xamarin.com>
//
// Copyright (c) 2013 Xamarin Inc.
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
	/// Specifies which type of desktop environment is being used.
	/// </summary>
	[Flags]
	public enum DesktopType : int
	{
		/// <summary>
		/// GNU Gnome desktop environment.
		/// </summary>
		Gnome = 1,

		/// <summary>
		/// KDE desktop environment
		/// </summary>
		Kde = 1 << 1,

		/// <summary>
		/// Apple Macintosh desktop environment
		/// </summary>
		Mac = 1 << 2,

		/// <summary>
		/// Ubuntu Unity desktop environemnt
		/// </summary>
		Unity = 1 << 3 | Gnome,

		/// <summary>
		/// Microsoft Windows desktop environment
		/// </summary>
		Windows = 1 << 4,

		/// <summary>
		/// All desktop environments
		/// </summary>
		All = 0xFFFFFFF
	}
}

