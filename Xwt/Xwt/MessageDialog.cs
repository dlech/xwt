// 
// MessageDialog.cs
//  
// Author:
//       Lluis Sanchez <lluis@xamarin.com>
// 
// Copyright (c) 2011 Xamarin Inc
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
using Xwt.Backends;


namespace Xwt
{
	public static class MessageDialog
	{
		public static WindowFrame RootWindow { get; set; }
		
		#region ShowError
		public static void ShowError (string primaryText)
		{
			ShowError (RootWindow, primaryText);
		}
		public static void ShowError (WindowFrame parent, string primaryText)
		{
			ShowError (parent, primaryText, null);
		}
		public static void ShowError (string primaryText, string secondaryText)
		{
			ShowError (RootWindow, primaryText, secondaryText);
		}
		public static void ShowError (WindowFrame parent, string primaryText, string secondaryText)
		{
			GenericAlert (parent, StockIcons.Error, primaryText, secondaryText, new Command (StockCommand.Ok));
		}
		#endregion
		
		#region ShowWarning
		public static void ShowWarning (string primaryText)
		{
			ShowWarning (RootWindow, primaryText);
		}
		public static void ShowWarning (WindowFrame parent, string primaryText)
		{
			ShowWarning (parent, primaryText, null);
		}
		public static void ShowWarning (string primaryText, string secondaryText)
		{
			ShowWarning (RootWindow, primaryText, secondaryText);
		}
		public static void ShowWarning (WindowFrame parent, string primaryText, string secondaryText)
		{
			GenericAlert (parent, StockIcons.Warning, primaryText, secondaryText, new Command (StockCommand.Ok));
		}
		#endregion
		
		#region ShowMessage
		public static void ShowMessage (string primaryText)
		{
			ShowMessage (RootWindow, primaryText);
		}
		public static void ShowMessage (WindowFrame parent, string primaryText)
		{
			ShowMessage (parent, primaryText, null);
		}
		public static void ShowMessage (string primaryText, string secondaryText)
		{
			ShowMessage (RootWindow, primaryText, secondaryText);
		}
		public static void ShowMessage (WindowFrame parent, string primaryText, string secondaryText)
		{
			GenericAlert (parent, StockIcons.Information, primaryText, secondaryText, new Command (StockCommand.Ok));
		}
		#endregion

		#region Confirm

		/// <summary>
		/// Displays a MessageDialog for user confirmation.
		/// </summary>
		/// <param name="primaryText">
		/// The primary message to display to the user. Generally, a short title.
		/// </param>
		/// <returns>True if the user clicks the OK button</returns>
		/// <remarks>
		/// The MessageDialog will have a Cancel button and an OK button.
		/// The OK button will be selected as the default button.
		/// </remarks>
		public static bool Confirm (string primaryText)
		{
			return Confirm (primaryText, (string)null);
		}

		/// <summary>
		/// Displays a MessageDialog for user confirmation.
		/// </summary>
		/// <param name="primaryText">
		/// The primary message to display to the user. Generally, a short title.
		/// </param>
		/// <returns>True if the user clicks the OK button</returns>
		/// <remarks>
		/// The MessageDialog will have a Cancel button and an OK button.
		/// The OK button will be selected as the default button.
		/// </remarks>
		public static bool Confirm (string primaryText, string secondaryText)
		{
			return Confirm (primaryText, secondaryText, new Command (StockCommand.Ok));
		}

		/// <summary>
		/// Displays a MessageDialog for user confirmation.
		/// </summary>
		/// <param name="primaryText">
		/// The primary message to display to the user. Generally, a short title.
		/// </param>
		/// <param name="confirmCommand">
		/// The <see cref="Command"/> that is activated when the user responds in the affirmative.
		/// A button will be created using the properties of the Command
		/// </param>
		/// <returns>True if the user clicks the confirm button</returns>
		/// <remarks>
		/// The MessageDialog will have a Cancel button in addition to the user specified
		/// confirm button. The confirm button will be selected as the default button.
		/// </remarks>
		public static bool Confirm (string primaryText, Command confirmCommand)
		{
			return Confirm (primaryText, null, confirmCommand);
		}

