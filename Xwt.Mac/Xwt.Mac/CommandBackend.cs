using System;
using MonoMac.AppKit;
using Xwt;
using Xwt.Backends;

namespace Xwt.Mac
{
	public class CommandBackend : Xwt.Backends.CommandBackend
	{
		Command frontendCommand;

		public CommandBackend ()
		{

		}

		public override void Initalize (ICommandEventSink eventSink)
		{
			base.Initalize (eventSink);
			// TODO: this typecast is a hack to access other objects
			
			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			frontendCommand = backendHost.Parent;

			if (frontendCommand.IsGlobalCommand) {
				switch (frontendCommand.GlobalCommand.Value) {
				case GlobalCommand.Preferences:
					Label = "&Preferences\u2026";
					SetAccelerator(frontendCommand, new Accelerator (Key.Comma, ModifierKeys.Command));
					break;
				default:
					break;
				}
			}
		}

		public override IMenuItemBackend CreateMenuItem() {
			var menuItem = new NSMenuItem ();
			// TODO: add a proper escape function for replacing the mnemonic
			menuItem.SetTitleWithMnemonic (frontendCommand.Label.Replace("_", "&"));
			if (frontendCommand.Accelerator  != null) {
			menuItem.KeyEquivalent = char.ToString((char)frontendCommand.Accelerator.Key);
			if (frontendCommand.Accelerator.HasModifiers) {
				var modifier = frontendCommand.Accelerator.Modifiers;
				if (modifier.HasFlag (ModifierKeys.Alt))
					menuItem.KeyEquivalentModifierMask |= NSEventModifierMask.AlternateKeyMask;
				else
					menuItem.KeyEquivalentModifierMask &= ~NSEventModifierMask.AlternateKeyMask;
				if (modifier.HasFlag (ModifierKeys.Command))
					menuItem.KeyEquivalentModifierMask |= NSEventModifierMask.CommandKeyMask;
				else
					menuItem.KeyEquivalentModifierMask &= ~NSEventModifierMask.CommandKeyMask;
				if (modifier.HasFlag (ModifierKeys.Control))
					menuItem.KeyEquivalentModifierMask |= NSEventModifierMask.ControlKeyMask;
				else
					menuItem.KeyEquivalentModifierMask &= ~NSEventModifierMask.ControlKeyMask;
				if (modifier.HasFlag (ModifierKeys.Shift))
					menuItem.KeyEquivalentModifierMask |= NSEventModifierMask.ShiftKeyMask;
				else
					menuItem.KeyEquivalentModifierMask &= ~NSEventModifierMask.ShiftKeyMask;
				}
		    }
			menuItem.Activated += (sender, e) => frontendCommand.Activate();
			return new MenuItemBackend(menuItem);
		}

		public override IButtonBackend CreateButton() {
			return new ButtonBackend();
		}

		protected override void AddCommandActivatedHandler(EventHandler handler)
		{

		}

		protected override void RemoveCommandActivatedHandler(EventHandler handler)
		{

		}
	}
}

