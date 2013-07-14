﻿//
// StockCommands.cs
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

namespace Xwt
{

	/// <summary>
	/// Enums of availible stock commands
	/// </summary>
	public static class StockCommands
	{
		/// <summary>Application commands</summary>
		/// <remarks>
		/// These are commands that affect the application as a whole.
		/// They are usually found on the App menu on Mac and scattered elsewhere
		/// on other platforms.
		/// </remarks>
		public enum App
		{
			About,
			[DefaultKeyboardShortcut(Key.h, ModifierKeys.Primary, DesktopType.Mac)]
			Hide,
			HideOthers,
			Preferences,
			Quit,
			UnhideAll,
		}

		/// <summary>File commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current file (document).
		/// They are usually found in the File menu.
		/// </remarks>
		public enum File
		{
			Close,
			CloseAll,
			Duplicate,
			Export,
			Import,
			New,
			Open,
			PageSetup,
			Print,
			PrintPreview,
			Revert,
			Save,
			SaveAll,
			SaveAs,
			SaveCopy,
		}

		/// <summary>Edit commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current selection.
		/// They are usually found in the Edit menu.
		/// </remarks>
		public enum Edit
		{
			Clear,
			Copy,
			Cut,
			Delete,
			DeselectAll,
			Find,
			FindNext,
			FindPrevious,
			Paste,
			PasteAsText,
			Redo,
			Replace,
			SelectAll,
			Undo,
		}

		/// <summary>Window commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current window.
		/// They are usually found in the Window menu.
		/// </remarks>
		public enum Window
		{
			Maximize,
			Minimize,
		}


		/// <summary>Dialog commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current dialog.
		/// They are usually represented as buttons in a dialog box.
		/// </remarks>
		public enum Dialog
		{
			Apply,
			Cancel,
			No,
			Ok,
			Yes,
		}

		/// <summary>Miscellaneous commands</summary>
		/// <remarks>
		/// These are commands that perform miscellaneous actions.
		/// </remarks>
		public enum Misc
		{
			Add,
			Help,
			Properties,
			Remove,
			Stop,
		}
	}
}