		/// <summary>
		/// Displays a MessageDialog for user confirmation.
		/// </summary>
		/// <param name="primaryText">
		/// The primary message to display to the user. Generally, a short title.
		/// </param>
		/// <param name="secondaryText">
		/// The secondary message to display to the user. Provides more descriptive
		/// information than the primary message.
		/// </param>
		/// <param name="confirmCommand">
		/// The <see cref="Command"/> that is activated when the user responds in the affirmative.
		/// A button will be created using the properties of the Command
		/// </param>
		/// <returns>True if the user clicks the confirm button</returns>
		/// <remarks>
		/// The MessageDialog will have a Cancel button in addition to the user specified
		/// confirm button. The confirm button will be selected as the default button.
		/// </remarks>
		public static bool Confirm (string primaryText, string secondaryText, Command confirmCommand)
		{
			var result = GenericAlert (RootWindow, StockIcons.Question, primaryText, secondaryText,
			                           new Command(StockCommand.Cancel), confirmCommand);
			return ReferenceEquals (result, confirmCommand);
		}

		/// <summary>
		/// Displays a MessageDialog for user confirmation.
		/// </summary>
		/// <param name="primaryText">
		/// The primary message to display to the user. Generally, a short title.
		/// </param>
		/// <param name="confirmCommand">
		/// The <see cref="Command"/> that is activated when the user responds in the affirmative.
		/// A button will be created using the properties of the Command
		/// </param>
		/// <param name="confirmIsDefault">
		/// When true, the confirm button will be the default button. When false, the
		/// Cancel button will be the default button.
		/// </param>
		/// <returns>True if the user clicks the confirm button</returns>
		/// <remarks>
		/// The MessageDialog will have a Cancel button in addition to the user specified
		/// confirm button.
		/// </remarks>
		public static bool Confirm (string primaryText, Command confirmCommand, bool confirmIsDefault)
		{
			return Confirm (primaryText, null, confirmCommand, confirmIsDefault);
		}

		/// <summary>
		/// Displays a MessageDialog for user confirmation.
		/// </summary>
		/// <param name="primaryText">
		/// The primary message to display to the user. Generally, a short title.
		/// </param>
		/// <param name="secondaryText">
		/// The secondary message to display to the user. Provides more descriptive
		/// information than the primary message.
		/// </param>
		/// <param name="confirmCommand">
		/// The <see cref="Command"/> that is activated when the user responds in the affirmative.
		/// A button will be created using the properties of the Command
		/// </param>
		/// <param name="confirmIsDefault">
		/// When true, the confirm button will be the default button. When false, the
		/// Cancel button will be the default button.
		/// </param>
		/// <returns>True if the user clicks the confirm button</returns>
		/// <remarks>
		/// The MessageDialog will have a Cancel button in addition to the user specified
		/// confirm button.
		/// </remarks>
		public static bool Confirm (string primaryText, string secondaryText, Command confirmCommand, bool confirmIsDefault)
		{
			var result = GenericAlert (RootWindow, StockIcons.Question, primaryText, secondaryText,
			                           confirmIsDefault ? 0 : 1, new Command (StockCommand.Cancel), confirmCommand);
			return ReferenceEquals (result, confirmCommand);
		}
		
		/// <summary>
		/// Displays a MessageDialog for user confirmation.
		/// </summary>
		/// <param name="message">
		/// The message to display
		/// </param>
		/// <returns>
		/// True if the user clicks the confirm button specified by the message.
		/// </returns>
		public static bool Confirm (ConfirmationMessage message)
		{
			return GenericAlert (RootWindow, message) == message.ConfirmCommand;
		}
		#endregion

		#region AskQuestion
		public static Command AskQuestion (string primaryText, params Command[] buttons)
		{
			return AskQuestion (primaryText, null, buttons);
		}
		
		public static Command AskQuestion (string primaryText, string secondaryText, params Command[] buttons)
		{
			return GenericAlert (RootWindow, StockIcons.Question, primaryText, secondaryText, buttons);
		}
		public static Command AskQuestion (string primaryText, int defaultButton, params Command[] buttons)
		{
			return AskQuestion (primaryText, null, defaultButton, buttons);
		}
		
		public static Command AskQuestion (string primaryText, string secondaryText, int defaultButton, params Command[] buttons)
		{
			return GenericAlert (RootWindow, StockIcons.Question, primaryText, secondaryText, defaultButton, buttons);
		}
		
		public static Command AskQuestion (QuestionMessage message)
		{
			return GenericAlert (RootWindow, message);
		}
		#endregion
		
		static Command GenericAlert (WindowFrame parent, Xwt.Drawing.Image icon, string primaryText, string secondaryText, params Command[] commands)
		{
			return GenericAlert (parent, icon, primaryText, secondaryText, commands.Length - 1, commands);
		}
		
