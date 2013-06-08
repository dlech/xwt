using System;

namespace Xwt
{
	/// <summary>
	/// Accelerator = Keyboard shortcut.
	/// </summary>
	public class Accelerator
	{
		public Key Key;
		public ModifierKeys Modifiers;

		public Accelerator (Key key, ModifierKeys modifiers)
		{
			Key = key;
			Modifiers = modifiers;
		}
	}
}

