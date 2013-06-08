using System;
using Xwt.Backends;

namespace Xwt.GtkBackend
{
	public class ActionBackend : IActionBackend
	{
		Gtk.Action action;

		#region IActionBackend implementation

		public ActionBackend (string name, string label)
		{
			action = new Gtk.Action (name, label);
		}

		public string Name {
			get {
				return action.Name;
			}
		}

		public string Label {
			get {
				return action.Label;
			}
			set {
				action.Label = value;
			}
		}

		public Accelerator Accelerator {
			get {
				return
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public string ToolTip {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public string ToolbarLabel {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public bool Visible {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public bool Sensitive {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		#endregion
	}
}

