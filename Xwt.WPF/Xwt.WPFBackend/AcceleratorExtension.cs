// -----------------------------------------------------------------------
// <copyright file="AcceleratorExtension.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Xwt.WPFBackend
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Extension Methods for Accelerator
	/// </summary>
	public static class AcceleratorExtension
	{
		public static string ToInputGesture(this Accelerator accelerator)
		{
			if (accelerator == null)
				return null;
			var text = string.Empty;
			if (accelerator.HasModifiers) {
				if (accelerator.Modifiers.Value.HasFlag (ModifierKeys.Control))
					text += "Ctrl+";
				if (accelerator.Modifiers.Value.HasFlag (ModifierKeys.Shift))
					text += "Shift+";
			}
			switch (accelerator.Key) {
				default:
					text += accelerator.Key.ToString ();
					break;
			}
			return text;
		}
	}
}
