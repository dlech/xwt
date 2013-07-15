// 
// MenuBackend.cs
//  
// Author:
//       Lluis Sanchez <lluis@xamarin.com>
// 
// Copyright (c) 2011 Xamarin Inc
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
using MonoMac.AppKit;
using MonoMac.Foundation;
using Xwt.Backends;
using Xwt.Commands;

namespace Xwt.Mac
{
	public class MenuBackend: NSMenu, IMenuBackend
	{
		bool mainMenuMode = false;

		public MenuBackend()
		{
			Delegate = new MenuDelegate (OnItemHighlighted);
		}

		public void InitializeBackend (object frontend, ApplicationContext context)
		{
			CommandManager.AddCommandHandlers ((XwtUiComponent)frontend, this);
		}

		public void InsertItem (int index, IMenuItemBackend menuItem)
		{
			var nsMenuItem = ((MenuItemBackend)menuItem).Item;
			base.InsertItem (nsMenuItem, index);
			if (mainMenuMode && nsMenuItem.Submenu != null)
				nsMenuItem.Submenu.Title = nsMenuItem.Title;
			ItemHighlighted += ((MenuItemBackend)menuItem).HandleItemHighlighted;
		}

		public void RemoveItem (IMenuItemBackend menuItem)
		{
			RemoveItem (((MenuItemBackend)menuItem).Item);
			ItemHighlighted -= ((MenuItemBackend)menuItem).HandleItemHighlighted;
		}
		
		public void SetMainMenuMode ()
		{
			mainMenuMode = true;
			for (int n=0; n<Count; n++) {
				var item = ItemAt (n);
				if (item.Menu != null)
					item.Submenu.Title = item.Title;
			}
		}

		public void EnableEvent (object eventId)
		{
		}

		public void DisableEvent (object eventId)
		{
		}
		
		public void Popup ()
		{
			var evt = NSApplication.SharedApplication.CurrentEvent;
			NSMenu.PopUpContextMenu (this, evt, evt.Window.ContentView, null);
		}
		
		public void Popup (IWidgetBackend widget, double x, double y)
		{
			NSMenu.PopUpContextMenu (this, NSApplication.SharedApplication.CurrentEvent, ((ViewBackend)widget).Widget, null);
		}

		public virtual bool HandlesCommand (Command command)
		{
			var commandBackend = command.GetBackend () as CommandBackend;
			return RespondsToSelector (commandBackend.action);
		}

		internal delegate void ItemHighlightedHandler(object sender, ItemHighlightedEventArgs e);
		internal event ItemHighlightedHandler ItemHighlighted;

		internal class ItemHighlightedEventArgs : EventArgs
		{
			public ItemHighlightedEventArgs (NSMenuItem item)
			{
				Item = item;
			}

			public NSMenuItem Item { get; private set; }
		}

		void OnItemHighlighted(NSMenuItem item)
		{
			if (ItemHighlighted != null)
				ItemHighlighted(this, new ItemHighlightedEventArgs(item));
		}

		class MenuDelegate : NSMenuDelegate
		{
			Action<NSMenuItem> itemHighlightedHander;

			public 	MenuDelegate(Action<NSMenuItem> handler)
			{
				itemHighlightedHander = handler;
			}

			public override void MenuWillHighlightItem (NSMenu menu, NSMenuItem item)
			{
				itemHighlightedHander.Invoke (item);
			}
		}
	}
}
