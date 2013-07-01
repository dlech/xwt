using System;
using MonoMac.AppKit;
using Xwt;
using Xwt.Backends;

namespace Xwt.Mac
{
	public class CommandBackend : Xwt.Backends.CommandBackend
	{
		Command frontendCommand;
		internal MonoMac.ObjCRuntime.Selector action;
		static int commandCount;

		public CommandBackend ()
		{
			var internalCommandId = string.Format("xwtCommand{0}:", commandCount++);
			action = new MonoMac.ObjCRuntime.Selector (internalCommandId);
		}

		public override void Initalize (ICommandEventSink eventSink)
		{
			base.Initalize (eventSink);
			// TODO: this typecast is a hack to access other objects
			
			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			frontendCommand = backendHost.Parent;

			if (frontendCommand.IsStockCommand) {
				switch (frontendCommand.StockCommand.Value) {
				case StockCommand.Close:
					action = new MonoMac.ObjCRuntime.Selector ("close:");
					break;
				case StockCommand.Copy:
					action = new MonoMac.ObjCRuntime.Selector ("copy:");
					break;
				case StockCommand.Cut:
					action = new MonoMac.ObjCRuntime.Selector ("cut:");
					break;
				case StockCommand.Delete:
					action = new MonoMac.ObjCRuntime.Selector ("delete:");
					break;
//				case StockCommand.Help:
//					action = new MonoMac.ObjCRuntime.Selector ("showHelp:");
//					break;
				case StockCommand.New:
					action = new MonoMac.ObjCRuntime.Selector ("new:");
					break;
				case StockCommand.Open:
					action = new MonoMac.ObjCRuntime.Selector ("open:");
					break;
				case StockCommand.Paste:
					action = new MonoMac.ObjCRuntime.Selector ("paste:");
					break;
				case StockCommand.Preferences:
					Label = "Preferences\u2026";
					SetAccelerator(frontendCommand, new Accelerator (Key.Comma, ModifierKeys.Command));
					break;
				case StockCommand.Print:
					action = new MonoMac.ObjCRuntime.Selector ("print:");
					break;
				case StockCommand.Quit:
					action = new MonoMac.ObjCRuntime.Selector ("terminate:");
					break;
				case StockCommand.Redo:
					action = new MonoMac.ObjCRuntime.Selector ("redo:");
					break;					
				case StockCommand.Replace:
					action = new MonoMac.ObjCRuntime.Selector ("replace:");
					break;
				case StockCommand.Revert:
					action = new MonoMac.ObjCRuntime.Selector ("revert:");
					break;					
				case StockCommand.Save:
					action = new MonoMac.ObjCRuntime.Selector ("save:");
					break;
				case StockCommand.SelectAll:
					action = new MonoMac.ObjCRuntime.Selector ("selectAll:");
					break;
				case StockCommand.Stop:
					action = new MonoMac.ObjCRuntime.Selector ("stop:");
					break;
				case StockCommand.Undo:
					action = new MonoMac.ObjCRuntime.Selector ("undo:");
					break;
				default:
					break;
				}
			}
		}

		public override IMenuItemBackend CreateMenuItem() {
			var menuItem = new NSMenuItem ();
			menuItem.Title = frontendCommand.Label.RemoveMnemonics ();
			if (frontendCommand.Accelerator  != null) {
				menuItem.KeyEquivalent = char.ToString((char)frontendCommand.Accelerator.Key);
				if (frontendCommand.Accelerator.HasModifiers)
					menuItem.KeyEquivalentModifierMask =
						frontendCommand.Accelerator.Modifiers.ToNSEventModifierMask ();
		    }
			menuItem.Action = action;
			menuItem.Target = null;
			return new MenuItemBackend(menuItem);
		}

		public override IButtonBackend CreateButton() {
			var button = new ButtonBackend();
			button.Widget.Title = frontendCommand.Label.RemoveMnemonics ();
			if (frontendCommand.Accelerator  != null) {
				button.Widget.KeyEquivalent = char.ToString((char)frontendCommand.Accelerator.Key);
				if (frontendCommand.Accelerator.HasModifiers)
					button.Widget.KeyEquivalentModifierMask =
						frontendCommand.Accelerator.Modifiers.ToNSEventModifierMask ();
			}
			button.Widget.Action = action;
			button.Widget.Target = null;
			return button;
		}

		protected override void AddCommandActivatedHandler(EventHandler handler)
		{

		}

		protected override void RemoveCommandActivatedHandler(EventHandler handler)
		{

		}
	}
}

