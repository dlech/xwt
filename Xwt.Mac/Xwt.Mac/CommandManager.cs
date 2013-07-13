//
// CommandManager.cs
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
using System.Reflection;
using MonoMac.Foundation;
using MonoMac.ObjCRuntime;
using Xwt.Backends;

namespace Xwt.Mac
{
	public class CommandManager
	{
		static CommandHandlerCollection handlers =
			new CommandHandlerCollection ();

		public static CommandHandlerCollection Handlers { get { return handlers; } }

		/// <summary>
		/// Inspects frontend for methods with <see cref="Xwt.CommandHandlerAttribute"/> and binds those methods to the backend
		/// </summary>
		/// <param name="frontend">Frontend.</param>
		/// <param name="backend">Backend - see remarks.</param>
		/// <remarks>
		/// Backend must contain the method
		/// <example>
		/// public void OnCommandActivated(NSObject sender)
		/// {
		/// 	CommandManager.Handlers.Invoke (sender, this);
		/// }
		/// </example>
		/// </remarks>
		public static void AddCommandHandlers(XwtUiComponent frontend, NSObject backend)
		{
			var frontendType = frontend.GetType ();
			foreach (var method in frontendType.GetMethods ()) {
				// have to copy reference from indexer so that we can use it in later in anonymous method 
				var methodRef = method;
				foreach (CommandHandlerAttribute attribute in method.GetCustomAttributes(typeof(CommandHandlerAttribute), true)) {
					var commandBackend = attribute.Command.GetBackend () as CommandBackend;
					var key = new Tuple<NSObject, string> (backend, commandBackend.action.Name);
					if (Handlers.ContainsKey (key))
						throw new ArgumentException ("command handler already exists");
					var methodInfo = backend.GetType ().GetMethod ("OnCommandActivated");
					if (methodInfo == null)
						throw new ArgumentException ("backend must have public method OnCommandActivated(NSObject)");
					Action<NSObject> nativeHandler = (sender) => {
						methodRef.Invoke (frontend, null);
					};
					Runtime.ConnectMethod (methodInfo, commandBackend.action);
					Handlers.Add (backend, commandBackend.action, nativeHandler);
				}
			}
		}
	}

	public class CommandHandlerCollection : Dictionary<Tuple<NSObject, string>, Action<NSObject>>
	{
		/// <summary>
		/// Add the specified object, selector and method.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="selector">Selector.</param>
		/// <param name="method">Method.</param>
		public void Add(NSObject obj, Selector selector, Action<NSObject> method)
		{
			Add (new Tuple<NSObject, string> (obj, selector.Name), method);
		}

		/// <summary>
		/// Invokes the selector specified by the senders action on the target
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="target">Target object</param>
		/// <remarks>
		/// Fails silently if the action does not exist on the target
		/// </remarks>
		public void Invoke(NSObject sender, NSObject target)
		{
			Action<NSObject> method;
			var senderType = sender.GetType ();
			var senderActionProperty = senderType.GetProperty ("Action");
			if (senderActionProperty == null)
				return;
			var senderAction = senderActionProperty.GetValue (sender, null) as Selector;
			if (!TryGetValue(new Tuple<NSObject, string> (target, senderAction.Name), out method))
			    return;
			method.Invoke (sender);
		}
	}
}

