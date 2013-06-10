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
	[BackendType(typeof(ICommandManagerBackend))]
	public class CommandManager : XwtObject
	{
		CommandCollection commands;

		public CommandCollection Commands
		{
			get { return commands; }
		}

		public CommandManager ()
		{
			commands = new CommandCollection (Backend.Listener);
		}

		new ICommandManagerBackend Backend { 
			get { return(ICommandManagerBackend)base.Backend; }
		}

		protected override object OnCreateBackend ()
		{
			return ToolkitEngine.Backend.CreateBackend<ICommandManagerBackend> ();
		}
	}
}