		static Command GenericAlert (WindowFrame parent, Xwt.Drawing.Image icon, string primaryText, string secondaryText, int defaultCommand, params Command[] commands)
		{
			GenericMessage message = new GenericMessage () {
				Icon = icon,
				Text = primaryText,
				SecondaryText = secondaryText,
				DefaultCommandIndex = defaultCommand
			};
			foreach (Command command in commands)
				message.Commands.Add (command);
			
			return GenericAlert (parent, message);
		}
		
		static Command GenericAlert (WindowFrame parent, MessageDescription message)
		{
			if (message.ApplyToAllCommand != null)
				return message.ApplyToAllCommand;

			IAlertDialogBackend backend = Toolkit.CurrentEngine.Backend.CreateBackend<IAlertDialogBackend> ();
			backend.Initialize (Toolkit.CurrentEngine.Context);

			using (backend) {
				var res = backend.Run (parent ?? RootWindow, message);
				
				if (backend.ApplyToAll)
					message.ApplyToAllCommand = res;
				
				return res;
			}
		}
	}
	
	public class MessageDescription
	{
		internal MessageDescription ()
		{
			DefaultCommandIndex = -1;
			ButtonCommands = new List<Command> ();
			Options = new List<AlertOption> ();
		}
		
		public IList<Command> ButtonCommands { get; private set; }
		public IList<AlertOption> Options { get; private set; }
		
		internal Command ApplyToAllCommand { get; set; }

		/// <summary>
		/// CancelCommand will be activated if the MessageDialog is closed instead
		/// of clicking a button
		/// </summary>
		public Command CancelCommand { get; set; }

		public Xwt.Drawing.Image Icon { get; set; }
		
		public string Text { get; set; }
		public string SecondaryText { get; set; }
		public bool AllowApplyToAll { get; set; }
		public int DefaultCommandIndex { get; set; }
		
		public void AddOption (string id, string text, bool setByDefault)
		{
			Options.Add (new AlertOption (id, text) { Value = setByDefault });
		}
		
		public bool GetOptionValue (string id)
		{
			foreach (var op in Options)
				if (op.Id == id)
					return op.Value;
			throw new ArgumentException ("Invalid option id");
		}

		public void SetOptionValue (string id, bool value)
		{
			foreach (var op in Options) {
				if (op.Id == id) {
					op.Value = value;
					return;
				}
			}
			throw new ArgumentException ("Invalid option id");
		}
	}
	
	public class AlertOption
	{
		internal AlertOption (string id, string text)
		{
			this.Id = id;
			this.Text = text;
		}

		public string Id { get; private set; }
		public string Text { get; private set; }
		public bool Value { get; set; }
	}
	
	public sealed class GenericMessage: MessageDescription
	{
		public GenericMessage ()
		{
		}
		
		public GenericMessage (string text)
		{
			Text = text;
		}
		
		public GenericMessage (string text, string secondaryText): this (text)
		{
			SecondaryText = secondaryText;
		}
		
		public new IList<Command> Commands {
			get { return base.ButtonCommands; }
		}
	}
	
	
	public sealed class QuestionMessage: MessageDescription
	{
		public QuestionMessage ()
		{
			Icon = StockIcons.Question;
		}
		
		public QuestionMessage (string text): this ()
		{
			Text = text;
		}
		
		public QuestionMessage (string text, string secondaryText): this (text)
		{
			SecondaryText = secondaryText;
		}
		
		public new IList<Command> Commands {
			get { return base.ButtonCommands; }
		}
	}
	
	public sealed class ConfirmationMessage: MessageDescription
	{
		Command confirmCommand;
		
		public ConfirmationMessage ()
		{
			Icon = StockIcons.Question;
			var cancelCommand = new Command (StockCommand.Cancel);
			ButtonCommands.Add (cancelCommand);
			CancelCommand = cancelCommand;
		}
		
		public ConfirmationMessage (Command confirmCommand): this ()
		{
			ConfirmCommand = confirmCommand;
		}
		
		public ConfirmationMessage (string primaryText, Command confirmCommand): this (confirmCommand)
		{
			Text = primaryText;
		}
		
		public ConfirmationMessage (string primaryText, string secondaryText, Command confirmCommand)
			: this (primaryText, confirmCommand)
		{
			SecondaryText = secondaryText;
		}
		
		public Command ConfirmCommand {
			get { return confirmCommand; }
			set {
				if (ButtonCommands.Count == 2)
					ButtonCommands.RemoveAt (1);
				ButtonCommands.Add (value);
				confirmCommand = value;
			}
		}
		
		public bool ConfirmIsDefault {
			get {
				return DefaultCommandIndex == 1;
			}
			set {
				if (value)
					DefaultCommandIndex = 1;
				else
					DefaultCommandIndex = 0;
			}
		}
	}
}

