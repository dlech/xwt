using System;

namespace Xwt
{
	/// <summary>
	/// Represents a keyboard shortcut used to invoke <see cref="Command"/>s
	/// </summary>
	public class KeyboardShortcut
	{
		Key key;

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		public Key Key { get; private set; }

		/// <summary>
		/// Gets the modifiers.
		/// </summary>
		/// <value>The modifiers.</value>
		public ModifierKeys Modifiers { get; private set; }

		/// <summary>
		/// Gets a value indicating whether this instance has modifiers.
		/// </summary>
		/// <value><c>true</c> if this instance has modifiers; otherwise, <c>false</c>.</value>
		public bool HasModifiers { get { return Modifiers != ModifierKeys.None; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="Xwt.KeyboardShortcut"/> class.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <remarks>Capital letters imply the Shift modifier</remarks>
		public KeyboardShortcut (Key key)
		{
			Key = key;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xwt.KeyboardShortcut"/> class.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="modifiers">Modifiers.</param>
		public KeyboardShortcut (Key key, ModifierKeys modifiers)
		{
			Key = key;
			Modifiers |= modifiers;
		}
	}
}

