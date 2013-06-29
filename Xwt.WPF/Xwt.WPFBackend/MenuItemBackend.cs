// 
// MenuItemBackend.cs
//  
// Author:
//       Carlos Alberto Cortez <calberto.cortez@gmail.com>
//       Eric Maupin <ermau@xamarin.com>
// 
// Copyright (c) 2011 Carlos Alberto Cortez
// Copyright (c) 2012 Xamarin, Inc.
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SWC = System.Windows.Controls;
using SWI = System.Windows.Input;
using SWMI = System.Windows.Media.Imaging;
using Xwt.Backends;


namespace Xwt.WPFBackend
{
	public class MenuItemBackend : Backend, IMenuItemBackend
	{
		object item;
		SWC.MenuItem menuItem;
		MenuBackend subMenu;
		MenuItemType type;
		IMenuItemEventSink menuItemEventSink;
		ICommandEventSink commandEventSink;

		public MenuItemBackend ()
			: this (new SWC.MenuItem())
		{
		}

		protected MenuItemBackend (object item)
		{
			this.item = item;
			this.menuItem = item as SWC.MenuItem;
		}

		public void Initialize (IMenuItemEventSink eventSink)
		{
			this.menuItemEventSink = eventSink;
		}

		public void Initialize (ICommandEventSink eventSink)
		{
			this.commandEventSink = eventSink;
		}

		public object Item {
			get { return this.item; }
		}

		public SWC.MenuItem MenuItem {
			get { return this.menuItem; }
		}

		public IMenuItemEventSink EventSink {
			get { return menuItemEventSink; }
		}

		public bool Checked {
			get { return this.menuItem.IsCheckable && this.menuItem.IsChecked; }
			set {
				if (!this.menuItem.IsCheckable)
					return;
				this.menuItem.IsChecked = value;
			}
		}

		public string Label {
			get { return this.menuItem.Header.ToString (); }
			set { this.menuItem.Header = value; }
		}

		public Accelerator Accelerator
		{
			get { return null; }
			set { menuItem.InputGestureText = value.ToInputGesture (); }
		}

		public bool Sensitive {
			get { return this.menuItem.IsEnabled; }
			set { this.menuItem.IsEnabled = value; }
		}

		public bool Visible {
			get { return this.menuItem.IsVisible; }
			set { this.menuItem.Visibility = (value) ? Visibility.Visible : Visibility.Collapsed; }
		}

		public void SetImage (ImageDescription imageBackend)
		{
			if (imageBackend.IsNull)
				this.menuItem.Icon = null;
			else
				this.menuItem.Icon = new ImageBox (Context) { ImageSource = imageBackend };
		}

		public void SetSubmenu (IMenuBackend menu)
		{
			if (menu == null) {
				this.menuItem.Items.Clear ();
				if (subMenu != null) {
					subMenu.RemoveFromParentItem ();
					subMenu = null;
					MenuItem.MouseEnter -= OnMouseEnter;
				}
				return;
			}

			var menuBackend = (MenuBackend)menu;
			menuBackend.RemoveFromParentItem ();

			foreach (var itemBackend in menuBackend.Items)
				this.menuItem.Items.Add (itemBackend.Item);

			/* If we have no items in the submenu, add a dummy item so that the arrow
			 * indicating that this has a submenu is visible. This is to make the
			 * behavior consistent with other platforms.
			 */
			if (!menuItem.HasItems)
				menuItem.Items.Add (menuBackend.dummyItem);
			

			// perform click event when hovering menu items to mimic Gtk.
			MenuItem.MouseEnter += OnMouseEnter;

			menuBackend.ParentItem = this;
			subMenu = menuBackend;

		}

		void OnMouseEnter(object sender, SWI.MouseEventArgs e)
		{
			if (!menuItem.IsSubmenuOpen)
				MenuItemClickHandler (sender, e);
		}

		public void SetType (MenuItemType type)
		{
			switch (type) {
				case MenuItemType.RadioButton:
				case MenuItemType.CheckBox:
					this.menuItem.IsCheckable = true;
					break;
				case MenuItemType.Normal:
					this.menuItem.IsCheckable = false;
					break;
			}

			this.type = type;
		}

		public void SetCommand (Command command)
		{
			var commandBackend = command.GetBackend() as CommandBackend;
			menuItem.Command = commandBackend.Command;
			SWI.ExecutedRoutedEventHandler execute = (sender, e) =>
			{

			};
			SWI.CanExecuteRoutedEventHandler canExecute = (sender, e) =>
			{
				e.CanExecute = true;
				// TODO: get value from command
				// e.CanExecute = command.Sensitive;
			};
			var binding = new SWI.CommandBinding (menuItem.Command, execute, canExecute);
			menuItem.CommandBindings.Add (binding);
		}

		public override void EnableEvent (object eventId)
		{
			if (menuItem == null)
				return;

			if (eventId is MenuItemEvent) {
				switch ((MenuItemEvent)eventId) {
					case MenuItemEvent.Clicked:
						this.menuItem.Click += MenuItemClickHandler;
						break;
				}
			}
		}

		public override void DisableEvent (object eventId)
		{
			if (menuItem == null)
				return;

			if (eventId is MenuItemEvent) {
				switch ((MenuItemEvent)eventId) {
					case MenuItemEvent.Clicked:
						this.menuItem.Click -= MenuItemClickHandler;
						break;
				}
			}
		}

		void MenuItemClickHandler (object sender, EventArgs args)
		{
			Context.InvokeUserCode (menuItemEventSink.OnClicked);
		}
	}
}
