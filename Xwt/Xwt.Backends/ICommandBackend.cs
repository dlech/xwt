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
	/// specific names and keyboard shortcuts
	/// </remarks>
	public interface ICommandBackend : IBackend
	{
		void Initalize (ICommandEventSink backendHost);

		IMenuItemBackend CreateMenuItem ();
		IButtonBackend CreateButton ();
	}

	public interface ICommandEventSink
	{
		void OnActivated ();
		void OnSensitiveChanged ();
	}

	public enum CommandEvent
	{
		Activated = 1,
		SensitiveChanged
	}
}
