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
			action = new Gtk.Action (frontendCommand.Id, frontendCommand.Label);
		}

		public override void InitializeBackend (object frontend, Backends.ApplicationContext context)
		{
			// TODO: anything needed here?
		}

		public Gtk.Action Action { get { return action; } }

	}
}
