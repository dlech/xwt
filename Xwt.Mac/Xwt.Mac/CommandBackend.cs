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

			switch (frontendCommand.Id) {
			case "Xwt.StockCommands.App.About":
				action = new Selector ("orderFrontStandardAboutPanel:");
				Label = string.Format ("About {0}", bundleName);
				break;
			case "Xwt.StockCommands.File.Close":
				action = new Selector ("close:");
				break;
			case "Xwt.StockCommands.File.CloseAll":
				DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.w, ModifierKeys.Primary | ModifierKeys.Alt);
				break;
			case "Xwt.StockCommands.Edit.Copy":
				action = new Selector ("copy:");
				break;
			case "Xwt.StockCommands.Edit.Cut":
				action = new Selector ("cut:");
				break;
			case "Xwt.StockCommands.Edit.Delete":
				action = new Selector ("delete:");
				break;
			case "Xwt.StockCommands.File.Export":
				Label = "_Export As\u2026";
				break;
			case "Xwt.StockCommands.Edit.Find":
				action = new Selector ("performTextFinderAction:");
				break;
			case "Xwt.StockCommands.Misc.Help":
				Label = string.Format ("{0} Help", bundleName);
				action = new Selector ("showHelp:");
				break;
			case "Xwt.StockCommands.App.Hide":
				Label = string.Format ("Hide {0}", bundleName);
				DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.h, ModifierKeys.Primary);
				action = new Selector("hide:");
				break;
			case "Xwt.StockCommands.App.HideOthers":
				Label = "Hide others";
				DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.h, ModifierKeys.Primary | ModifierKeys.Alt);
				action = new Selector("hideOtherApplications:");
				break;
			case "Xwt.StockCommands.Window.Maximize":
				Label = "Zoom";
				action = new Selector("performZoom:");
				break;
			case "Xwt.StockCommands.Window.Minimize":
				DefaultKeyboardShortcut = new KeyboardShortcutSequence(Key.m, ModifierKeys.Primary);
				action = new Selector("performMiniaturize:");
				break;
			case "Xwt.StockCommands.File.New":
				action = new Selector ("new:");
				break;
			case "Xwt.StockCommands.File.Open":
				action = new Selector ("open:");
				break;
			case "Xwt.StockCommands.Edit.Paste":
				action = new Selector ("paste:");
				break;
			case "Xwt.StockCommands.Edit.PasteAsText":
				action = new Selector ("pasteAsPlainText:");
				break;
			case "Xwt.StockCommands.App.Preferences":
				Label = "Preferences\u2026";
				DefaultKeyboardShortcut = new KeyboardShortcutSequence (Key.Comma, ModifierKeys.Primary);
				break;
			case "Xwt.StockCommands.File.Print":
				action = new Selector ("print:");
				break;
			case "Xwt.StockCommands.App.Quit":
				Label = string.Format ("Quit {0}", bundleName);
				action = new Selector ("terminate:");
				break;
			case "Xwt.StockCommands.Edit.Redo":
				action = new Selector ("redo:");
				break;					
			case "Xwt.StockCommands.Edit.Replace":
				action = new Selector ("replace:");
				break;
			case "Xwt.StockCommands.File.Revert":
				action = new Selector ("revert:");
				break;					
			case "Xwt.StockCommands.File.Save":
				action = new Selector ("save:");
				break;
			case "Xwt.StockCommands.Edit.SelectAll":
				action = new Selector ("selectAll:");
				break;
			case "Xwt.StockCommands.Misc.Stop":
				action = new Selector ("stop:");
				break;
			case "Xwt.StockCommands.Edit.Undo":
				action = new Selector ("undo:");
				break;
			case "Xwt.StockCommands.App.UnhideAll":
				Label = "Show All";
				action = new Selector ("unhideAllApplications:");
				break;
			default:
				break;
			}
		}

		public override IMenuItemBackend CreateMenuItem() {
			var menuItem = new NSMenuItem ();
			if (Label != null)
				menuItem.Title = Label.RemoveMnemonics ();
			if (DefaultKeyboardShortcut  != null) {
				if (DefaultKeyboardShortcut.Count == 1) {
					menuItem.KeyEquivalent = DefaultKeyboardShortcut[0].Key.ToMacKey ();
					if (DefaultKeyboardShortcut[0].HasModifiers)
						menuItem.KeyEquivalentModifierMask =
							DefaultKeyboardShortcut[0].Modifiers.ToNSEventModifierMask ();
				} else {
					// TODO: handle more than one shortcut in sequence
				}
		    }
			menuItem.Action = action;
			menuItem.Target = null;
			return new MenuItemBackend(menuItem);
		}

		public override IButtonBackend CreateButton() {
			var button = new ButtonBackend();
			button.Widget.Title = Label.RemoveMnemonics ();
			if (DefaultKeyboardShortcut  != null) {
				if (DefaultKeyboardShortcut.Count == 1) {
					button.Widget.KeyEquivalent = DefaultKeyboardShortcut[0].Key.ToMacKey ();
					if (DefaultKeyboardShortcut[0].HasModifiers)
						button.Widget.KeyEquivalentModifierMask =
							frontendCommand.DefaultKeyboardShortcut[0].Modifiers.ToNSEventModifierMask ();
				} else {
					// TODO: handle more than one shortcut in sequence
				}
			}
			button.Widget.Action = action;
			button.Widget.Target = null;
			return button;
		}
	}
}

