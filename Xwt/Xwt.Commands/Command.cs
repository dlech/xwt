// 
// Command.cs
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
using System.Collections.Generic;
using Xwt.Drawing;
using Xwt.Backends;

namespace Xwt.Commands
{
	[BackendType(typeof(ICommandBackend))]
	public class Command : XwtComponent
	{
		static Dictionary<string, Command> commands;

		protected class CommandBackendHost : BackendHost<Command, ICommandBackend>, ICommandEventSink
		{
			protected override void OnBackendCreated ()
			{
				base.OnBackendCreated ();
				Backend.Initalize (this);
			}

			public void OnLabelChanged ()
			{ 
				throw new NotImplementedException ();
			}
			
			public void OnAcceleratorChanged ()
			{ 
				throw new NotImplementedException ();
			}
			
			public void OnIconChanged ()
			{ 
				throw new NotImplementedException ();
			}

			public void OnSensitiveChanged ()
			{
				throw new NotImplementedException ();
			}

			public void OnVisibleChanged ()
			{
				throw new NotImplementedException ();
			}
		}

		protected override Xwt.Backends.BackendHost CreateBackendHost ()
		{
			return new CommandBackendHost ();
		}

		static Command ()
		{
			commands = new Dictionary<string, Command> ();
		}

		Command (string id)
		{
			Id = id;
		}

		Command (string id, string label)
			: this (id)
		{
			Label = label;
		}

		Command (string id, string label, KeyboardShortcutSequence shortcut)
			: this(id, label)
		{
			DefaultKeyboardShortcut = shortcut;
		}

		Command (string id, string label, KeyboardShortcutSequence shortcut, Image icon)
			: this(id, label, shortcut)
		{
			Icon = icon;
		}

		/// <summary>
		/// Gets the global instance of a command with the specified id.
		/// </summary>
		/// <returns>The command.</returns>
		/// <param name="id">Identifier.</param>
		public static Command GetCommandForId(string id)
		{
			if (!commands.ContainsKey (id)) {
				commands.Add (id, new Command (id));
			}
			return commands[id];
		}

		/// <summary>
		/// Gets the global instance of a stock command.
		/// </summary>
		/// <returns>The command.</returns>
		/// <param name="id">Stock command identifier.</param>
		public static Command GetCommandForId(Enum id)
		{
			return GetCommandForId (id.GetType().ToString () + "." + id.ToString ());
		}

		public string Id { get; private set; }

		public string ShortId 
		{
			get
			{
				return Id.Substring (Id.LastIndexOf ('.') + 1);
			}
		}

		public string Label {
			get { return Backend.Label; } 
			set { Backend.Label = value; }
		}

		public string Tooltip {
			get { return Backend.ToolTip; } 
			set { Backend.ToolTip = value; }
		}

		public Image Icon { get; internal set; }

		public KeyboardShortcutSequence DefaultKeyboardShortcut
		{
			get { return Backend.DefaultKeyboardShortcut; }
			set { Backend.DefaultKeyboardShortcut = value; }
		}

		public bool Sensitive { get; set; }		

		public bool Visible { get; set; }

		ICommandBackend Backend
		{
			get { return (ICommandBackend)base.BackendHost.Backend; }
		}

		public MenuItem CreateMenuItem ()
		{
			var menuItemBackend = Backend.CreateMenuItem ();
			return Toolkit.CurrentEngine.Backend.CreateFrontend<MenuItem> (menuItemBackend);
		}

		public Button CreateButton ()
		{
			var buttonBackend = Backend.CreateButton ();
			return Toolkit.CurrentEngine.Backend.CreateFrontend<Button> (buttonBackend);
		}

		/// <summary>Stock Application commands</summary>
		/// <remarks>
		/// These are commands that affect the application as a whole.
		/// They are usually found on the App menu on Mac and scattered elsewhere
		/// on other platforms.
		/// </remarks>
		public static class App
		{
			public static Command About { get { return GetCommandForId(StockCommands.App.About); } }
			public static Command Hide { get { return GetCommandForId(StockCommands.App.Hide); } }
			public static Command HideOthers { get { return GetCommandForId(StockCommands.App.HideOthers); } }
			public static Command Preferences { get { return GetCommandForId(StockCommands.App.Preferences); } }
			public static Command Quit { get { return GetCommandForId(StockCommands.App.Quit); } }
			public static Command UnhideAll { get { return GetCommandForId(StockCommands.App.UnhideAll); } }
		}

