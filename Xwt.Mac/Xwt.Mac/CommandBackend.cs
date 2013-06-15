using System;
using Xwt.Backends;

namespace Xwt.Mac
{
	public class CommandBackend : Xwt.Backends.CommandBackend
	{
		public CommandBackend ()
		{

		}

		public override IMenuItemBackend CreateMenuItem() {
			return new MenuItemBackend();
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

