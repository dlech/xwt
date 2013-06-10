using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Xwt.Backends
{
	public class CommandCollection : Collection<Command>
	{
		ICollectionListener listener;
		
		public CommandCollection (ICollectionListener listener)
		{
			this.listener = listener;
		}

		protected override void InsertItem (int index, Command item)
		{
			base.InsertItem (index, item);
			if (listener != null)
				listener.ItemAdded (this, item);
		}
		
		protected override void RemoveItem (int index)
		{
			object ob = this [index];
			base.RemoveItem (index);
			if (listener != null)
				listener.ItemRemoved (this, ob);
		}
		
		protected override void SetItem (int index, Command item)
		{
			object ob = this [index];
			base.SetItem (index, item);
			if (listener != null)
				listener.ItemRemoved (this, ob);
			if (listener != null)
				listener.ItemAdded (this, item);
		}

		protected override void ClearItems ()
		{
			List<Command> copy = new List<Command> (this);
			base.ClearItems ();
			foreach (var c in copy)
				listener.ItemRemoved (this, c);
		}
	}
}
