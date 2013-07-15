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

		Command (Enum baseEnum)
		{
			BaseEnum = baseEnum;
		}

		Command (Enum baseEnum, string label)
			: this (baseEnum)
		{
			Label = label;
		}

		Command (Enum baseEnum, string label, KeyboardShortcutSequence shortcut)
			: this(baseEnum, label)
		{
			DefaultKeyboardShortcut = shortcut;
		}

		Command (Enum baseEnum, string label, KeyboardShortcutSequence shortcut, Image icon)
			: this(baseEnum, label, shortcut)
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
			var commandTypeName = id.Substring(0, id.LastIndexOf('.'));
			var commandType = Type.GetType (commandTypeName);
			var fieldName = id.Substring (commandTypeName.Length + 1);
			var commandField = commandType.GetField (fieldName);
			return GetCommandForEnum ((Enum)commandField.GetValue (null));
		}

		/// <summary>
		/// Gets the global instance of a stock command.
		/// </summary>
		/// <returns>The command.</returns>
		/// <param name="id">Stock command identifier.</param>
		public static Command GetCommandForEnum(Enum e)
		{
			var id = GetIdForEnum (e);
			if (!commands.ContainsKey (id)) {
				commands.Add (id, new Command (e));
			}
			return commands[id];
		}

		public Enum BaseEnum { get; private set; }

		public string Id
		{
			get
			{
				return GetIdForEnum (BaseEnum);
			}
		}


		public string ShortId 
		{
			get
			{
				return BaseEnum.ToString ();
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
			MenuItem menuItem = null;
			// if this is a List command, then return a MenuItem with a Submenu that will contain the list
			IterateAttributes<ListCommandAttribute>((attribute) =>
			{
				menuItem = new MenuItem (Label);
				menuItem.SubMenu = new Menu ();
				menuItem.Clicked += (sender, e) =>
				{
					menuItem.SubMenu = new Menu ();
					var listMenuItemBackend = Backend.CreateListMenuItem (0, "Name");
					var listMenuItem = Toolkit.CurrentEngine.Backend.CreateFrontend<MenuItem> (listMenuItemBackend);
					menuItem.SubMenu.Items.Add (listMenuItem);
				};
			});
			if (menuItem != null)
				return menuItem;

			// otherwise, just let the backend to the work
			var menuItemBackend = Backend.CreateMenuItem ();
			return Toolkit.CurrentEngine.Backend.CreateFrontend<MenuItem> (menuItemBackend);
		}

		public Button CreateButton ()
		{
			var buttonBackend = Backend.CreateButton ();
			return Toolkit.CurrentEngine.Backend.CreateFrontend<Button> (buttonBackend);
		}

		public void IterateAttributes<T>(Action<T> action)
		{
			var commandType = BaseEnum.GetType ();
			var commandField = commandType.GetField (ShortId);
			if (commandField == null)
				return;
			var attributeList = commandField.GetCustomAttributes (typeof(T), true);
			foreach (T shortcutAttribute in attributeList)
				action.Invoke (shortcutAttribute);
		}
		
		private static string GetIdForEnum(Enum e)
		{
			return e.GetType ().ToString () + "." + e.ToString ();
		}

		/// <summary>Stock Application commands</summary>
		/// <remarks>
		/// These are commands that affect the application as a whole.
		/// They are usually found on the App menu on Mac and scattered elsewhere
		/// on other platforms.
		/// </remarks>
		public static class App
		{
			public static Command About { get { return GetCommandForEnum(StockCommands.App.About); } }
			public static Command Hide { get { return GetCommandForEnum(StockCommands.App.Hide); } }
			public static Command HideOthers { get { return GetCommandForEnum(StockCommands.App.HideOthers); } }
			public static Command Preferences { get { return GetCommandForEnum(StockCommands.App.Preferences); } }
			public static Command Quit { get { return GetCommandForEnum(StockCommands.App.Quit); } }
			public static Command UnhideAll { get { return GetCommandForEnum(StockCommands.App.UnhideAll); } }
		}

		/// <summary>Stock File commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current file (document).
		/// They are usually found in the File menu.
		/// </remarks>
		public static class File
		{
			public static Command Close { get { return GetCommandForEnum(StockCommands.File.Close); } }
			public static Command CloseAll { get { return GetCommandForEnum(StockCommands.File.CloseAll); } }
			public static Command Duplicate { get { return GetCommandForEnum(StockCommands.File.Duplicate); } }
			public static Command Export { get { return GetCommandForEnum(StockCommands.File.Export); } }
			public static Command Import { get { return GetCommandForEnum(StockCommands.File.Import); } }
			public static Command New { get { return GetCommandForEnum(StockCommands.File.New); } }
			public static Command Open { get { return GetCommandForEnum(StockCommands.File.Open); } }
			public static Command PageSetup { get { return GetCommandForEnum(StockCommands.File.PageSetup); } }
			public static Command Print { get { return GetCommandForEnum(StockCommands.File.Print); } }
			public static Command PrintPreview { get { return GetCommandForEnum(StockCommands.File.PrintPreview); } }
			public static Command Revert { get { return GetCommandForEnum(StockCommands.File.Revert); } }
			public static Command Save { get { return GetCommandForEnum(StockCommands.File.Save); } }
			public static Command SaveAll { get { return GetCommandForEnum(StockCommands.File.SaveAll); } }
			public static Command SaveAs { get { return GetCommandForEnum(StockCommands.File.SaveAs); } }
		}

		/// <summary>Stock Edit commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current selection.
		/// They are usually found in the Edit menu.
		/// </remarks>
		public static class Edit
		{
			public static Command Copy { get { return GetCommandForEnum(StockCommands.Edit.Copy); } }
			public static Command Cut { get { return GetCommandForEnum(StockCommands.Edit.Cut); } }
			public static Command Delete { get { return GetCommandForEnum(StockCommands.Edit.Delete); } }
			public static Command DeselectAll { get { return GetCommandForEnum(StockCommands.Edit.DeselectAll); } }
			public static Command Find { get { return GetCommandForEnum(StockCommands.Edit.Find); } }
			public static Command FindNext { get { return GetCommandForEnum(StockCommands.Edit.FindNext); } }
			public static Command FindPrevious { get { return GetCommandForEnum(StockCommands.Edit.FindPrevious); } }
			public static Command Paste { get { return GetCommandForEnum(StockCommands.Edit.Paste); } }
			public static Command PasteAsText { get { return GetCommandForEnum(StockCommands.Edit.PasteAsText); } }
			public static Command Redo { get { return GetCommandForEnum(StockCommands.Edit.Redo); } }
			public static Command Replace { get { return GetCommandForEnum(StockCommands.Edit.Replace); } }
			public static Command SelectAll { get { return GetCommandForEnum(StockCommands.Edit.SelectAll); } }
			public static Command Undo { get { return GetCommandForEnum(StockCommands.Edit.Undo); } }
		}

		/// <summary>Stock Window commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current window.
		/// They are usually found in the Window menu.
		/// </remarks>
		public static class Window
		{
			public static Command Maximize { get { return GetCommandForEnum(StockCommands.Window.Maximize); } }
			public static Command Minimize { get { return GetCommandForEnum(StockCommands.Window.Minimize); } }
		}

		/// <summary>Stock Dialog commands</summary>
		/// <remarks>
		/// These are commands that perform actions on the current dialog.
		/// They are usually represented as buttons in a dialog box.
		/// </remarks>
		public static class Dialog
		{
			public static Command Apply { get { return GetCommandForEnum(StockCommands.Dialog.Apply); } }
			public static Command Cancel { get { return GetCommandForEnum(StockCommands.Dialog.Cancel); } }
			public static Command No { get { return GetCommandForEnum(StockCommands.Dialog.No); } }
			public static Command Ok { get { return GetCommandForEnum(StockCommands.Dialog.Ok); } }
			public static Command Yes { get { return GetCommandForEnum(StockCommands.Dialog.Yes); } }
		}

		/// <summary>Miscellaneous stock commands</summary>
		/// <remarks>
		/// These are commands that perform miscellaneous actions.
		/// </remarks>
		public static class Misc
		{
			public static Command Add { get { return GetCommandForEnum(StockCommands.Misc.Add); } }
			public static Command Help { get { return GetCommandForEnum(StockCommands.Misc.Help); } }
			public static Command Properties { get { return GetCommandForEnum(StockCommands.Misc.Properties); } }
			public static Command Remove { get { return GetCommandForEnum(StockCommands.Misc.Remove); } }
			public static Command Stop { get { return GetCommandForEnum(StockCommands.Misc.Stop); } }
		}
	}
}

