// -----------------------------------------------------------------------
// <copyright file="WindowStartPosition.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Xwt
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Position hint for window when it is first displayed
	/// </summary>
	public enum WindowPosition
	{
		/// <summary>
		/// The window position will be set manually
		/// </summary>
		Manual,

		/// <summary>
		/// The window will be centered on the screen
		/// </summary>
		CenterScreen,

		/// <summary>
		/// The window will be centered on the parent window
		/// </summary>
		CenterParent
	}
}
