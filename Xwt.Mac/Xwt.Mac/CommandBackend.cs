using System;
using MonoMac.ObjCRuntime;
using MonoMac.Foundation;
using MonoMac.AppKit;
using Xwt;
using Xwt.Backends;

namespace Xwt.Mac
{
	public class CommandBackend : Xwt.Backends.CommandBackend
	{
		Command frontendCommand;
		internal Selector action;
		static int commandCount;

		public CommandBackend ()
		{
			var internalCommandId = string.Format("xwtCommand{0}:", commandCount++);
			action = new Selector (internalCommandId);
		}

		public override void Initalize (ICommandEventSink eventSink)
		{
			base.Initalize (eventSink);
			// TODO: this typecast is a hack to access other objects
			
			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			frontendCommand = backendHost.Parent;

			if (frontendCommand.IsStockCommand) {
				switch (frontendCommand.StockCommand.Value) {
				case StockCommand.About:
					action = new Selector ("orderedFrontStandardAboutPanel:");
					Label = string.Format ("About {0}", NSApplication.SharedApplication.Description);
					break;
				case StockCommand.Close:
					action = new Selector ("close:");
					break;
				case StockCommand.Copy:
					action = new Selector ("copy:");
					break;
				case StockCommand.Cut:
					action = new Selector ("cut:");
					break;
				case StockCommand.Delete:
					action = new Selector ("delete:");
					break;
				case StockCommand.Help:
					action = new Selector ("showHelp:");
					break;
				case StockCommand.Maximize:
					Label = "Zoom";
					action = new Selector("performZoom:");
					break;
				case StockCommand.Minimize:
					Accelerator = new Accelerator(Key.m, ModifierKeys.Command);
					action = new Selector("performMiniaturize:");
					break;
				case StockCommand.New:
					action = new Selector ("new:");
					break;
				case StockCommand.Open:
					action = new Selector ("open:");
					break;
				case StockCommand.Paste:
					action = new Selector ("paste:");
					break;
				case StockCommand.Preferences:
					Label = "Preferences\u2026";
					SetAccelerator(frontendCommand, new Accelerator (Key.Comma, ModifierKeys.Command));
					break;
				case StockCommand.Print:
					action = new Selector ("print:");
					break;
				case StockCommand.Quit:
					action = new Selector ("terminate:");
					break;
				case StockCommand.Redo:
					action = new Selector ("redo:");
					break;					
				case StockCommand.Replace:
					action = new Selector ("replace:");
					break;
				case StockCommand.Revert:
					action = new Selector ("revert:");
					break;					
				case StockCommand.Save:
					action = new Selector ("save:");
					break;
				case StockCommand.SelectAll:
					action = new Selector ("selectAll:");
					break;
				case StockCommand.Stop:
					action = new Selector ("stop:");
					break;
				case StockCommand.Undo:
					action = new Selector ("undo:");
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
			Action<NSObject> method = (sender) => {
				handler (sender, EventArgs.Empty);
			};
			MacEngine.App.AddTargetMethod (action, method);
		}

		protected override void RemoveCommandActivatedHandler(EventHandler handler)
		{

		}
	}
}

