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
		ApplicationContext context;

		public virtual void Initalize (ICommandEventSink eventSink)
		{
			this.eventSink = eventSink;

			// TODO: this typecast is a hack to access other objects
			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			var command = backendHost.Parent;

			// IMPORTANT: Capital letter for accelerator key implies shift modifier
			if (command.IsStockCommand) {
				switch (command.StockCommand) {
				case StockCommand.About:
					command.Label = "_About";
					break;
				case StockCommand.Add:
					command.Label = "_Add";
					break;
				case StockCommand.Apply:
					command.Label = "_Apply";
					break;
				case StockCommand.Cancel:
					command.Label = "_Cancel";
					break;
				case StockCommand.Clear:
					command.Label = "_Clear";
					break;
				case StockCommand.Close:
					command.Label = "_Close";
					command.Accelerator = new Accelerator (Key.w, ModifierKeys.Command);
					break;
				case StockCommand.CloseAll:
					command.Label = "_Close All";
					break;
				case StockCommand.Copy:
					command.Label = "_Copy";
					command.Accelerator = new Accelerator (Key.c, ModifierKeys.Command);
					break;
				case StockCommand.Cut:
					command.Label = "Cu_t";
					command.Accelerator = new Accelerator (Key.x, ModifierKeys.Command);
					break;
				case StockCommand.Delete:
					command.Label = "_Delete";
					command.Accelerator = new Accelerator (Key.Delete);
					break;
				case StockCommand.DeselectAll:
					command.Label = "Deselect All";
					command.Accelerator = new Accelerator (Key.A, ModifierKeys.Command);
					break;
				case StockCommand.Export:
					command.Label = "_Export\u2026";
					break;
				case StockCommand.Find:
					command.Label = "_Find\u2026";
					command.Accelerator = new Accelerator (Key.f, ModifierKeys.Command);
					break;
				case StockCommand.FindNext:
					command.Label = "Find Ne_xt";
					command.Accelerator = new Accelerator (Key.g, ModifierKeys.Command);
					break;
				case StockCommand.FindPrevious:
					command.Label = "Find Previous";
					command.Accelerator = new Accelerator (Key.G, ModifierKeys.Command);
					break;
				case StockCommand.Help:
					command.Label = "_Help";
					command.Accelerator = new Accelerator (Key.F1);
					break;
				case StockCommand.Import:
					command.Label = "_Import\u2026";
					break;
				case StockCommand.Maximize:
					command.Label = "Ma_ximize";
					break;
				case StockCommand.Minimize:
					command.Label = "Mi_nimize";
					break;
				case StockCommand.New:
					command.Label = "_New\u2026";
					command.Accelerator = new Accelerator (Key.n, ModifierKeys.Command);
					break;
				case StockCommand.No:
					command.Label = "_No";
					break;
				case StockCommand.Ok:
					command.Label = "_OK";
					break;
				case StockCommand.Open:
					command.Label = "_Open\u2026";
					command.Accelerator = new Accelerator (Key.o, ModifierKeys.Command);
					break;
				case StockCommand.PageSetup:
					command.Label = "Page Setup\u2026";
					break;
				case StockCommand.Paste:
					command.Label = "_Paste";
					command.Accelerator = new Accelerator (Key.v, ModifierKeys.Command);
					break;
				case StockCommand.PasteAsText:
					command.Label = "Paste As Text\u2026";
					command.Accelerator = new Accelerator (Key.V, ModifierKeys.Command);
					break;
				case StockCommand.PrintPreview:
					command.Label = "Print Preview\u2026";
					command.Accelerator = new Accelerator (Key.P, ModifierKeys.Command);
					break;
				case StockCommand.Print:
					command.Label = "Print\u2026";
					command.Accelerator = new Accelerator (Key.p, ModifierKeys.Command);
					break;
				case StockCommand.Preferences:
					command.Label = "Preferences";
					break;
				case StockCommand.Properties:
					command.Label = "Pr_operties\u2026";
					command.Accelerator = new Accelerator (Key.Return, ModifierKeys.Alt);
					break;
				case StockCommand.Quit:
					command.Label = "_Quit";
					command.Accelerator = new Accelerator (Key.q, ModifierKeys.Command);
					break;
				case StockCommand.Redo:
					command.Label = "_Redo";
					command.Accelerator = new Accelerator (Key.Z, ModifierKeys.Command);
					break;
				case StockCommand.Remove:
					command.Label = "_Remove";
					break;
				case StockCommand.Replace:
					command.Label = "_Replace\u2026";
					command.Accelerator = new Accelerator (Key.h, ModifierKeys.Command);
					break;
				case StockCommand.Revert:
					command.Label = "Revert";
					break;
				case StockCommand.Save:
					command.Label = "_Save";
					command.Accelerator = new Accelerator (Key.s, ModifierKeys.Command);
					break;
				case StockCommand.SaveAll:
					command.Label = "Save All";
					break;
				case StockCommand.SaveAs:
					command.Label = "Save _As\u2026";
					command.Accelerator = new Accelerator (Key.N, ModifierKeys.Command);
					break;
				case StockCommand.SaveCopy:
					command.Label = "Save a Copy\u2026";
					break;
				case StockCommand.SelectAll:
					command.Label = "Select _All";
					command.Accelerator = new Accelerator (Key.a, ModifierKeys.Command);
					break;
				case StockCommand.SendTo:
					command.Label = "Send To\u2026";
					command.Accelerator = new Accelerator (Key.m, ModifierKeys.Command);
					break;
				case StockCommand.Stop:
					command.Label = "_Stop";
					break;
				case StockCommand.Undo:
					command.Label = "_Undo";
					command.Accelerator = new Accelerator (Key.z, ModifierKeys.Command);
					break;
				case StockCommand.Yes:
					command.Label = "_Yes";
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

		public virtual Accelerator Accelerator { get; set; }

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
		}

		public void DisableEvent(object eventId)
		{
		}
	}
}
