﻿///
// CommandHandlerAttribute.cs
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
using System.Linq;
using System.Text;
using Xwt.Backends;

namespace Xwt.Commands
{


	/// <summary>
	/// Default implementation of <see cref="ICommandBackend"/>
	/// </summary>
	public abstract class CommandBackend : ICommandBackend
	{
		ICommandEventSink eventSink;
		ApplicationContext context;

		public virtual void Initalize (ICommandEventSink eventSink)
		{
			this.eventSink = eventSink;

			// TODO: this typecast is a hack to access other objects
			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			var command = backendHost.Parent;

			// IMPORTANT: Capital letter for accelerator key implies shift modifier
			switch (command.Id) {
			case "Xwt.StockCommands.App.About":
				Label = "_About";
				break;
			case "Xwt.StockCommands.Misc.Add":
				Label = "_Add";
				break;
			case "Xwt.StockCommands.Dialog.Apply":
				Label = "_Apply";
				break;
			case "Xwt.StockCommands.Dialog.Cancel":
				Label = "_Cancel";
				break;
			case "Xwt.StockCommands.Edit.Clear":
				Label = "_Clear";
				break;
			case "Xwt.StockCommands.File.Close":
				Label = "_Close";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.w, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.File.CloseAll":
				Label = "_Close All";
				break;
			case "Xwt.StockCommands.Edit.Copy":
				Label = "_Copy";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.c, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.Edit.Cut":
				Label = "Cu_t";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.x, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.Edit.Delete":
				Label = "_Delete";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.Delete);
				break;
			case "Xwt.StockCommands.Edit.DeselectAll":
				Label = "Deselect All";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.A, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.File.Export":
				Label = "_Export\u2026";
				break;
			case "Xwt.StockCommands.Edit.Find":
				Label = "_Find\u2026";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.f, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.Edit.FindNext":
				Label = "Find Ne_xt";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.g, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.Edit.FindPrevious":
				Label = "Find Previous";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.G, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.Misc.Help":
				Label = "_Help";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.F1);
				break;
			case "Xwt.StockCommands.File.Import":
				Label = "_Import\u2026";
				break;
			case "Xwt.StockCommands.Window.Maximize":
				Label = "Ma_ximize";
				break;
			case "Xwt.StockCommands.Window.Minimize":
				Label = "Mi_nimize";
				break;
			case "Xwt.StockCommands.File.New":
				Label = "_New\u2026";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.n, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.Dialog.No":
				Label = "_No";
				break;
			case "Xwt.StockCommands.Dialog.Ok":
				Label = "_OK";
				break;
			case "Xwt.StockCommands.File.Open":
				Label = "_Open\u2026";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.o, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.File.PageSetup":
				Label = "Page Setup\u2026";
				break;
			case "Xwt.StockCommands.Edit.Paste":
				Label = "_Paste";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.v, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.Edit.PasteAsText":
				Label = "Paste As Text\u2026";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.V, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.File.PrintPreview":
				Label = "Print Preview\u2026";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.P, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.File.Print":
				Label = "Print\u2026";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.p, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.App.Preferences":
				Label = "Preferences";
				break;
			case "Xwt.StockCommands.Misc.Properties":
				Label = "Pr_operties\u2026";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.Return, ModifierKeys.Alt);
				break;
			case "Xwt.StockCommands.App.Quit":
				Label = "_Quit";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.q, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.Edit.Redo":
				Label = "_Redo";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.Z, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.Misc.Remove":
				Label = "_Remove";
				break;
			case "Xwt.StockCommands.Edit.Replace":
				Label = "_Replace\u2026";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.h, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.File.Revert":
				Label = "Revert";
				break;
			case "Xwt.StockCommands.File.Save":
				Label = "_Save";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.s, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.File.SaveAll":
				Label = "Save All";
				break;
			case "Xwt.StockCommands.File.SaveAs":
				Label = "Save _As\u2026";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.N, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.File.SaveCopy":
				Label = "Save a Copy\u2026";
				break;
			case "Xwt.StockCommands.Edit.SelectAll":
				Label = "Select _All";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.a, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.Misc.Stop":
				Label = "_Stop";
				break;
			case "Xwt.StockCommands.Edit.Undo":
				Label = "_Undo";
				command.DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.z, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.Dialog.Yes":
				Label = "_Yes";
				break;
			default:
				Label = Label ?? command.ShortId;
				break;
			}
		}

		public virtual string Label { get; set; }

		public virtual string ToolTip { get; set; }

		public virtual KeyboardShortcutSequence DefaultKeyboardShortcut { get; set; }

		public virtual bool Sensitive { get; set; }

		public virtual bool Visible { get; set; }

		public abstract IMenuItemBackend CreateMenuItem ();
		public abstract IButtonBackend CreateButton ();

		public virtual void InitializeBackend(object frontend, ApplicationContext context)
		{
			this.context = context;
		}

		public virtual void EnableEvent(object eventId)
		{
		}

		public void DisableEvent(object eventId)
		{
		}
	}
}
