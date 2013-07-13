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

namespace Xwt
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
		public static Command GetCommandForId(StockCommandId id)
		{
			return GetCommandForId (id.ToString ());
		}

		public string Id { get; private set; }

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

		/// <summary>
		/// Gets the stock command.
		/// </summary>
		/// <value>The stock command or <see cref="StockCommandId.NotAStockCommand"/>
		/// if the command is not a stock command</value>
		/// <remarks>
		public StockCommandId StockCommand
		{
			get
			{
				var stockId = StockCommandId.NotAStockCommand;
				Enum.TryParse<StockCommandId> (Id, out stockId);
				return stockId;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is stock command.
		/// </summary>
		/// <value><c>true</c> if this instance is stock command; otherwise, <c>false</c>.</value>
		public bool IsStockCommand
		{
			get
			{
				return StockCommand != StockCommandId.NotAStockCommand;
			}
		}

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
	}
}

