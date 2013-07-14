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
		static CommandHandlerCollection<Action<NSObject>> activationHandlers =
			new CommandHandlerCollection<Action<NSObject>> ();
		static CommandHandlerCollection<Func<NSObject, bool>> statusRequestHandlers =
			new CommandHandlerCollection<Func<NSObject, bool>> ();

		public static CommandHandlerCollection<Action<NSObject>> ActivationHandlers { get { return activationHandlers; } }

		public static CommandHandlerCollection<Func<NSObject, bool>> StatusRequestHandlers { get { return statusRequestHandlers; } }

		/// <summary>
		/// Inspects frontend for methods with <see cref="Xwt.CommandHandlerAttribute"/> and binds those methods to the backend
		/// </summary>
		/// <param name="frontend">Frontend.</param>
		/// <param name="backend">Backend. Must also implement <see cref="Xwt.Mac.IViewObject"/></param>
		/// <remarks>
		/// Backend must contain these methods
		/// <example>
		/// public void OnCommandActivated (NSObject sender)
		/// {
		/// 	CommandManager.Handlers.Invoke (sender, this);
		/// }
		/// 
		/// [Export("validateUserInterfaceItem:")]
		/// public bool ValidateUserInterfaceItem (NSObject item)
		/// {
		/// 	return CommandManager.StatusRequestHandlers.Invoke (item, this);
		/// }
		/// </example>
		/// </remarks>
		public static void AddCommandHandlers (XwtUiComponent frontend, NSObject backend)
		{
			var frontendType = frontend.GetType ();
			foreach (var method in frontendType.GetMethods ()) {
				// have to copy reference from indexer so that we can use it in later in anonymous method 
				var methodRef = method;
				foreach (CommandHandlerAttribute attribute in method.GetCustomAttributes(typeof(CommandHandlerAttribute), true)) {
					var commandBackend = attribute.Command.GetBackend () as CommandBackend;
					var key = new Tuple<NSObject, string> (backend, commandBackend.action.Name);
					if (ActivationHandlers.ContainsKey (key))
						throw new ArgumentException ("command handler already exists");
					var methodInfo = backend.GetType ().GetMethod ("OnCommandActivated");
					if (methodInfo == null)
						throw new ArgumentException ("backend must have public method void OnCommandActivated(NSObject)");
					Action<NSObject> nativeHandler = (sender) => {
						methodRef.Invoke (frontend, null);
					};
					Runtime.ConnectMethod (methodInfo, commandBackend.action);
					ActivationHandlers.Add (backend, commandBackend.action, nativeHandler);
				}
				foreach (CommandStatusRequestHandlerAttribute attribute in method.GetCustomAttributes(typeof(CommandStatusRequestHandlerAttribute), true)) {
					var commandBackend = attribute.Command.GetBackend () as CommandBackend;
					var key = new Tuple<NSObject, string> (backend, commandBackend.action.Name);
					if (StatusRequestHandlers.ContainsKey (key))
						throw new ArgumentException ("command handler already exists");
					var methodInfo = backend.GetType ().GetMethod ("OnCommandActivated");
					if (methodInfo == null)
						throw new ArgumentException ("backend must have public method void OnCommandActivated(NSObject)");
					Func<NSObject, bool> nativeHandler = (sender) => {
						return (bool)methodRef.Invoke (frontend, null);
					};
					StatusRequestHandlers.Add (backend, commandBackend.action, nativeHandler);
				}
			}
		}
	}

	public class CommandHandlerCollection<T> : Dictionary<Tuple<NSObject, string>, T>
	{
		/// <summary>
		/// Add the specified object, selector and method.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="selector">Selector.</param>
		/// <param name="method">Method.</param>
		public void Add(NSObject obj, Selector selector, T method)
		{
			Add (new Tuple<NSObject, string> (obj, selector.Name), method);
		}
	}

	public static class CommandHandlerCollectionExt
	{
		/// <summary>
		/// Invokes the selector specified by the senders action on the target
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="target">Target object</param>
		/// <remarks>
		/// Fails silently if the action does not exist on the target
		/// </remarks>
		public static void Invoke (this CommandHandlerCollection<Action<NSObject>> collection, NSObject sender, NSObject target)
		{
			Action<NSObject> method;
			var senderType = sender.GetType ();
			var senderActionProperty = senderType.GetProperty ("Action");
			if (senderActionProperty == null)
				return;
			var senderAction = senderActionProperty.GetValue (sender, null) as Selector;
			if (!collection.TryGetValue(new Tuple<NSObject, string> (target, senderAction.Name), out method))
				return;
			method.Invoke (sender);
		}

		/// <summary>
		/// Responds to an user item validation request
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="target">Target object</param>
		/// <remarks>
		/// Returns true if the action does not exist on the target
		/// </remarks>
		public static bool Invoke (this CommandHandlerCollection<Func<NSObject, bool>> collection, NSObject sender, NSObject target)
		{
			Func<NSObject, bool> method;
			var senderType = sender.GetType ();
			var senderActionProperty = senderType.GetProperty ("Action");
			if (senderActionProperty == null)
				return true;
			var senderAction = senderActionProperty.GetValue (sender, null) as Selector;
			if (!collection.TryGetValue(new Tuple<NSObject, string> (target, senderAction.Name), out method))
				return true;
			return method.Invoke (sender);
		}
	}
}

