// -----------------------------------------------------------------------
// <copyright file="Command_Manager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Xwt
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Xwt.Backends;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public interface ICommandTarget
	{
		CommandCollection Commands { get; }
	}
}
