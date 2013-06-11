// 
// CommandCollection.cs
//  
// Author:
//       David Lechner <david@lechnology.com>
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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xwt.Backends;

namespace Xwt
{
	public class CommandCollection : Collection<Command>
	{
		ICommandTarget target;
		ICollectionListener listener;
		
		public CommandCollection (ICommandTarget target, ICollectionListener listener)
		{
			this.target = target;
			this.listener = listener;
		}

		protected override void InsertItem (int index, Command item)
		{
			if (item != null)
				item.target = target;
			base.InsertItem (index, item);
			if (listener != null)
				listener.ItemAdded (this, item);
		}
		
		protected override void RemoveItem (int index)
		{
			var command = this [index];
			if (command != null)
				command.target = null;
			base.RemoveItem (index);
			if (listener != null)
				listener.ItemRemoved (this, command);
		}
		
		protected override void SetItem (int index, Command item)
		{
			var command = this [index];
			if (command != null)
				command.target = null;
			if (item != null)
				item.target = target;
			base.SetItem (index, item);
			if (listener != null)
				listener.ItemRemoved (this, command);
			if (listener != null)
				listener.ItemAdded (this, item);
		}

		protected override void ClearItems ()
		{
			List<Command> copy = new List<Command> (this);
			base.ClearItems ();
			foreach (var c in copy) {
				c.target = null;
				if (listener != null)
					listener.ItemRemoved (this, c);
			}
		}
	}
}
