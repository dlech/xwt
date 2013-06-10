// 
// Command.cs
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
using Xwt.Drawing;
using Xwt.Backends;

namespace Xwt
{
	[BackendType(typeof(ICommandBackend))]
	public class Command : XwtComponent
	{
		protected class CommandBackendHost : BackendHost<Command, ICommandBackend>, ICommandEventSink
		{
			protected override void OnBackendCreated ()
			{
				base.OnBackendCreated ();
				Backend.Initalize (this);
			}

			public void OnCommand ()
			{
				throw new NotImplementedException ();
			}

			public void OnEnabledChanged ()
			{
				throw new NotImplementedException ();
			}
		}

		protected override Xwt.Backends.BackendHost CreateBackendHost ()
		{
			return new CommandBackendHost ();
		}

		public Command (string id)
			: this (id, id) { }

		public Command (string id, string label)
			: this (id, label, null, null) { }

		public Command (string id, string label, Accelerator accelerator)
			: this(id, label, accelerator, null) { }

		public Command (string id, string label, Accelerator accelerator, Image icon)
		{
			Id = id;
			Label = label;
			Accelerator = accelerator;
			Icon = icon;
		}

		public Command (StockCommand command)
		{
			Id = command.ToString ();
			StockCommand = command;
		}

		public string Id { get; internal set; }

		public string Label { get; internal set; }

		public Image Icon { get; internal set; }

		public Accelerator Accelerator { get; internal set; }

		public StockCommand? StockCommand { get; internal set; }

		public bool IsStockCommand { get { return StockCommand != null; } }

		ICommandBackend Backend
		{
			get { return (ICommandBackend)base.BackendHost.Backend; }
		}
	}
}

