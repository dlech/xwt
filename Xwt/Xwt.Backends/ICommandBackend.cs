// -----------------------------------------------------------------------
// <copyright file="ICommandBackend.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Xwt.Backends
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Predefined common Command objects
	/// </summary>
	/// <remarks>
	/// Default implementation is <see cref="Xwt.Backend.CommandBackend"/>
	/// Backend implementations should override as necessary to provide platform
	/// specific lables and keyboard shortcuts
	/// </remarks>
	public interface ICommandBackend : IBackend
	{
		string Label { get; set; }
		string ToolTip { get; set; }
		KeyboardShortcutSequence DefaultKeyboardShortcut { get; set; }
		bool Sensitive { get; set; }
		bool Visible { get; set; }

		void Initalize (ICommandEventSink backendHost);

		IMenuItemBackend CreateMenuItem ();
		IButtonBackend CreateButton ();
	}

	public interface ICommandEventSink
	{
		void OnSensitiveChanged ();
	}
}
