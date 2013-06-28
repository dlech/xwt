// -----------------------------------------------------------------------
// <copyright file="CommandBackend.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Xwt.GtkBackend
{
	using System;
	using Xwt.Backends;

	/// <summary>
	/// TODO: Update summary.
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
			action = new Gtk.Action (frontendCommand.Id, frontendCommand.Label, null, stockId);
			action.Tooltip = frontendCommand.Tooltip;

			string accelerator = null;
			// Most Commands with StockId will get accelerator without us generating it
			var needsAccelerator = Action.StockId == null || Action.StockId == Gtk.Stock.Print;
			if (needsAccelerator && frontendCommand.Accelerator != null) {
				accelerator = string.Empty;
				if (frontendCommand.Accelerator.HasModifiers) {
					if (frontendCommand.Accelerator.Modifiers.HasFlag (ModifierKeys.Shift))
						accelerator += "<Shift>";
					if (frontendCommand.Accelerator.Modifiers.HasFlag (ModifierKeys.Alt))
						accelerator += "<Alt>";
					if (frontendCommand.Accelerator.Modifiers.HasFlag (ModifierKeys.Control))
						accelerator += "<Control>";
					if (frontendCommand.Accelerator.Modifiers.HasFlag (ModifierKeys.Command))
						accelerator += "<Primary>";
					accelerator += (char)frontendCommand.Accelerator.Key;
				}
			}
			GtkEngine.GlobalActionGroup.Add (action, accelerator);
			action.AccelGroup = GtkEngine.GlobalAccelGroup;
			action.ConnectAccelerator ();
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
	}
}
