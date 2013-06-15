//
// GtkMacInterop.cs
//
// Author:
//       David Lechner <david@lechnology.com>
//
// Copyright (c) 2013 David Lechner
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;

namespace Xwt.GtkBackend
{
	using System;

	/// <summary>
	/// Add properties for AcceleratorGroup and ActionGroup to Gtk.Window
	/// </summary>
	public static class GtkWindow
	{
		static Dictionary<Gtk.Window, Gtk.AccelGroup> defualtAccelGroups;
		static Dictionary<Gtk.Window, Gtk.ActionGroup> defaultActionGroups;

		static GtkWindow ()
		{
			defualtAccelGroups = new Dictionary<Gtk.Window, Gtk.AccelGroup> ();
			defaultActionGroups = new Dictionary<Gtk.Window, Gtk.ActionGroup> ();
		}

		public static Gtk.AccelGroup GetDefaultAccelGroup(this Gtk.Window window)
		{
			if (!defualtAccelGroups.ContainsKey (window)) {
				var accelGroup = new Gtk.AccelGroup ();
				window.AddAccelGroup (accelGroup);
				defualtAccelGroups.Add (window, accelGroup);
			}
			return defualtAccelGroups [window];
		}

		public static Gtk.ActionGroup GetDefaultActionGroup(this Gtk.Window window)
		{
			if (!defaultActionGroups.ContainsKey (window)) {
				defaultActionGroups.Add (window, new Gtk.ActionGroup ("Default"));
			}
			return defaultActionGroups [window];
		}
	}
}
