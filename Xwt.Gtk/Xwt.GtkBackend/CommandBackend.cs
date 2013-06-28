// 
// CommandBackend.cs
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
using Xwt.Backends;

namespace Xwt.GtkBackend
{

	/// <summary>
	/// Gtk implementation of CommandBackend
	/// </summary>
	public class CommandBackend : Xwt.Backends.CommandBackend
	{
		Gtk.Action action;

		public override void Initalize (ICommandEventSink eventSink)
		{
			base.Initalize (eventSink);

			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			var frontendCommand = backendHost.Parent;

			string stockId = null;

			if (frontendCommand.IsGlobalCommand) {
				switch (frontendCommand.GlobalCommand.Value) {
					case GlobalCommand.Import:
					case GlobalCommand.Export:
						break;
					// TODO: check for mismatches and missing items or manually check all cases.
					default:
						var gtkStockType = typeof(Gtk.Stock);
						var gtkStockProperty = gtkStockType.GetProperty (frontendCommand.GlobalCommand.Value.ToString ());
						stockId = gtkStockProperty.GetValue (null, null) as string;
						break;
				}
			}
			action = new Gtk.Action (frontendCommand.Id, frontendCommand.Label, null, stockId)
			{
				Tooltip = frontendCommand.Tooltip
			};
			Accelerator = frontendCommand.Accelerator;
		}

		public override IMenuItemBackend CreateMenuItem ()
		{
			var menuItem = (Gtk.MenuItem)action.CreateMenuItem ();
			return new MenuItemBackend (menuItem);
		}

		public override IButtonBackend CreateButton ()
		{
			var button = new Gtk.Button (action.StockId);
			if (action.StockId == null) {
				button.Name = action.Name;
				button.Label = action.Label;
				button.TooltipText = action.Tooltip;
				if (action.IconName != null) {
					var icon = action.CreateIcon (Gtk.IconSize.Button);
					button.Image = icon;
				}
			}
			return new ButtonBackend (button);
		}

		public Gtk.Action Action { get { return action; } }

		protected override void AddCommandActivatedHandler (EventHandler handler)
		{
			action.Activated += handler;
		}

		protected override void RemoveCommandActivatedHandler (EventHandler handler)
		{
			action.Activated -= handler;
		}

		public override string Label {
			get {
				return (action == null) ? base.Label : action.Label;
			}
			set {
				if (action == null) {
					base.Label = value;
				} else {
					action.Label = value;
				}
			}
		}

		public override string ToolTip {
			get {
				return (action == null) ? base.ToolTip : action.Tooltip;
			}
			set {
				if (action == null) {
					base.ToolTip = value;
				} else {
					action.Tooltip = value;
				}
			}
		}

		public override Accelerator Accelerator {
			get {
				return base.Accelerator;
			}
			set {
				if (action == null) {
					base.Accelerator = value;
				} else {
					Gtk.StockItem stockItem;
					if (Gtk.StockManager.LookupItem(action.StockId, out stockItem)) {
						var gtkKey = stockItem.Keyval;
						var gtkModifier = stockItem.Modifier;
						var modifer = ModifierKeys.None;
						if (gtkModifier.HasFlag (Gdk.ModifierType.ControlMask))
							modifer |= ModifierKeys.Control;
						if (gtkModifier.HasFlag (Gdk.ModifierType.ShiftMask))
							modifer |= ModifierKeys.Shift;
						if (gtkModifier.HasFlag (Gdk.ModifierType.Mod1Mask))
							modifer |= ModifierKeys.Alt;
						base.Accelerator = new Accelerator ((Key)gtkKey, modifer);
					} else {
						base.Accelerator = value;
					}
					GtkEngine.GlobalActionGroup.Remove (action);
					action.DisconnectAccelerator();
					string accelPath = null;
					// Most Commands with StockId will get accelerator without us generating it
					var needsAccelerator = Action.StockId == null || Action.StockId == Gtk.Stock.Print;
					if (needsAccelerator && value != null) {
						accelPath = ParseAccelerator(value);
					}
					if (needsAccelerator || Action.StockId != null) {
						GtkEngine.GlobalActionGroup.Add (action, accelPath);
						action.AccelGroup = GtkEngine.GlobalAccelGroup;
						action.ConnectAccelerator ();
					}
				}
			}
		}

		/// <summary>
		/// Converts Xwt Accelerator to Gtk AccelPath
		/// </summary>
		/// <param name="accelerator">The Xwt Accelerator</param>
		/// <returns>the Gtk AccelPath</returns>
		string ParseAccelerator(Accelerator accelerator)
		{
			var accelPath = string.Empty;
			if (accelerator.HasModifiers) {
				if (accelerator.Modifiers.HasFlag (ModifierKeys.Shift))
					accelPath += "<Shift>";
				if (accelerator.Modifiers.HasFlag (ModifierKeys.Alt))
					accelPath += "<Alt>";
				if (accelerator.Modifiers.HasFlag (ModifierKeys.Control))
					accelPath += "<Control>";
				if (accelerator.Modifiers.HasFlag (ModifierKeys.Command))
					accelPath += "<Primary>";
				accelPath += (char)accelerator.Key;
			}
			return accelPath;
		}
	}
}
