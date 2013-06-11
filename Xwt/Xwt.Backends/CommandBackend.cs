// -----------------------------------------------------------------------
// <copyright file="CommandBackend.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Xwt.Backends
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Default implementation of <see cref="ICommandBackend"/>
	/// </summary>
	public abstract class CommandBackend : ICommandBackend
	{
		ICommandEventSink eventSink;
		List<CommandEvent> enabledEvents;
		ApplicationContext context;

		public virtual void Initalize (ICommandEventSink eventSink)
		{
			this.eventSink = eventSink;

			// TODO: this typecast is a hack to access other objects
			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			var command = backendHost.Parent;
			if (command.IsStockCommand) {
				switch (command.StockCommand) {
					case StockCommand.Ok:
						command.Label = "_OK";
						break;
					case StockCommand.Cancel:
						command.Label = "_Cancel";
						break;
					case StockCommand.Yes:
						command.Label = "_Yes";
						break;
					case StockCommand.No:
						command.Label = "_No";
						break;
					case StockCommand.Add:
						command.Label = "_Add";
						break;
					case StockCommand.Remove:
						command.Label = "_Remove";
						break;
					case StockCommand.Clear:
						command.Label = "_Clear";
						break;
					case StockCommand.Stop:
						command.Label = "_Stop";
						break;
					case StockCommand.Apply:
						command.Label = "_Apply";
						break;
					case StockCommand.New:
						command.Label = "_New\u2026";
						command.Accelerator = new Accelerator (Key.N, ModifierKeys.Control);
						break;
					case StockCommand.Open:
						command.Label = "_Open\u2026";
						command.Accelerator = new Accelerator (Key.O, ModifierKeys.Control);
						break;
					case StockCommand.Save:
						command.Label = "_Save";
						command.Accelerator = new Accelerator (Key.S, ModifierKeys.Control);
						break;
					case StockCommand.SaveAs:
						command.Label = "Save _As\u2026";
						command.Accelerator = new Accelerator (Key.N, ModifierKeys.Control | ModifierKeys.Shift);
						break;
					case StockCommand.SaveCopy:
						command.Label = "Save a Copy\u2026";
						break;
					case StockCommand.Revert:
						command.Label = "Revert";
						break;
					case StockCommand.PageSetup:
						command.Label = "Page Setup\u2026";
						break;
					case StockCommand.PrintPreview:
						command.Label = "Print Preview\u2026";
						command.Accelerator = new Accelerator (Key.P, ModifierKeys.Shift | ModifierKeys.Control);
						break;
					case StockCommand.Print:
						command.Label = "Print\u2026";
						command.Accelerator = new Accelerator (Key.P, ModifierKeys.Control);
						break;
					case StockCommand.SendTo:
						command.Label = "Send To\u2026";
						command.Accelerator = new Accelerator (Key.M, ModifierKeys.Control);
						break;
					case StockCommand.Properties:
						command.Label = "Pr_operties\u2026";
						command.Accelerator = new Accelerator (Key.Return, ModifierKeys.Alt);
						break;
					case StockCommand.Close:
						command.Label = "_Close";
						command.Accelerator = new Accelerator (Key.W, ModifierKeys.Control);
						break;
					case StockCommand.Quit:
						command.Label = "_Quit";
						command.Accelerator = new Accelerator (Key.Q, ModifierKeys.Control);
						break;
					case StockCommand.Undo:
						command.Label = "_Undo";
						command.Accelerator = new Accelerator (Key.Z, ModifierKeys.Control);
						break;
					case StockCommand.Redo:
						command.Label = "_Redo";
						command.Accelerator = new Accelerator (Key.Z, ModifierKeys.Shift | ModifierKeys.Control);
						break;
					case StockCommand.Cut:
						command.Label = "Cu_t";
						command.Accelerator = new Accelerator (Key.X, ModifierKeys.Control);
						break;
					case StockCommand.Copy:
						command.Label = "_Copy";
						command.Accelerator = new Accelerator (Key.C, ModifierKeys.Control);
						break;
					case StockCommand.Paste:
						command.Label = "_Paste";
						command.Accelerator = new Accelerator (Key.V, ModifierKeys.Control);
						break;
					case StockCommand.PasteSpecial:
						command.Label = "Paste Special\u2026";
						command.Accelerator = new Accelerator (Key.V, ModifierKeys.Shift | ModifierKeys.Control);
						break;
					case StockCommand.Duplicate:
						command.Label = "_Duplicate";
						command.Accelerator = new Accelerator (Key.U, ModifierKeys.Control);
						break;
					case StockCommand.Delete:
						command.Label = "_Delete";
						command.Accelerator = new Accelerator (Key.Delete);
						break;
					case StockCommand.SelectAll:
						command.Label = "Select _All";
						command.Accelerator = new Accelerator (Key.A, ModifierKeys.Control);
						break;
					case StockCommand.DeselectAll:
						command.Label = "Deselect All";
						command.Accelerator = new Accelerator (Key.A, ModifierKeys.Shift | ModifierKeys.Control);
						break;
					case StockCommand.Find:
						command.Label = "_Find\u2026";
						command.Accelerator = new Accelerator (Key.F, ModifierKeys.Control);
						break;
					case StockCommand.FindNext:
						command.Label = "Find Ne_xt";
						command.Accelerator = new Accelerator (Key.G, ModifierKeys.Control);
						break;
					case StockCommand.FindPrevious:
						command.Label = "Find Previous";
						command.Accelerator = new Accelerator (Key.G, ModifierKeys.Shift | ModifierKeys.Control);
						break;
					case StockCommand.Replace:
						command.Label = "_Replace\u2026";
						command.Accelerator = new Accelerator (Key.H, ModifierKeys.Control);
						break;
					case StockCommand.Preferences:
						command.Label = "Preferences";
						break;
					default:
						command.Label = command.Id;
						break;
				}
			} else {

			}
		}

		protected void SetLabel (Command command, string label)
		{
			command.Label = label;
		}

		protected void SetAccelerator (Command command, Accelerator accelerator)
		{
			command.Accelerator = accelerator;
		}

		public abstract IMenuItemBackend CreateMenuItem ();
		public abstract IMenuBackend CreateMenu ();

		public virtual void InitializeBackend(object frontend, ApplicationContext context)
		{
			this.context = context;
		}

		public virtual void EnableEvent(object eventId)
		{
			var commandEventId = eventId as CommandEvent?;
			if (commandEventId.HasValue) {
				if (enabledEvents == null)
					enabledEvents = new List<CommandEvent> ();
				enabledEvents.Add (commandEventId.Value);
				switch (commandEventId.Value) {
					case CommandEvent.Activated:
						AddCommandActivatedHandler(HandleCommandActivated);
						break;
				}
			}
		}

		public void DisableEvent(object eventId)
		{
			var commandEventId = eventId as CommandEvent?;
			if (commandEventId.HasValue) {
				enabledEvents.Remove (commandEventId.Value);
				switch (commandEventId.Value) {
					case CommandEvent.Activated:
						RemoveCommandActivatedHandler (HandleCommandActivated);
						break;
				}
			}
		}

		void HandleCommandActivated (object sender, EventArgs e)
		{
			context.InvokeUserCode (delegate
			{
				eventSink.OnActivated ();
			});
		}

		protected abstract void AddCommandActivatedHandler (EventHandler handler);
		protected abstract void RemoveCommandActivatedHandler (EventHandler handler);
	}
}
