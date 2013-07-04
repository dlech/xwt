using System;
using System.Diagnostics;
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
			action = new Selector (string.Format("xwtCommand{0}:", commandCount++));
		}

		public override void Initalize (ICommandEventSink eventSink)
		{
			base.Initalize (eventSink);
			// TODO: this typecast is a hack to access other objects
			
			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			frontendCommand = backendHost.Parent;

			// TODO: make BundleName a property of MacEngine or Util
			string bundleName;
			var key = new NSString ("CFBundleName");
			NSObject value;
			if (NSBundle.MainBundle.InfoDictionary.TryGetValue (key, out value))
				bundleName = (value as NSString).ToString ();
			else
				bundleName = Process.GetCurrentProcess ().ProcessName;

			if (frontendCommand.IsStockCommand) {
				switch (frontendCommand.StockCommand) {
				case StockCommandId.About:
					action = new Selector ("orderFrontStandardAboutPanel:");
					Label = string.Format ("About {0}", bundleName);
					break;
				case StockCommandId.Close:
					action = new Selector ("close:");
					break;
				case StockCommandId.CloseAll:
					Accelerator = new Accelerator (Key.w, ModifierKeys.Command | ModifierKeys.Alt);
					break;
				case StockCommandId.Copy:
					action = new Selector ("copy:");
					break;
				case StockCommandId.Cut:
					action = new Selector ("cut:");
					break;
				case StockCommandId.Delete:
					action = new Selector ("delete:");
					break;
				case StockCommandId.Export:
					Label = "_Export As\u2026";
					break;
				case StockCommandId.Find:
					action = new Selector ("performTextFinderAction:");
					break;
				case StockCommandId.Help:
					Label = string.Format ("{0} Help", bundleName);
					action = new Selector ("showHelp:");
					break;
				case StockCommandId.HideApplication:
					Label = string.Format ("Hide {0}", bundleName);
					Accelerator = new Accelerator (Key.h, ModifierKeys.Command);
					action = new Selector("hide:");
					break;
				case StockCommandId.HideOtherApplications:
					Label = "Hide others";
					Accelerator = new Accelerator (Key.h, ModifierKeys.Command | ModifierKeys.Alt);
					action = new Selector("hideOtherApplications:");
					break;
				case StockCommandId.Maximize:
					Label = "Zoom";
					action = new Selector("performZoom:");
					break;
				case StockCommandId.Minimize:
					Accelerator = new Accelerator(Key.m, ModifierKeys.Command);
					action = new Selector("performMiniaturize:");
					break;
				case StockCommandId.New:
					action = new Selector ("new:");
					break;
				case StockCommandId.Open:
					action = new Selector ("open:");
					break;
				case StockCommandId.Paste:
					action = new Selector ("paste:");
					break;
				case StockCommandId.PasteAsText:
					action = new Selector ("pasteAsPlainText:");
					break;
				case StockCommandId.Preferences:
					Label = "Preferences\u2026";
					Accelerator = new Accelerator (Key.Comma, ModifierKeys.Command);
					break;
				case StockCommandId.Print:
					action = new Selector ("print:");
					break;
				case StockCommandId.Quit:
					Label = string.Format ("Quit {0}", bundleName);
					action = new Selector ("terminate:");
					break;
				case StockCommandId.Redo:
					action = new Selector ("redo:");
					break;					
				case StockCommandId.Replace:
					action = new Selector ("replace:");
					break;
				case StockCommandId.Revert:
					action = new Selector ("revert:");
					break;					
				case StockCommandId.Save:
					action = new Selector ("save:");
					break;
				case StockCommandId.SelectAll:
					action = new Selector ("selectAll:");
					break;
				case StockCommandId.Stop:
					action = new Selector ("stop:");
					break;
				case StockCommandId.Undo:
					action = new Selector ("undo:");
					break;
				case StockCommandId.UnhideAllApplications:
					Label = "Show All";
					action = new Selector ("unhideAllApplications:");
					break;
				default:
					break;
				}
			}
		}

		public override IMenuItemBackend CreateMenuItem() {
			var menuItem = new NSMenuItem ();
			menuItem.Title = Label.RemoveMnemonics ();
			if (Accelerator  != null) {
				menuItem.KeyEquivalent = Accelerator.Key.ToMacKey ();
				if (Accelerator.HasModifiers)
					menuItem.KeyEquivalentModifierMask =
						Accelerator.Modifiers.ToNSEventModifierMask ();
		    }
			menuItem.Action = action;
			menuItem.Target = null;
			return new MenuItemBackend(menuItem);
		}

		public override IButtonBackend CreateButton() {
			var button = new ButtonBackend();
			button.Widget.Title = Label.RemoveMnemonics ();
			if (Accelerator  != null) {
				button.Widget.KeyEquivalent = Accelerator.Key.ToMacKey ();
				if (Accelerator.HasModifiers)
					button.Widget.KeyEquivalentModifierMask =
						frontendCommand.Accelerator.Modifiers.ToNSEventModifierMask ();
			}
			button.Widget.Action = action;
			button.Widget.Target = null;
			return button;
		}
	}
}

