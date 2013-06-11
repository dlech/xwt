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

			if (frontendCommand.IsStockCommand) {
				switch (frontendCommand.StockCommand.Value) {
					// TODO: check for mismatches or manually check all cases.
					default:
						var gtkStockType = typeof(Gtk.Stock);
						var gtkStockProperty = gtkStockType.GetProperty (frontendCommand.StockCommand.Value.ToString ());
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

		public override IMenuBackend CreateMenu ()
		{
			var menu = (Gtk.Menu)action.CreateMenu ();
			return new MenuBackend (menu);
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
