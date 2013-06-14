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

	}
}
