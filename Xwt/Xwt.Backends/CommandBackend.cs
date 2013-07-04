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
				case StockCommandId.About:
					command.Label = "_About";
					break;
				case StockCommandId.Add:
					command.Label = "_Add";
					break;
				case StockCommandId.Apply:
					command.Label = "_Apply";
					break;
				case StockCommandId.Cancel:
					command.Label = "_Cancel";
					break;
				case StockCommandId.Clear:
					command.Label = "_Clear";
					break;
				case StockCommandId.Close:
					command.Label = "_Close";
					command.Accelerator = new Accelerator (Key.w, ModifierKeys.Command);
					break;
				case StockCommandId.CloseAll:
					command.Label = "_Close All";
					break;
				case StockCommandId.Copy:
					command.Label = "_Copy";
					command.Accelerator = new Accelerator (Key.c, ModifierKeys.Command);
					break;
				case StockCommandId.Cut:
					command.Label = "Cu_t";
					command.Accelerator = new Accelerator (Key.x, ModifierKeys.Command);
					break;
				case StockCommandId.Delete:
					command.Label = "_Delete";
					command.Accelerator = new Accelerator (Key.Delete);
					break;
				case StockCommandId.DeselectAll:
					command.Label = "Deselect All";
					command.Accelerator = new Accelerator (Key.A, ModifierKeys.Command);
					break;
				case StockCommandId.Export:
					command.Label = "_Export\u2026";
					break;
				case StockCommandId.Find:
					command.Label = "_Find\u2026";
					command.Accelerator = new Accelerator (Key.f, ModifierKeys.Command);
					break;
				case StockCommandId.FindNext:
					command.Label = "Find Ne_xt";
					command.Accelerator = new Accelerator (Key.g, ModifierKeys.Command);
					break;
				case StockCommandId.FindPrevious:
					command.Label = "Find Previous";
					command.Accelerator = new Accelerator (Key.G, ModifierKeys.Command);
					break;
				case StockCommandId.Help:
					command.Label = "_Help";
					command.Accelerator = new Accelerator (Key.F1);
					break;
				case StockCommandId.Import:
					command.Label = "_Import\u2026";
					break;
				case StockCommandId.Maximize:
					command.Label = "Ma_ximize";
					break;
				case StockCommandId.Minimize:
					command.Label = "Mi_nimize";
					break;
				case StockCommandId.New:
					command.Label = "_New\u2026";
					command.Accelerator = new Accelerator (Key.n, ModifierKeys.Command);
					break;
				case StockCommandId.No:
					command.Label = "_No";
					break;
				case StockCommandId.Ok:
					command.Label = "_OK";
					break;
				case StockCommandId.Open:
					command.Label = "_Open\u2026";
					command.Accelerator = new Accelerator (Key.o, ModifierKeys.Command);
					break;
				case StockCommandId.PageSetup:
					command.Label = "Page Setup\u2026";
					break;
				case StockCommandId.Paste:
					command.Label = "_Paste";
					command.Accelerator = new Accelerator (Key.v, ModifierKeys.Command);
					break;
				case StockCommandId.PasteAsText:
					command.Label = "Paste As Text\u2026";
					command.Accelerator = new Accelerator (Key.V, ModifierKeys.Command);
					break;
				case StockCommandId.PrintPreview:
					command.Label = "Print Preview\u2026";
					command.Accelerator = new Accelerator (Key.P, ModifierKeys.Command);
					break;
				case StockCommandId.Print:
					command.Label = "Print\u2026";
					command.Accelerator = new Accelerator (Key.p, ModifierKeys.Command);
					break;
				case StockCommandId.Preferences:
					command.Label = "Preferences";
					break;
				case StockCommandId.Properties:
					command.Label = "Pr_operties\u2026";
					command.Accelerator = new Accelerator (Key.Return, ModifierKeys.Alt);
					break;
				case StockCommandId.Quit:
					command.Label = "_Quit";
					command.Accelerator = new Accelerator (Key.q, ModifierKeys.Command);
					break;
				case StockCommandId.Redo:
					command.Label = "_Redo";
					command.Accelerator = new Accelerator (Key.Z, ModifierKeys.Command);
					break;
				case StockCommandId.Remove:
					command.Label = "_Remove";
					break;
				case StockCommandId.Replace:
					command.Label = "_Replace\u2026";
					command.Accelerator = new Accelerator (Key.h, ModifierKeys.Command);
					break;
				case StockCommandId.Revert:
					command.Label = "Revert";
					break;
				case StockCommandId.Save:
					command.Label = "_Save";
					command.Accelerator = new Accelerator (Key.s, ModifierKeys.Command);
					break;
				case StockCommandId.SaveAll:
					command.Label = "Save All";
					break;
				case StockCommandId.SaveAs:
					command.Label = "Save _As\u2026";
					command.Accelerator = new Accelerator (Key.N, ModifierKeys.Command);
					break;
				case StockCommandId.SaveCopy:
					command.Label = "Save a Copy\u2026";
					break;
				case StockCommandId.SelectAll:
					command.Label = "Select _All";
					command.Accelerator = new Accelerator (Key.a, ModifierKeys.Command);
					break;
				case StockCommandId.SendTo:
					command.Label = "Send To\u2026";
					command.Accelerator = new Accelerator (Key.m, ModifierKeys.Command);
					break;
				case StockCommandId.Stop:
					command.Label = "_Stop";
					break;
				case StockCommandId.Undo:
					command.Label = "_Undo";
					command.Accelerator = new Accelerator (Key.z, ModifierKeys.Command);
					break;
				case StockCommandId.Yes:
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
