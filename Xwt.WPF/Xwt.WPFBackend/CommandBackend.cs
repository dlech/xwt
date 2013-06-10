// -----------------------------------------------------------------------
// <copyright file="CommandBackend.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Xwt.WPFBackend
{
	using System;
	using Xwt.Backends;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class CommandBackend : Xwt.Backends.CommandBackend
	{
		public System.Windows.Input.ICommand Command { get; private set; }
		public System.Windows.Input.CommandBinding CommandBinding { get; private set; }

		public CommandBackend () : this (null) { }

		public CommandBackend (System.Windows.Input.ICommand command)
		{
			this.Command = command;
		}

		public override void Initalize (ICommandEventSink eventSink)
		{
			base.Initalize (eventSink);

			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			var frontendCommand = backendHost.Parent;
			if (frontendCommand.IsStockCommand) {
				switch (frontendCommand.StockCommand) {
					case StockCommand.New:
						Command = System.Windows.Input.ApplicationCommands.New;
						break;
					case StockCommand.Quit:
						SetLabel(frontendCommand, "E_xit");
						break;
				}
			}
			if (Command == null) {
				Command = new System.Windows.Input.RoutedUICommand (frontendCommand.Label,
				                                                    frontendCommand.Id,
				                                                    frontendCommand.GetType ());
			}
		}
	}
}
