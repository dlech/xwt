// -----------------------------------------------------------------------
// <copyright file="StockCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Xwt
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public enum GlobalCommand
	{
		Add,
		Apply,
		Cancel,
		Clear,
		Close,
		Copy,
		Cut,
		Delete,
		DeselectAll,
		Duplicate,
		Export,
		Find,
		FindNext,
		FindPrevious,
		Import,
		New,
		No,
		Ok,
		Open,
		Preferences,
		Print,
		PageSetup,
		Paste,
		PasteSpecial,
		PrintPreview,
		Properties,
		Quit,
		Redo,
		Remove,
		Replace,
		Revert,
		Save,
		SaveAs,
		SaveCopy,
		SelectAll,
		SendTo,
		Stop,
		Undo,
		Yes
	}

	public static class GlobalCommandExt
	{
		static Dictionary<GlobalCommand, Command> commands;

		static GlobalCommandExt ()
		{
			commands = new Dictionary<GlobalCommand, Command> ();
		}

		public static Command Get(this GlobalCommand command)
		{
			if (!commands.ContainsKey (command)) {
				commands.Add (command, new Command (command));
			}
			return commands[command];
		}
	}
}
