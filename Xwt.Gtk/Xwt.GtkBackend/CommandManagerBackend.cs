// -----------------------------------------------------------------------
// <copyright file="CommandManagerBackend.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Xwt.GtkBackend
{
	using System;
	using System.Collections.Generic;
	using Xwt.Backends;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class CommandManagerBackend : ICommandManagerBackend
	{
		Gtk.UIManager uiManager;
		Gtk.ActionGroup actionGroup;

		class CommandCollectionListener : ICollectionListener
		{
			Gtk.ActionGroup actionGroup;

			public CommandCollectionListener (Gtk.ActionGroup actionGroup)
			{
				this.actionGroup = actionGroup;
			}

			public void ItemAdded (object collection, object item)
			{
				var command = item as Command;
				var commandBackend = command.GetBackend () as CommandBackend;
				string accelerator = null;
				if (command.Accelerator != null) {
					accelerator = string.Empty;
					if (command.Accelerator.HasModifiers) {
						if (command.Accelerator.Modifiers.Value.HasFlag (ModifierKeys.Control))
							accelerator += "<Primary>";
						accelerator += command.Accelerator.Key.ToString ().ToLower();
					}
				}
				actionGroup.Add (commandBackend.Action, accelerator);
			}

			public void ItemRemoved (object collection, object item)
			{
				var command = item as Command;
				var commandBackend = command.GetBackend () as CommandBackend;
				actionGroup.Remove (commandBackend.Action);
			}
		}

		public CommandManagerBackend ()
		{
			uiManager = new Gtk.UIManager ();
			actionGroup = new Gtk.ActionGroup ("OnlyOne");
			uiManager.InsertActionGroup (actionGroup, 0);
			Listener = new CommandCollectionListener (actionGroup);
		}

		public ICollectionListener Listener { get; private set; }

		public void InitializeBackend (object frontend, ApplicationContext context)
		{
			
		}

		public void EnableEvent (object eventId)
		{
			
		}

		public void DisableEvent (object eventId)
		{
			
		}
	}
}
