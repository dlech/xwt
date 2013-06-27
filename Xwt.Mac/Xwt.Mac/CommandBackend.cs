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

			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			frontendCommand = backendHost.Parent;
		}

		public override IMenuItemBackend CreateMenuItem() {
			var menuItem = new NSMenuItem ();
			menuItem.SetTitleWithMnemonic (frontendCommand.Label.Replace("_", "&"));
			if (frontendCommand.Accelerator  != null) {
			menuItem.KeyEquivalent = frontendCommand.Accelerator.Key.ToString ();
			if (frontendCommand.Accelerator.HasModifiers) {
				var modifier = frontendCommand.Accelerator.Modifiers.Value;
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
			menuItem.KeyEquivalentModifierMask = NSEventModifierMask.CommandKeyMask;
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

