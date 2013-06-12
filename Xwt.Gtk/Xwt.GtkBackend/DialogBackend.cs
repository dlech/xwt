// 
// DialogBackend.cs
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
using Xwt.Backends;
using System.Collections.Generic;
using System.Linq;


namespace Xwt.GtkBackend
{
	public class DialogBackend: WindowBackend, IDialogBackend
	{
		Command[] commands;
		Gtk.Button[] buttons;
		
		public DialogBackend ()
		{
		}
		
		public override void Initialize ()
		{
			Window = new Gtk.Dialog ();
			Window.VBox.PackStart (CreateMainLayout ());
			Window.ActionArea.Hide ();
		}
		
		new Gtk.Dialog Window {
			get { return (Gtk.Dialog) base.Window; }
			set { base.Window = value; }
		}
		
		new IDialogEventSink EventSink {
			get { return (IDialogEventSink) base.EventSink; }
		}
		
		public void SetCommands (IEnumerable<Command> newCommands)
		{
			if (buttons != null) {
				foreach (var b in buttons) {
					((Gtk.Container)b.Parent).Remove (b);
					b.Destroy ();
				}
			}
			commands = newCommands.ToArray ();
			buttons = new Gtk.Button [commands.Length];
			
			for (int n=0; n<commands.Length; n++) {
				var db = commands[n];
				Gtk.Button b = new Gtk.Button ();
				b.Show ();
				b.Label = db.Label;
				Window.ActionArea.Add (b);
				buttons[n] = b;
				buttons[n].Clicked += HandleButtonClicked;
			}
			UpdateActionAreaVisibility ();
		}

		void UpdateActionAreaVisibility ()
		{
			Window.ActionArea.Visible = Window.ActionArea.Children.Any (c => c.Visible);
		}
		
		void UpdateButton (Command command, Gtk.Button b)
		{
			if (!string.IsNullOrEmpty (command.Label) && command.Icon == null) {
				b.Label = command.Label;
			} else if (string.IsNullOrEmpty (command.Label) && command.Icon != null) {
				var pix = command.Icon.ToImageDescription ();
				b.Image = new ImageBox (ApplicationContext, pix);
			} else if (!string.IsNullOrEmpty (command.Label)) {
				Gtk.Box box = new Gtk.HBox (false, 3);
				var pix = command.Icon.ToImageDescription ();
				box.PackStart (new ImageBox (ApplicationContext, pix), false, false, 0);
				box.PackStart (new Gtk.Label (command.Label), true, true, 0);
				b.Image = box;
			}
			if (command.Visible)
				b.ShowAll ();
			else
				b.Hide ();
			b.Sensitive = command.Sensitive;
			UpdateActionAreaVisibility ();
		}
		
		void HandleButtonClicked (object o, EventArgs a)
		{
			int i = Array.IndexOf (buttons, (Gtk.Button) o);
			ApplicationContext.InvokeUserCode (delegate {
				EventSink.OnDialogButtonClicked (commands[i]);
			});
		}

		public void UpdateButton (Command command)
		{
			int i = Array.IndexOf (commands, command);
			UpdateButton (command, buttons[i]);
		}

		public void RunLoop (IWindowFrameBackend parent)
		{
			var p = (WindowFrameBackend) parent;
			MessageService.RunCustomDialog (Window, p != null ? p.Window : null);
		}

		public void EndLoop ()
		{
			Window.Respond (Gtk.ResponseType.Ok);
		}
	}
}

