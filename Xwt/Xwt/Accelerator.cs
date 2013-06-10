using System;

namespace Xwt
{
	/// <summary>
	/// Accelerator = Keyboard shortcut.
	/// </summary>
	public class Accelerator
	{
		public Key Key { get; private set; }

		public ModifierKeys? Modifiers { get; private set; }

		public bool HasModifiers { get { return Modifiers != null;  } }

		public Accelerator (Key key)
		{
			Key = key;
		}

		public Accelerator (Key key, ModifierKeys modifiers)
		{
			Key = key;
			Modifiers = modifiers;
		}
	}
}

