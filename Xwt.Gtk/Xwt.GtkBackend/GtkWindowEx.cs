// -----------------------------------------------------------------------
// <copyright file="GtkWindowEx.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Gtk
{
	using System;

	/// <summary>
	/// Add properties for AcceleratorGroup and ActionGroup to Gtk.Window
	/// </summary>
	public class WindowEx : Window
	{
		public AccelGroup DefualtAccelGroup { get; private set; }
		public ActionGroup DefaultActionGroup { get; private set; }

		public WindowEx (string title) : base (title) {
			DefualtAccelGroup = new AccelGroup ();
			AddAccelGroup (DefualtAccelGroup);
			DefaultActionGroup = new ActionGroup ("Default");
		}
	}
}
