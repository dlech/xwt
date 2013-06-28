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

			// IMPORTANT: Capital letter for accelerator key implies shift modifier
			if (command.IsGlobalCommand) {
				switch (command.GlobalCommand) {
					case GlobalCommand.Ok:
						command.Label = "_OK";
						break;
					case GlobalCommand.Cancel:
						command.Label = "_Cancel";
						break;
					case GlobalCommand.Yes:
						command.Label = "_Yes";
						break;
					case GlobalCommand.No:
						command.Label = "_No";
						break;
					case GlobalCommand.Add:
						command.Label = "_Add";
						break;
					case GlobalCommand.Remove:
						command.Label = "_Remove";
						break;
					case GlobalCommand.Clear:
						command.Label = "_Clear";
						break;
					case GlobalCommand.Stop:
						command.Label = "_Stop";
						break;
					case GlobalCommand.Apply:
						command.Label = "_Apply";
						break;
					case GlobalCommand.New:
						command.Label = "_New\u2026";
						command.Accelerator = new Accelerator (Key.n, ModifierKeys.Command);
						break;
					case GlobalCommand.Open:
						command.Label = "_Open\u2026";
						command.Accelerator = new Accelerator (Key.o, ModifierKeys.Command);
						break;
					case GlobalCommand.Save:
						command.Label = "_Save";
						command.Accelerator = new Accelerator (Key.s, ModifierKeys.Command);
						break;
					case GlobalCommand.SaveAs:
						command.Label = "Save _As\u2026";
						command.Accelerator = new Accelerator (Key.N, ModifierKeys.Command);
						break;
					case GlobalCommand.SaveCopy:
						command.Label = "Save a Copy\u2026";
						break;
					case GlobalCommand.Revert:
						command.Label = "Revert";
						break;
					case GlobalCommand.PageSetup:
						command.Label = "Page Setup\u2026";
						break;
					case GlobalCommand.PrintPreview:
						command.Label = "Print Preview\u2026";
						command.Accelerator = new Accelerator (Key.P, ModifierKeys.Command);
						break;
					case GlobalCommand.Print:
						command.Label = "Print\u2026";
						command.Accelerator = new Accelerator (Key.p, ModifierKeys.Command);
						break;
					case GlobalCommand.SendTo:
						command.Label = "Send To\u2026";
						command.Accelerator = new Accelerator (Key.m, ModifierKeys.Command);
						break;
					case GlobalCommand.Properties:
						command.Label = "Pr_operties\u2026";
						command.Accelerator = new Accelerator (Key.Return, ModifierKeys.Alt);
						break;
					case GlobalCommand.Close:
						command.Label = "_Close";
						command.Accelerator = new Accelerator (Key.w, ModifierKeys.Command);
						break;
					case GlobalCommand.Quit:
						command.Label = "_Quit";
						command.Accelerator = new Accelerator (Key.q, ModifierKeys.Command);
						break;
					case GlobalCommand.Undo:
						command.Label = "_Undo";
						command.Accelerator = new Accelerator (Key.z, ModifierKeys.Command);
						break;
					case GlobalCommand.Redo:
						command.Label = "_Redo";
						command.Accelerator = new Accelerator (Key.Z, ModifierKeys.Command);
						break;
					case GlobalCommand.Cut:
						command.Label = "Cu_t";
						command.Accelerator = new Accelerator (Key.x, ModifierKeys.Command);
						break;
					case GlobalCommand.Copy:
						command.Label = "_Copy";
						command.Accelerator = new Accelerator (Key.c, ModifierKeys.Command);
						break;
					case GlobalCommand.Paste:
						command.Label = "_Paste";
						command.Accelerator = new Accelerator (Key.v, ModifierKeys.Command);
						break;
					case GlobalCommand.PasteSpecial:
						command.Label = "Paste Special\u2026";
						command.Accelerator = new Accelerator (Key.V, ModifierKeys.Command);
						break;
					case GlobalCommand.Duplicate:
						command.Label = "_Duplicate";
						command.Accelerator = new Accelerator (Key.u, ModifierKeys.Command);
						break;
					case GlobalCommand.Delete:
						command.Label = "_Delete";
						command.Accelerator = new Accelerator (Key.Delete);
						break;
					case GlobalCommand.SelectAll:
						command.Label = "Select _All";
						command.Accelerator = new Accelerator (Key.a, ModifierKeys.Command);
						break;
					case GlobalCommand.DeselectAll:
						command.Label = "Deselect All";
						command.Accelerator = new Accelerator (Key.A, ModifierKeys.Command);
						break;
					case GlobalCommand.Find:
						command.Label = "_Find\u2026";
						command.Accelerator = new Accelerator (Key.f, ModifierKeys.Command);
						break;
					case GlobalCommand.FindNext:
						command.Label = "Find Ne_xt";
						command.Accelerator = new Accelerator (Key.g, ModifierKeys.Command);
						break;
					case GlobalCommand.FindPrevious:
						command.Label = "Find Previous";
						command.Accelerator = new Accelerator (Key.G, ModifierKeys.Command);
						break;
					case GlobalCommand.Replace:
						command.Label = "_Replace\u2026";
						command.Accelerator = new Accelerator (Key.h, ModifierKeys.Command);
						break;
					case GlobalCommand.Preferences:
						command.Label = "Preferences";
						break;
					case GlobalCommand.Import:
						command.Label = "_Import\u2026";
						break;
					case GlobalCommand.Export:
						command.Label = "_Export\u2026";
						break;
					default:
						command.Label = command.Id;
						break;
				}
			} else {

			}
		}

		public virtual string Label { get; set; }

		public virtual string ToolTip { get; set; }

		public virtual bool Sensitive { get; set; }

		public virtual bool Visible { get; set; }

		protected void SetAccelerator (Command command, Accelerator accelerator)
		{
			command.Accelerator = accelerator;
		}

		public abstract IMenuItemBackend CreateMenuItem ();
		public abstract IButtonBackend CreateButton ();

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
