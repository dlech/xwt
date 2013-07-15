///
// CommandHandlerBackend.cs
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
using System.Linq;
using System.Text;
using Xwt.Commands;

namespace Xwt.Backends
{


	/// <summary>
	/// Default implementation of <see cref="ICommandBackend"/>
	/// </summary>
	public abstract class CommandBackend : ICommandBackend
	{
		ICommandEventSink eventSink;
		ApplicationContext context;

		public virtual void Initalize (ICommandEventSink eventSink)
		{
			this.eventSink = eventSink;

			// TODO: this typecast is a hack to access other objects
			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			var command = backendHost.Parent;

			Label = command.ShortId;
			// TODO : add Name and ShortName property to Xwt.Application
			var appName = System.Diagnostics.Process.GetCurrentProcess ().ProcessName;

			IterateCommandAttribute<DefaultKeyboardShortcutAttribute> (command, (attribute) =>
			{
				if (attribute.DesktopType.HasFlag (Desktop.DesktopType)) 
					DefaultKeyboardShortcut = attribute.Shortcut;
			});
			IterateCommandAttribute<LabelAttribute> (command, (attribute) =>
			{
				if (attribute.DesktopType.HasFlag (Desktop.DesktopType)) 
					Label = string.Format(attribute.Label, appName);
			});
		}

		protected void IterateCommandAttribute<T>(Command command, Action<T> action)
		{
			var commandTypeName = command.Id.Substring(0, command.Id.LastIndexOf('.'));
			var commandType = Type.GetType (commandTypeName, false);
			if (commandType != null) {
				var commandField = commandType.GetField (command.ShortId);
				if (commandField != null) {
					var attributeList = commandField.GetCustomAttributes (typeof(T), true);
					foreach (T shortcutAttribute in attributeList) {
						action.Invoke (shortcutAttribute);
					}
				}
			}
		}

		public virtual string Label { get; set; }

		public virtual string ToolTip { get; set; }

		public virtual KeyboardShortcutSequence DefaultKeyboardShortcut { get; set; }

		public virtual bool Sensitive { get; set; }

		public virtual bool Visible { get; set; }

		public abstract IMenuItemBackend CreateMenuItem ();
		public abstract IButtonBackend CreateButton ();

		public virtual void InitializeBackend(object frontend, ApplicationContext context)
		{
			this.context = context;
		}

		public virtual void EnableEvent(object eventId)
		{
		}

		public void DisableEvent(object eventId)
		{
		}
	}
}
