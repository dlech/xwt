//
// DialogBackend.cs
//
// Author:
//       Lluis Sanchez <lluis@xamarin.com>
//
// Copyright (c) 2013 Xamarin Inc.
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
using MonoMac.AppKit;
using Xwt.Backends;
using Xwt.Commands;

namespace Xwt.Mac
{
	public class DialogBackend: WindowBackend, IDialogBackend
	{
		VBox mainBox;
		HBox buttonBox;
		Widget dialogChild;
		Size minSize;
		Dictionary<Command ,Button> buttons = new Dictionary<Command, Button> ();

		public DialogBackend ()
		{
		}

		public override void InitializeBackend (object frontend, ApplicationContext context)
		{
			base.InitializeBackend (frontend, context);

			mainBox = new VBox () {
				Spacing = 0,
				Margin = 0
			};
			buttonBox = new HBox () {
				Spacing = 0,
				Margin = 0
			};
			mainBox.PackEnd (buttonBox);
			base.SetChild ((IWidgetBackend) Toolkit.GetBackend (mainBox));
		}

		public override void SetMinSize (Size s)
		{
			minSize = s;
			var bs = buttonBox.Surface.GetPreferredSize ();
			if (bs.Width > s.Width)
				s.Width = bs.Width;
			s.Height += bs.Height;
			base.SetMinSize (s);
		}

		protected override void SetChild (IWidgetBackend child)
		{
			if (dialogChild != null)
				mainBox.Remove (dialogChild);
			dialogChild = child != null ? ((ViewBackend) child).Frontend : null;
			if (dialogChild != null)
				mainBox.PackStart (dialogChild, true);
		}

		public override void UpdateChildPlacement (IWidgetBackend childBackend)
		{
			// Do nothing. Child placement will be applied to the main box that contains the child.
		}

		#region IDialogBackend implementation

		public void SetButtons (System.Collections.Generic.IEnumerable<Command> commandList)
		{
			buttonBox.Clear ();

			foreach (var b in commandList) {
				var button = new Button ();
				var tb = b;
				button.Clicked += delegate {
					OnClicked (tb);
				};
				buttonBox.PackEnd (button);
				buttons [b] = button;
				UpdateButton (b, button);
			}
			if (minSize != Size.Zero)
				SetMinSize (minSize);
		}

		void OnClicked (Command command)
		{
			ApplicationContext.InvokeUserCode (delegate {
				((IDialogEventSink)EventSink).OnDialogButtonClicked (command);
			});
		}
		public void UpdateButton (Command command)
		{
			Button realButton;
			if (buttons.TryGetValue (command, out realButton)) {
				UpdateButton (command, realButton);
				if (minSize != Size.Zero)
					SetMinSize (minSize);
			}
		}

		public void UpdateButton (Command b, Button realButton)
		{
			realButton.Label = b.Label;
			realButton.Image = b.Icon;
			realButton.Sensitive = b.Sensitive;
			realButton.Visible = b.Visible;
		}

		protected override void OnBoundsChanged ()
		{
			mainBox.Surface.Reallocate ();
			base.OnBoundsChanged ();
		}

		public void RunLoop (IWindowFrameBackend parent)
		{
			Visible = true;
			NSApplication.SharedApplication.RunModalForWindow (this);
		}

		public void EndLoop ()
		{
			NSApplication.SharedApplication.StopModal ();
		}

		#endregion
	}
}

