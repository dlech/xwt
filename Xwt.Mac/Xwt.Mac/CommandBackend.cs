///
// CommandBackend.cs
//
// Author:
//       David Lechner <david@lechnology.com>
//
// Copyright (c) 2013 David Lechner
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Diagnostics;
using MonoMac.ObjCRuntime;
using MonoMac.Foundation;
using MonoMac.AppKit;
using Xwt;
using Xwt.Backends;
using Xwt.Commands;

namespace Xwt.Mac
{
	public class CommandBackend : Xwt.Backends.CommandBackend
	{
		Command frontendCommand;
		internal Selector action;
		static int commandCount;

		public CommandBackend ()
		{
			action = new Selector (string.Format ("xwtCommand{0}:", commandCount++));
		}

		public override void Initalize (ICommandEventSink eventSink)
		{
			base.Initalize (eventSink);
			// TODO: this typecast is a hack to access other objects
			
			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			frontendCommand = backendHost.Parent;

			frontendCommand.IterateAttributes<CocoaActionAttribute> ((attribute) =>
			{
				action = new Selector (attribute.Selector);
			});
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

		public override IMenuItemBackend CreateListMenuItem (int index, string label) {
			var menuItem = CreateMenuItem () as MenuItemBackend;
			menuItem.Label = string.Format ("{0} {1}", index + 1, label);
			menuItem.Item.Tag = index;
			return menuItem;
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

