//
// StockCommandId.cs
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

namespace Xwt
{
	/// <summary>
	/// Global instances of Stock Commands
	/// </summary>
	public static class StockCommands
	{
		public static Command About { get { return Command.GetCommandForId(StockCommandId.About); } }
		public static Command Add { get { return Command.GetCommandForId(StockCommandId.Add); } }
		public static Command Apply { get { return Command.GetCommandForId(StockCommandId.Apply); } }
		public static Command Cancel { get { return Command.GetCommandForId(StockCommandId.Cancel); } }
		public static Command Clear { get { return Command.GetCommandForId(StockCommandId.Clear); } }
		public static Command Close { get { return Command.GetCommandForId(StockCommandId.Close); } }
		public static Command CloseAll { get { return Command.GetCommandForId(StockCommandId.CloseAll); } }
		public static Command Copy { get { return Command.GetCommandForId(StockCommandId.Copy); } }
		public static Command Cut { get { return Command.GetCommandForId(StockCommandId.Cut); } }
		public static Command Delete { get { return Command.GetCommandForId(StockCommandId.Delete); } }
		public static Command DeselectAll { get { return Command.GetCommandForId(StockCommandId.DeselectAll); } }
		public static Command Duplicate { get { return Command.GetCommandForId(StockCommandId.Duplicate); } }
		public static Command Export { get { return Command.GetCommandForId(StockCommandId.Export); } }
		public static Command Find { get { return Command.GetCommandForId(StockCommandId.Find); } }
		public static Command FindNext { get { return Command.GetCommandForId(StockCommandId.FindNext); } }
		public static Command FindPrevious { get { return Command.GetCommandForId(StockCommandId.FindPrevious); } }
		public static Command Help { get { return Command.GetCommandForId(StockCommandId.Help); } }
		public static Command HideApplication { get { return Command.GetCommandForId(StockCommandId.HideApplication); } }
		public static Command HideOtherApplications { get { return Command.GetCommandForId(StockCommandId.HideOtherApplications); } }
		public static Command Import { get { return Command.GetCommandForId(StockCommandId.Import); } }
		public static Command Maximize { get { return Command.GetCommandForId(StockCommandId.Maximize); } }
		public static Command Minimize { get { return Command.GetCommandForId(StockCommandId.Minimize); } }
		public static Command New { get { return Command.GetCommandForId(StockCommandId.New); } }
		public static Command No { get { return Command.GetCommandForId(StockCommandId.No); } }
		public static Command Ok { get { return Command.GetCommandForId(StockCommandId.Ok); } }
		public static Command Open { get { return Command.GetCommandForId(StockCommandId.Open); } }
		public static Command PageSetup { get { return Command.GetCommandForId(StockCommandId.PageSetup); } }
		public static Command Paste { get { return Command.GetCommandForId(StockCommandId.Paste); } }
		public static Command PasteAsText { get { return Command.GetCommandForId(StockCommandId.PasteAsText); } }
		public static Command Preferences { get { return Command.GetCommandForId(StockCommandId.Preferences); } }
		public static Command Print { get { return Command.GetCommandForId(StockCommandId.Print); } }
		public static Command PrintPreview { get { return Command.GetCommandForId(StockCommandId.PrintPreview); } }
		public static Command Properties { get { return Command.GetCommandForId(StockCommandId.Properties); } }
		public static Command Quit { get { return Command.GetCommandForId(StockCommandId.Quit); } }
		public static Command Redo { get { return Command.GetCommandForId(StockCommandId.Redo); } }
		public static Command Remove { get { return Command.GetCommandForId(StockCommandId.Remove); } }
		public static Command Replace { get { return Command.GetCommandForId(StockCommandId.Replace); } }
		public static Command Revert { get { return Command.GetCommandForId(StockCommandId.Revert); } }
		public static Command Save { get { return Command.GetCommandForId(StockCommandId.Save); } }
		public static Command SaveAll { get { return Command.GetCommandForId(StockCommandId.SaveAll); } }
		public static Command SaveAs { get { return Command.GetCommandForId(StockCommandId.SaveAs); } }
		public static Command SaveCopy { get { return Command.GetCommandForId(StockCommandId.SaveCopy); } }
		public static Command SelectAll { get { return Command.GetCommandForId(StockCommandId.SelectAll); } }
		public static Command SendTo { get { return Command.GetCommandForId(StockCommandId.SendTo); } }
		public static Command Stop { get { return Command.GetCommandForId(StockCommandId.Stop); } }
		public static Command Undo { get { return Command.GetCommandForId(StockCommandId.Undo); } }
		public static Command UnhideAllApplications { get { return Command.GetCommandForId(StockCommandId.UnhideAllApplications); } }
		public static Command Yes { get { return Command.GetCommandForId(StockCommandId.Yes); } }
	}
}
