//
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
using Xwt;

namespace Xwt.Commands
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
			[Label ("_About {0}", DesktopType.All & ~DesktopType.Gnome)]
			[Label ("_About", DesktopType.Gnome)]
			[CocoaAction ("orderFrontStandardAboutPanel:")]
			[GtkStockId ("gtk-about")]
			About,

			[Label ("Hide {0}", DesktopType.Mac)]
			[DefaultKeyboardShortcut (Key.h, ModifierKeys.Primary, DesktopType.Mac)]
			[CocoaAction ("hide:")]
			Hide,

			[Label ("Hide Others", DesktopType.Mac)]
			[DefaultKeyboardShortcut (Key.h, ModifierKeys.Primary | ModifierKeys.Alt, DesktopType.Mac)]
			[CocoaAction ("hideOtherApplications:")]
			HideOthers,

			[Label ("_Preferences", DesktopType.All & ~DesktopType.Windows & ~DesktopType.Mac & ~DesktopType.Kde)]
			[Label ("Preferences\u2026", DesktopType.Mac)]
			[Label ("_Options\u2026", DesktopType.Windows)]
			[Label ("Configure {0}", DesktopType.Kde)]
			[DefaultKeyboardShortcut (Key.Comma, ModifierKeys.Primary, DesktopType.Mac)]
			[GtkStockId ("gtk-preferences")]
			Preferences,

			[Label ("_Quit", DesktopType.All & ~DesktopType.Mac & ~DesktopType.Windows)]
			[Label ("Quit {0}", DesktopType.Mac)]
			[Label ("E_xit {0}", DesktopType.Windows)]
			[DefaultKeyboardShortcut (Key.q, ModifierKeys.Primary, DesktopType.All & ~DesktopType.Windows)]
			[CocoaAction ("terminate:")]
			[GtkStockId ("gtk-quit")]
			Quit,

			[Label ("Show All", DesktopType.Mac)]
			[CocoaAction ("unhideAllApplications:")]
			UnhideAll,
		}

		/// <summary>File commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current file (document).
		/// They are usually found in the File menu.
		/// </remarks>
		public enum File
		{
			[Label ("_Close", DesktopType.All & ~DesktopType.Kde)]
			[Label ("_Close File", DesktopType.Kde)]
			[DefaultKeyboardShortcut (Key.F4, ModifierKeys.Primary, DesktopType.Windows)]
			[DefaultKeyboardShortcut (Key.W, ModifierKeys.Primary, DesktopType.All & ~DesktopType.Windows)]
			[CocoaAction ("close:")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Close")]
			[GtkStockId ("gtk-close")]
			Close,

			[Label ("Close _All", DesktopType.All & ~DesktopType.Kde & ~DesktopType.Windows)]
			[Label ("Close _All Files", DesktopType.Kde)]
			[Label ("Close _all", DesktopType.Windows)]
			[DefaultKeyboardShortcut (Key.W, ModifierKeys.Primary | ModifierKeys.Alt, DesktopType.Mac)]
			CloseAll,

			[Label ("_Duplicate", DesktopType.All)]
			Duplicate,

			[Label ("_Export\u2026", DesktopType.All & ~DesktopType.Mac)]
			[Label ("Export As\u2026", DesktopType.Mac)]
			Export,

			[Label ("_Import\u2026", DesktopType.All)]
			Import,

			[Label ("_New", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.N, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("new:")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.New")]
			New,

			[Label ("_Open\u2026", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.O, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("open:")]
			[GtkStockId ("gtk-open")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Open")]
			Open,

			[Label ("Page Set_up", DesktopType.All & ~DesktopType.Mac & ~DesktopType.Windows)]
			[Label ("Page set_up", DesktopType.Windows)]
			[Label ("Page Setup\u2026", DesktopType.Mac)]
			[DefaultKeyboardShortcut (Key.P, ModifierKeys.Primary | ModifierKeys.Shift, DesktopType.Mac)]
			PageSetup,

			[Label ("_Print\u2026", DesktopType.All & ~DesktopType.Kde)]
			[Label ("_Print", DesktopType.Kde)]
			[DefaultKeyboardShortcut (Key.P, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("print:")]
			[GtkStockId ("gtk-print")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Print")]
			Print,

			[Label ("Print Preview", DesktopType.All & ~DesktopType.Windows)]
			[Label ("Print preview", DesktopType.Windows)]
			[DefaultKeyboardShortcut (Key.P, ModifierKeys.Primary | ModifierKeys.Shift, DesktopType.Gnome)]
			[GtkStockId ("gtk-print-preview")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.PrintPreview")]
			PrintPreview,

			[Label ("_Revert", DesktopType.All & ~DesktopType.Mac)]
			[Label ("Revert to Saved", DesktopType.Mac)]
			[CocoaAction ("revert:")]
			[GtkStockId ("gtk-revert-to-saved")]
			Revert,

			[Label ("_Save", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.S, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("save:")]
			[GtkStockId ("gtk-save")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Save")]
			Save,

			[Label ("Save A_ll", DesktopType.All & ~DesktopType.Windows)]
			[Label ("Save a_ll", DesktopType.Windows)]
			SaveAll,

			[Label ("Save _As\u2026", DesktopType.All & ~DesktopType.Windows)]
			[Label ("Save _as\u2026", DesktopType.Windows)]
			[DefaultKeyboardShortcut (Key.S, ModifierKeys.Primary | ModifierKeys.Shift, DesktopType.All)]
			[GtkStockId ("gtk-save-as")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.SaveAs")]
			SaveAs,
		}

		/// <summary>Edit commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current selection.
		/// They are usually found in the Edit menu.
		/// </remarks>
		public enum Edit
		{	
			[Label ("_Copy", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.C, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("copy:")]
			[GtkStockId ("gtk-copy")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Copy")]
			Copy,

			[Label ("C_ut", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.X, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("cut:")]
			[GtkStockId ("gtk-cut")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Cut")]
			Cut,

			[Label ("_Delete", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.Delete, DesktopType.All)]
			[CocoaAction ("delete:")]
			[GtkStockId ("gtk-delete")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Delete")]
			Delete,

			[Label ("Delect All", DesktopType.All & ~DesktopType.Windows)]
			[Label ("Select _none", DesktopType.Windows)]
			[DefaultKeyboardShortcut (Key.A, ModifierKeys.Primary | ModifierKeys.Shift, DesktopType.All)]
			DeselectAll,

			[Label ("_Find\u2026", DesktopType.All & ~DesktopType.Mac)]
			[DefaultKeyboardShortcut (Key.F, ModifierKeys.Primary, DesktopType.All)]
			[GtkStockId ("gtk-find")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Find")]
			Find,

			[Label ("Find _Next", DesktopType.All & ~DesktopType.Windows)]
			[Label ("Find _next", DesktopType.Windows)]
			[DefaultKeyboardShortcut (Key.F3, DesktopType.Windows | DesktopType.Kde)]
			[DefaultKeyboardShortcut (Key.G, ModifierKeys.Primary, DesktopType.Mac | DesktopType.Gnome)]
			[CocoaAction ("performTextFinderAction:")]
			FindNext,

			[Label ("Find Pre_vious", DesktopType.All & ~DesktopType.Windows)]
			[Label ("Find pre_vious", DesktopType.Windows)]
			[DefaultKeyboardShortcut (Key.F3, ModifierKeys.Shift, DesktopType.Windows | DesktopType.Kde)]
			[DefaultKeyboardShortcut (Key.G, ModifierKeys.Primary | ModifierKeys.Shift, DesktopType.Mac | DesktopType.Gnome)]
			FindPrevious,

			[Label ("_Paste", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.V, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("paste:")]
			[GtkStockId ("gtk-paste")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Paste")]
			Paste,

			[Label ("Paste as Text", DesktopType.All & ~DesktopType.Windows)]
			[Label ("Paste as text", DesktopType.Windows)]
			[DefaultKeyboardShortcut (Key.V, ModifierKeys.Primary | ModifierKeys.Shift, DesktopType.All)]
			[CocoaAction ("pasteAsPlainText:")]
			PasteAsText,

			[Label ("_Redo", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.Y, ModifierKeys.Primary, DesktopType.Windows)]
			[DefaultKeyboardShortcut (Key.Z, ModifierKeys.Primary | ModifierKeys.Shift, DesktopType.All & ~DesktopType.Windows)]
			[CocoaAction ("redo:")]
			[GtkStockId ("gtk-redo")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Redo")]
			Redo,

			[DefaultKeyboardShortcut (Key.H, ModifierKeys.Primary, DesktopType.Windows | DesktopType.Gnome)]
			[DefaultKeyboardShortcut (Key.R, ModifierKeys.Primary, DesktopType.Kde)]
			[DefaultKeyboardShortcut (Key.F, ModifierKeys.Primary | ModifierKeys.Alt, DesktopType.Mac)]
			[CocoaAction ("replace:")]
			[GtkStockId ("gtk-find-and-replace")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Replace")]
			Replace,

			[Label ("Select _All", DesktopType.All & ~DesktopType.Windows)]
			[Label ("Select _all", DesktopType.Windows)]
			[DefaultKeyboardShortcut (Key.A, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("selectAll:")]
			SelectAll,

			[Label ("_Undo", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.Z, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("undo:")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Undo")]
			Undo,
		}

		/// <summary>Format commands</summary>
		/// <remarks>
		/// These are commands that perform formatting actions usually on text.
		/// They are usually found in the Format menu.
		/// </remarks>
		public enum Format
		{
			[GtkStockId ("gtk-justify-center")]
			AlignCenter,
			
			[GtkStockId ("gtk-justify-fill")]
			AlignJustified,
			
			[GtkStockId ("gtk-justify-left")]
			AlignLeft,
			
			[GtkStockId ("gtk-justify-right")]
			AlignRight,

			[Label ("_Bold", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.B, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("addFontTrait:")]
			[GtkStockId ("gtk-bold")]
			[WindowsCommand ("System.Windows.Input.EditingCommands.ToggleBold")]
			Bold,

			DecreaseFontSize,
			
			[GtkStockId ("gtk-unindent")]
			DecreaseIndent,

			[Label ("_Italic", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.I, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("addFontTrait:")]
			[GtkStockId ("gtk-italic")]
			[WindowsCommand ("System.Windows.Input.EditingCommands.ToggleItalic")]
			Italic,

			IncreaseFontSize,
			
			[GtkStockId ("gtk-indent")]
			IncreaseIndent,
			
			[CocoaAction ("orderFrontFontPanel:")]
			[GtkStockId ("gtk-select-font")]
			ShowFontDialog,
			
			[CocoaAction ("orderFrontColorPanel:")]
			[GtkStockId ("gtk-color-picker")]
			ShowColorPicker,
			
			[GtkStockId ("gtk-strikethrough")]
			Strikethrough,

			[Label ("_Underline", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.U, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("addFontTrait:")]
			[GtkStockId ("gtk-underline")]
			[WindowsCommand ("System.Windows.Input.EditingCommands.ToggleUnderline")]
			Underline,
		}

		/// <summary>View commands</summary>
		/// <remarks>
		/// These are commands that ...
		/// They are usually found in the View menu.
		/// </remarks>
		public enum View
		{
			[DefaultKeyboardShortcut (Key.F5, DesktopType.All)]
			[CocoaAction ("reload:")]
			[GtkStockId ("gtk-refresh")]
			[WindowsCommand ("System.Windows.Input.NavigationCommands.Refresh")]
			Refresh,

			[DefaultKeyboardShortcut (Key.K0, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("zoomImageToActualSize:")]
			[GtkStockId ("gtk-100")]
			Zoom100,
			
			[CocoaAction ("zoomImageToFit:")]
			[GtkStockId ("gtk-fit")]
			ZoomFit,

			[DefaultKeyboardShortcut (Key.Plus, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("zoomIn:")]
			[GtkStockId ("gtk-in")]
			ZoomIn,

			[DefaultKeyboardShortcut (Key.Minus, ModifierKeys.Primary, DesktopType.All)]
			[CocoaAction ("zoomOut:")]
			[GtkStockId ("gtk-out")]
			ZoomOut
		}

		/// <summary>Window commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current window.
		/// They are usually found in the Window menu.
		/// </remarks>
		public enum Window
		{
			[Label ("Zoom", DesktopType.Mac)]
			[CocoaAction ("performZoom:")]
			Maximize,

			[DefaultKeyboardShortcut (Key.M, ModifierKeys.Primary, DesktopType.Mac)]
			[CocoaAction ("performMiniaturize:")]
			Minimize,
		}


		/// <summary>Dialog commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current dialog.
		/// They are usually represented as buttons in a dialog box.
		/// </remarks>
		public enum Dialog
		{
			[Label ("_Apply", DesktopType.All)]
			[GtkStockId ("gtk-apply")]
			Apply,

			[Label ("_Back", DesktopType.All)]
			Back,

			[Label ("_Cancel", DesktopType.All)]
			[GtkStockId ("gtk-cancel")]
			Cancel,

			[Label ("_Next", DesktopType.All)]
			Next,

			[Label ("_No", DesktopType.All)]
			[GtkStockId ("gtk-no")]
			No,

			[Label ("_OK", DesktopType.All)]
			[GtkStockId ("gtk-ok")]
			Ok,

			[Label ("_Yes", DesktopType.All)]
			[GtkStockId ("gtk-yes")]
			Yes,
		}

		/// <summary>Miscellaneous commands</summary>
		/// <remarks>
		/// These are commands that perform miscellaneous actions.
		/// </remarks>
		public enum Misc
		{
			[Label ("_Add", DesktopType.All)]
			[GtkStockId ("gtk-add")]
			Add,

			[Label ("{0} Help", DesktopType.All & ~DesktopType.Gnome & ~DesktopType.Kde)]
			[Label ("_Contents", DesktopType.Gnome)]
			[Label ("{0} _Handbook", DesktopType.Kde)]
			[DefaultKeyboardShortcut (Key.Question, ModifierKeys.Primary, DesktopType.Mac)]
			[DefaultKeyboardShortcut (Key.F1, DesktopType.All & ~DesktopType.Mac)]
			[CocoaAction ("showHelp:")]
			[GtkStockId ("gtk-help")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Help")]
			Help,

			[Label ("P_roperties", DesktopType.All)]
			[DefaultKeyboardShortcut (Key.I, ModifierKeys.Primary | ModifierKeys.Alt, DesktopType.Mac)]
			[DefaultKeyboardShortcut (Key.Return, ModifierKeys.Alt, DesktopType.All & ~DesktopType.Mac)]
			[GtkStockId ("gtk-properties")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Properties")]
			Properties,

			[Label ("_Remove", DesktopType.All)]
			[GtkStockId ("gtk-remove")]
			Remove,

			[Label ("_Stop", DesktopType.All)]
			[CocoaAction ("stop:")]
			[GtkStockId ("gtk-stop")]
			[WindowsCommand ("System.Windows.Input.ApplicationCommands.Stop")]
			Stop,
		}
	}
}
