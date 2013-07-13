//
// KeyboardShortcutSequence.cs
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
using System.Collections.ObjectModel;

namespace Xwt
{
	/// <summary>
	/// Represents a sequence of <see cref="KeyboardShortcut"/>s
	/// </summary>
	/// <remarks>
	/// Currenly only allows 1 or 2 <see cref="Xwt.KeyboardShortcut"/>s
	/// </remarks>
	public class KeyboardShortcutSequence : ReadOnlyCollection<KeyboardShortcut>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Xwt.KeyboardShortcutSequence"/> class
		/// with a single <see cref="Xwt.KeyboardShortcut"/>
		/// </summary>
		/// <param name="shortcut">The shortcut.</param>
		public KeyboardShortcutSequence (KeyboardShortcut shortcut) 
			: base (new KeyboardShortcut[] { shortcut })
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xwt.KeyboardShortcutSequence"/> class
		/// with two sequential <see cref="Xwt.KeyboardShortcut"/>s
		/// </summary>
		/// <param name="shortcut1">The first shortcut in the sequence.</param>
		/// <param name="shortcut2">The second shortcut in the sequence.</param>
		public KeyboardShortcutSequence (KeyboardShortcut shortcut1, KeyboardShortcut shortcut2)
			: base (new KeyboardShortcut[] { shortcut1, shortcut2 })
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xwt.KeyboardShortcutSequence"/> class
		/// with a single <see cref="Xwt.KeyboardShortcut"/>
		/// </summary>
		/// <param name="key">The shortcut key.</param>
		public KeyboardShortcutSequence (Key key) 
			: this (new KeyboardShortcut (key))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xwt.KeyboardShortcutSequence"/> class
		/// with a single <see cref="Xwt.KeyboardShortcut"/>
		/// </summary>
		/// <param name="key">The shortcut key.</param>
		/// <param name="modifiers">The shortcut key's modifiers.</param>
		public KeyboardShortcutSequence (Key key, ModifierKeys modifiers) 
			: this (new KeyboardShortcut (key, modifiers))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xwt.KeyboardShortcutSequence"/> class
		/// with two sequential <see cref="Xwt.KeyboardShortcut"/>s
		/// </summary>
		/// <param name="key1">The first shortcut key.</param>
		/// <param name="modifiers1">The first shortcut key's modifiers.</param>		
		/// <param name="key2">The second shortcut key.</param>
		public KeyboardShortcutSequence (Key key1, ModifierKeys modifiers1, Key key2) 
			: this (new KeyboardShortcut (key1, modifiers1), new KeyboardShortcut (key2))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xwt.KeyboardShortcutSequence"/> class
		/// with two sequential <see cref="Xwt.KeyboardShortcut"/>s
		/// </summary>
		/// <param name="key1">The first shortcut key.</param>
		/// <param name="modifiers1">The first shortcut key's modifiers.</param>		
		/// <param name="key2">The second shortcut key.</param>
		/// <param name="modifiers2">The second shortcut key's modifiers.</param>
		public KeyboardShortcutSequence (Key key1, ModifierKeys modifiers1, Key key2, ModifierKeys modifiers2) 
			: this (new KeyboardShortcut (key1, modifiers1), new KeyboardShortcut (key2, modifiers2))
		{
		}
	}
}