		/// <summary>Stock File commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current file (document).
		/// They are usually found in the File menu.
		/// </remarks>
		public static class File
		{
			public static Command Close { get { return GetCommandForId(StockCommands.File.Close); } }
			public static Command CloseAll { get { return GetCommandForId(StockCommands.File.CloseAll); } }
			public static Command Duplicate { get { return GetCommandForId(StockCommands.File.Duplicate); } }
			public static Command Export { get { return GetCommandForId(StockCommands.File.Export); } }
			public static Command Import { get { return GetCommandForId(StockCommands.File.Import); } }
			public static Command New { get { return GetCommandForId(StockCommands.File.New); } }
			public static Command Open { get { return GetCommandForId(StockCommands.File.Open); } }
			public static Command PageSetup { get { return GetCommandForId(StockCommands.File.PageSetup); } }
			public static Command Print { get { return GetCommandForId(StockCommands.File.Print); } }
			public static Command PrintPreview { get { return GetCommandForId(StockCommands.File.PrintPreview); } }
			public static Command Revert { get { return GetCommandForId(StockCommands.File.Revert); } }
			public static Command Save { get { return GetCommandForId(StockCommands.File.Save); } }
			public static Command SaveAll { get { return GetCommandForId(StockCommands.File.SaveAll); } }
			public static Command SaveAs { get { return GetCommandForId(StockCommands.File.SaveAs); } }
		}

		/// <summary>Stock Edit commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current selection.
		/// They are usually found in the Edit menu.
		/// </remarks>
		public static class Edit
		{
			public static Command Copy { get { return GetCommandForId(StockCommands.Edit.Copy); } }
			public static Command Cut { get { return GetCommandForId(StockCommands.Edit.Cut); } }
			public static Command Delete { get { return GetCommandForId(StockCommands.Edit.Delete); } }
			public static Command DeselectAll { get { return GetCommandForId(StockCommands.Edit.DeselectAll); } }
			public static Command Find { get { return GetCommandForId(StockCommands.Edit.Find); } }
			public static Command FindNext { get { return GetCommandForId(StockCommands.Edit.FindNext); } }
			public static Command FindPrevious { get { return GetCommandForId(StockCommands.Edit.FindPrevious); } }
			public static Command Paste { get { return GetCommandForId(StockCommands.Edit.Paste); } }
			public static Command PasteAsText { get { return GetCommandForId(StockCommands.Edit.PasteAsText); } }
			public static Command Redo { get { return GetCommandForId(StockCommands.Edit.Redo); } }
			public static Command Replace { get { return GetCommandForId(StockCommands.Edit.Replace); } }
			public static Command SelectAll { get { return GetCommandForId(StockCommands.Edit.SelectAll); } }
			public static Command Undo { get { return GetCommandForId(StockCommands.Edit.Undo); } }
		}

		/// <summary>Stock Window commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current window.
		/// They are usually found in the Window menu.
		/// </remarks>
		public static class Window
		{
			public static Command Maximize { get { return GetCommandForId(StockCommands.Window.Maximize); } }
			public static Command Minimize { get { return GetCommandForId(StockCommands.Window.Minimize); } }
		}

		/// <summary>Stock Dialog commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current dialog.
		/// They are usually represented as buttons in a dialog box.
		/// </remarks>
		public static class Dialog
		{
			public static Command Apply { get { return GetCommandForId(StockCommands.Dialog.Apply); } }
			public static Command Cancel { get { return GetCommandForId(StockCommands.Dialog.Cancel); } }
			public static Command No { get { return GetCommandForId(StockCommands.Dialog.No); } }
			public static Command Ok { get { return GetCommandForId(StockCommands.Dialog.Ok); } }
			public static Command Yes { get { return GetCommandForId(StockCommands.Dialog.Yes); } }
		}

		/// <summary>Miscellaneous stock commands</summary>
		/// <remarks>
		/// These are commands that perform miscellaneous actions.
		/// </remarks>
		public static class Misc
		{
			public static Command Add { get { return GetCommandForId(StockCommands.Misc.Add); } }
			public static Command Help { get { return GetCommandForId(StockCommands.Misc.Help); } }
			public static Command Properties { get { return GetCommandForId(StockCommands.Misc.Properties); } }
			public static Command Remove { get { return GetCommandForId(StockCommands.Misc.Remove); } }
			public static Command Stop { get { return GetCommandForId(StockCommands.Misc.Stop); } }
		}
	}
}

