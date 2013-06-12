// 
// Dialog.cs
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
using Xwt.Drawing;


namespace Xwt
{
	[BackendType (typeof(IDialogBackend))]
	public class Dialog: Window
	{
		Command resultCommand;
		bool loopEnded;
		
		public Dialog ()
		{
		}
		
		protected new class WindowBackendHost: Window.WindowBackendHost, ICollectionListener, IDialogEventSink
		{
			new Dialog Parent { get { return (Dialog) base.Parent; } }
			
			public virtual void ItemAdded (object collection, object item)
			{
				if (collection == Parent.Commands) {
					((Command)item).Target = Parent;
				}
			}

			public virtual void ItemRemoved (object collection, object item)
			{
				if (collection == Parent.Commands) {
					((Command)item).Target = null;
				}
			}
			
			public void OnDialogButtonClicked (Command command)
			{
				Parent.OnCommandActivated (command);
			}
		}
		
		protected override BackendHost CreateBackendHost ()
		{
			return new WindowBackendHost ();
		}
		
		IDialogBackend Backend {
			get { return (IDialogBackend) BackendHost.Backend; } 
		}
		
		protected virtual void OnCommandActivated (Command cmd)
		{
			Respond (cmd);
		}
		
		public Command Run ()
		{
			return Run (null);
		}
		
		public Command Run (WindowFrame parent)
		{
			BackendHost.ToolkitEngine.ValidateObject (parent);
			AdjustSize ();

			BackendHost.ToolkitEngine.InvokePlatformCode (delegate {
				Backend.RunLoop ((IWindowFrameBackend) Toolkit.GetBackend (parent));
			});
			return resultCommand;
		}
		
		public void Respond (Command cmd)
		{
			resultCommand = cmd;
			if (!loopEnded) {
				loopEnded = true;
				Backend.EndLoop ();
			}
		}
	}
}

