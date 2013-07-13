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
		/// <remarks>
		/// If the key is an uppercase character (A-Z), then the shift modifier will be
		/// added automatically. OS X does this anyway, so we do it too to keep things
		/// consistant cross-platform.
		/// </remarks>
		public Key Key {
			get { return key; }
			private set
			{ 
				key = value;
				if (key >= Key.A && key <= Key.Z)
					Modifiers |= ModifierKeys.Shift;
			}
		}

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

