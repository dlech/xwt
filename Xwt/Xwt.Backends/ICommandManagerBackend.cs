using Xwt.Backends;
// -----------------------------------------------------------------------
// <copyright file="ICommandManager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Xwt.Backends
{
	using System;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public interface ICommandManagerBackend : IBackend
	{
		ICollectionListener Listener { get; }
	}
}
