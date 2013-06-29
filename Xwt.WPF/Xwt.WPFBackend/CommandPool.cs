﻿// 
// CommandBackend.cs
//  
// Author:
//	   David Lechner <david@lechnology.com>
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

namespace Xwt.WPFBackend
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using SWI = System.Windows.Input;

	/// <summary>
	/// Global pool of Commands. Used to bind commands to windows. Needed to make 
	/// accelerator keys work
	/// </summary>
	public static class CommandPool
	{
		public static BindingList<CommandBackend> Commands { get; private set; }

		static CommandPool()
		{
			Commands = new BindingList<CommandBackend> ();
			Commands.ListChanged += OnCommandListChanged;
			Application.WindowAdded += OnApplicatoinWindowAdded;
		}

		static void OnApplicatoinWindowAdded (Application.WindowEventArgs e)
		{
			foreach (var command in Commands)
				AddCommandBinding (e.Window, command);
		}

		static void AddCommandBinding(WindowFrame window, CommandBackend command)
		{
			var backend = Toolkit.GetBackend (window) as WindowFrameBackend;
			backend.Window.CommandBindings.Add (command.CommandBinding);
		}

		static void OnCommandListChanged(object sender, ListChangedEventArgs e)
		{
			WindowFrameBackend backend;
			CommandBackend command;

			switch (e.ListChangedType) {
				case ListChangedType.ItemAdded:
					foreach(var window in Application.Windows) {
						command = Commands[e.NewIndex];
						AddCommandBinding (window, command);
					}
					break;
				case ListChangedType.ItemDeleted:
					foreach (var window in Application.Windows) {
						backend = Toolkit.GetBackend (window) as WindowFrameBackend;
						command = Commands[e.OldIndex];
						backend.Window.CommandBindings.Remove (command.CommandBinding);
					}
					break;
			}
		}
	}
}
