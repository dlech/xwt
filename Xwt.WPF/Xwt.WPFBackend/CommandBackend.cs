// 
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

using System;
using System.Collections.Generic;
using SWI = System.Windows.Input;
using Xwt.Backends;

namespace Xwt.WPFBackend
{

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class CommandBackend : Xwt.Backends.CommandBackend
	{
		Dictionary<EventHandler, SWI.ExecutedRoutedEventHandler> CommandActivatedHandlers;

		public SWI.RoutedUICommand Command { get; private set; }
		public SWI.CommandBinding CommandBinding { get; private set; }
		
		public CommandBackend ()
		{
			CommandActivatedHandlers = new Dictionary<EventHandler, SWI.ExecutedRoutedEventHandler> ();
		}

		public override void Initalize (ICommandEventSink eventSink)
		{
			base.Initalize (eventSink);

			var backendHost = eventSink as BackendHost<Command, ICommandBackend>;
			var frontendCommand = backendHost.Parent;
			if (frontendCommand.IsStockCommand) {
				switch (frontendCommand.StockCommand) {
					case StockCommandId.Close:
						Command = SWI.ApplicationCommands.Close;
						break;
					case StockCommandId.Copy:
						Command = SWI.ApplicationCommands.Copy;
						break;
					case StockCommandId.Cut:
						Command = SWI.ApplicationCommands.Cut;
						break;
					case StockCommandId.Delete:
						Command = SWI.ApplicationCommands.Delete;
						break;
					case StockCommandId.Find:
						Command = SWI.ApplicationCommands.Find;
						break;
					//case GlobalCommand.Help:
					//  Command = SWI.ApplicationCommands.Help;
					//  break;
					case StockCommandId.New:
						Command = SWI.ApplicationCommands.New;
						break;
					case StockCommandId.Open:
						Command = SWI.ApplicationCommands.Open;
						break;
					case StockCommandId.Paste:
						Command = SWI.ApplicationCommands.Paste;
						break;
					case StockCommandId.Print:
						Command = SWI.ApplicationCommands.Print;
						break;
					case StockCommandId.PrintPreview:
						Command = SWI.ApplicationCommands.PrintPreview;
						break;
					case StockCommandId.Properties:
						Command = SWI.ApplicationCommands.Properties;
						break;
					case StockCommandId.Redo:
						Command = SWI.ApplicationCommands.Redo;
						break;
					case StockCommandId.Replace:
						Command = SWI.ApplicationCommands.Replace;
						break;
					case StockCommandId.Save:
						Command = SWI.ApplicationCommands.Save;
						break;
					case StockCommandId.SaveAs:
						Command = SWI.ApplicationCommands.SaveAs;
						break;
					case StockCommandId.Stop:
						Command = SWI.ApplicationCommands.Stop;
						break;
					case StockCommandId.Undo:
						Command = SWI.ApplicationCommands.Undo;
						break;
					case StockCommandId.Quit:
						frontendCommand.Label = "E_xit";
						frontendCommand.DefaultKeyboardShortcut = null;
						break;
				}
			}
			if (Command == null) {
				Command = new SWI.RoutedUICommand (frontendCommand.Label ?? string.Empty, frontendCommand.Id, frontendCommand.GetType ());
				KeyboardShortcut = frontendCommand.DefaultKeyboardShortcut;
			}
			CommandBinding = new SWI.CommandBinding (Command);
			CommandPool.Commands.Add (this);
		}

		public override IMenuItemBackend CreateMenuItem ()
		{
			var menuItemBackend = new MenuItemBackend ();
			menuItemBackend.Label = Command.Text;
			menuItemBackend.MenuItem.Command = Command;
			return menuItemBackend;
		}

		public override IButtonBackend CreateButton ()
		{
			var buttonBackend = new ButtonBackend ();
			return buttonBackend;
		}

		protected override void AddCommandActivatedHandler (EventHandler handler)
		{
			SWI.ExecutedRoutedEventHandler handlerWrapper = (sender, e) =>
			{
				handler(sender, e);
			};
			CommandActivatedHandlers.Add (handler, handlerWrapper);
			CommandBinding.Executed += handlerWrapper;
		}

		protected override void RemoveCommandActivatedHandler (EventHandler handler)
		{
			SWI.ExecutedRoutedEventHandler handlerWrapper;
			CommandActivatedHandlers.TryGetValue (handler, out handlerWrapper);
			if (handlerWrapper != null) {
				CommandBinding.Executed -= handlerWrapper;
			}
		}

		public override string Label
		{
			get
			{
				if (Command == null)
					return base.Label;
				return Command.Text;
			}
			set
			{
				if (Command == null) {
					base.Label = value;
				} else {
					Command.Text = value;
				}
			}
		}

		public override KeyboardShortcut KeyboardShortcut
		{
			get
			{
				return base.KeyboardShortcut;
			}
			set
			{
				base.KeyboardShortcut = value;
				if (Command != null && value != null) {
					var modifiers = SWI.ModifierKeys.None;
					if (value.Modifiers.HasFlag (ModifierKeys.Command) ||
							value.Modifiers.HasFlag (ModifierKeys.Control))
						modifiers |= SWI.ModifierKeys.Control;
					if (value.Modifiers.HasFlag (ModifierKeys.Shift))
						modifiers |= SWI.ModifierKeys.Shift;
					if (value.Modifiers.HasFlag (ModifierKeys.Alt))
						modifiers |= SWI.ModifierKeys.Alt;
					Command.InputGestures.Add (new SWI.KeyGesture (ConvertKey (value.Key), modifiers));
				}
			}
		}

		SWI.Key ConvertKey(Key key)
		{
			switch (key) {
			case Key.Cancel:
				return SWI.Key.Cancel;
			case Key.BackSpace:
				return SWI.Key.Back;
			case Key.Tab:
				return SWI.Key.Tab;
			case Key.LineFeed:
				return SWI.Key.LineFeed;
			case Key.Clear:
				return SWI.Key.Clear;
			case Key.Return:
				return SWI.Key.Return;
			case Key.NumPadEnter:
				return SWI.Key.Enter;
			case Key.Pause:
				return SWI.Key.Pause;
			//case Key.CapsLock:
			//  return SWI.Key.Capital;
			case Key.CapsLock:
				return SWI.Key.CapsLock;
			//case Key:
			//  return SWI.Key.KanaMode;
			//case Key:
			//  return SWI.Key.HangulMode;
			//case Key:
			//  return SWI.Key.JunjaMode;
			//case Key:
			//  return SWI.Key.FinalMode;
			//case Key:
			//  return SWI.Key.HanjaMode;
			//case Key:
			//  return SWI.Key.KanjiMode;
			case Key.Escape:
				return SWI.Key.Escape;
			//case Key:
			//  return SWI.Key.ImeConvert;
			//case Key:
			//  return SWI.Key.ImeNonConvert;
			//case Key:
			//  return SWI.Key.ImeAccept;
			//case Key:
			//  return SWI.Key.ImeModeChange;
			case Key.Space:
				return SWI.Key.Space;
			//case Key.PageUp:
			//  return SWI.Key.Prior;
			case Key.PageUp:
				return SWI.Key.PageUp;
			//case Key.PageDown:
			//  return SWI.Key.Next;
			case Key.PageDown:
				return SWI.Key.PageDown;
			case Key.End:
				return SWI.Key.End;
			case Key.Home:
				return SWI.Key.Home;
			case Key.Left:
				return SWI.Key.Left;
			case Key.Up:
				return SWI.Key.Up;
			case Key.Right:
				return SWI.Key.Right;
			case Key.Down:
				return SWI.Key.Down;
			case Key.Select:
				return SWI.Key.Select;
			case Key.Print:
				return SWI.Key.Print;
			case Key.Execute:
				return SWI.Key.Execute;
			//case Key:
			//  return SWI.Key.Snapshot;
			case Key.SysReq:
				return SWI.Key.PrintScreen;
			case Key.Insert:
				return SWI.Key.Insert;
			case Key.Delete:
				return SWI.Key.Delete;
			case Key.Help:
				return SWI.Key.Help;
			case Key.K0:
				return SWI.Key.D0;
			case Key.K1:
				return SWI.Key.D1;
			case Key.K2:
				return SWI.Key.D2;
			case Key.K3:
				return SWI.Key.D3;
			case Key.K4:
				return SWI.Key.D4;
			case Key.K5:
				return SWI.Key.D5;
			case Key.K6:
				return SWI.Key.D6;
			case Key.K7:
				return SWI.Key.D7;
			case Key.K8:
				return SWI.Key.D8;
			case Key.K9:
				return SWI.Key.D9;
			case Key.a:
			case Key.A:
				return SWI.Key.A;
			case Key.b:
			case Key.B:
				return SWI.Key.B;
			case Key.c:
			case Key.C:
				return SWI.Key.C;
			case Key.d:
			case Key.D:
				return SWI.Key.D;
			case Key.e:
			case Key.E:
				return SWI.Key.E;
			case Key.f:
			case Key.F:
				return SWI.Key.F;
			case Key.g:
			case Key.G:
				return SWI.Key.G;
			case Key.h:
			case Key.H:
				return SWI.Key.H;
			case Key.i:
			case Key.I:
				return SWI.Key.I;
			case Key.j:
			case Key.J:
				return SWI.Key.J;
			case Key.k:
			case Key.K:
				return SWI.Key.K;
			case Key.l:
			case Key.L:
				return SWI.Key.L;
			case Key.m:
			case Key.M:
				return SWI.Key.M;
			case Key.n:
			case Key.N:
				return SWI.Key.N;
			case Key.o:
			case Key.O:
				return SWI.Key.O;
			case Key.p:
			case Key.P:
				return SWI.Key.P;
			case Key.q:
			case Key.Q:
				return SWI.Key.Q;
			case Key.r:
			case Key.R:
				return SWI.Key.R;
			case Key.s:
			case Key.S:
				return SWI.Key.S;
			case Key.t:
			case Key.T:
				return SWI.Key.T;
			case Key.u:
			case Key.U:
				return SWI.Key.U;
			case Key.v:
			case Key.V:
				return SWI.Key.V;
			case Key.w:
			case Key.W:
				return SWI.Key.W;
			case Key.x:
			case Key.X:
				return SWI.Key.X;
			case Key.y:
			case Key.Y:
				return SWI.Key.Y;
			case Key.z:
			case Key.Z:
				return SWI.Key.Z;
			case Key.SuperLeft:
				return SWI.Key.LWin;
			case Key.SuperRight:
				return SWI.Key.RWin;
			case Key.Menu:
				return SWI.Key.Apps;
			//case Key:
			//  return SWI.Key.Sleep;
			case Key.NumPad0:
				return SWI.Key.NumPad0;
			case Key.NumPad1:
				return SWI.Key.NumPad1;
			case Key.NumPad2:
				return SWI.Key.NumPad2;
			case Key.NumPad3:
				return SWI.Key.NumPad3;
			case Key.NumPad4:
				return SWI.Key.NumPad4;
			case Key.NumPad5:
				return SWI.Key.NumPad5;
			case Key.NumPad6:
				return SWI.Key.NumPad6;
			case Key.NumPad7:
				return SWI.Key.NumPad7;
			case Key.NumPad8:
				return SWI.Key.NumPad8;
			case Key.NumPad9:
				return SWI.Key.NumPad9;
			case Key.NumPadMultiply:
				return SWI.Key.Multiply;
			case Key.NumPadAdd:
				return SWI.Key.Add;
			//case Key.NumPadDecimal:
			//  return SWI.Key.Separator;
			case Key.NumPadSubtract:
				return SWI.Key.Subtract;
			case Key.NumPadDecimal:
				return SWI.Key.Decimal;
			case Key.NumPadDivide:
				return SWI.Key.Divide;
			case Key.F1:
				return SWI.Key.F1;
			case Key.F2:
				return SWI.Key.F2;
			case Key.F3:
				return SWI.Key.F3;
			case Key.F4:
				return SWI.Key.F4;
			case Key.F5:
				return SWI.Key.F5;
			case Key.F6:
				return SWI.Key.F6;
			case Key.F7:
				return SWI.Key.F7;
			case Key.F8:
				return SWI.Key.F8;
			case Key.F9:
				return SWI.Key.F9;
			case Key.F10:
				return SWI.Key.F10;
			//case Key.F11:
			//  return SWI.Key.F11;
			//case Key.F12:
			//  return SWI.Key.F12;
			//case Key.F13:
			//  return SWI.Key.F13;
			//case Key.F14:
			//  return SWI.Key.F14;
			//case Key.F15:
			//  return SWI.Key.F15;
			//case Key.F16:
			//  return SWI.Key.F16;
			//case Key.F17:
			//  return SWI.Key.F17;
			//case Key.F18:
			//  return SWI.Key.F18;
			//case Key.F19:
			//  return SWI.Key.F19;
			//case Key.F20:
			//  return SWI.Key.F20;
			//case Key.F21:
			//  return SWI.Key.F21;
			//case Key.F22:
			//  return SWI.Key.F22;
			//case Key.F23:
			//  return SWI.Key.F23;
			//case Key.F24:
			//  return SWI.Key.F24;
			case Key.NumLock:
				return SWI.Key.NumLock;
			case Key.ScrollLock:
				return SWI.Key.Scroll;
			case Key.ShiftLeft:
				return SWI.Key.LeftShift;
			case Key.ShiftRight:
				return SWI.Key.RightShift;
			case Key.ControlLeft:
				return SWI.Key.LeftCtrl;
			case Key.ControlRight:
				return SWI.Key.RightCtrl;
			case Key.AltLeft:
				return SWI.Key.LeftAlt;
			case Key.AltRight:
				return SWI.Key.RightAlt;
			//case Key:
			//  return SWI.Key.BrowserBack;
			//case Key:
			//  return SWI.Key.BrowserForward;
			//case Key:
			//  return SWI.Key.BrowserRefresh;
			//case Key:
			//  return SWI.Key.BrowserStop;
			//case Key:
			//  return SWI.Key.BrowserSearch;
			//case Key:
			//  return SWI.Key.BrowserFavorites;
			//case Key:
			//  return SWI.Key.BrowserHome;
			//case Key:
			//  return SWI.Key.VolumeMute;
			//case Key:
			//  return SWI.Key.VolumeDown;
			//case Key:
			//  return SWI.Key.VolumeUp;
			//case Key:
			//  return SWI.Key.MediaNextTrack;
			//case Key:
			//  return SWI.Key.MediaPreviousTrack;
			//case Key:
			//  return SWI.Key.MediaStop;
			//case Key:
			//  return SWI.Key.MediaPlayPause;
			//case Key:
			//  return SWI.Key.LaunchMail;
			//case Key:
			//  return SWI.Key.SelectMedia;
			//case Key:
			//  return SWI.Key.LaunchApplication1;
			//case Key:
			//  return SWI.Key.LaunchApplication2;
			//case Key:
			//  return SWI.Key.Oem1;
			case Key.Semicolon:
				return SWI.Key.OemSemicolon;
			case Key.Plus:
				return SWI.Key.OemPlus;
			case Key.Comma:
				return SWI.Key.OemComma;
			case Key.Minus:
				return SWI.Key.OemMinus;
			case Key.Period:
				return SWI.Key.OemPeriod;
			//case Key:
			//  return SWI.Key.Oem2;
			case Key.Question:
				return SWI.Key.OemQuestion;
			//case Key:
			//  return SWI.Key.Oem3;
			//case Key:
			//  return SWI.Key.OemTilde;
			//case Key:
			//  return SWI.Key.AbntC1;
			//case Key:
			//  return SWI.Key.AbntC2;
			//case Key:
			//  return SWI.Key.Oem4;
			//case Key:
			//  return SWI.Key.OemOpenBrackets;
			//case Key:
			//  return SWI.Key.Oem5;
			//case Key:
			//  return SWI.Key.OemPipe;
			//case Key:
			//  return SWI.Key.Oem6;
			//case Key:
			//  return SWI.Key.OemCloseBrackets;
			//case Key:
			//  return SWI.Key.Oem7;
			//case Key:
			//  return SWI.Key.OemQuotes;
			//case Key:
			//  return SWI.Key.Oem8;
			//case Key:
			//  return SWI.Key.Oem102;
			//case Key:
			//  return SWI.Key.OemBackslash;
			//case Key:
			//  return SWI.Key.ImeProcessed;
			//case Key:
			//  return SWI.Key.System;
			//case Key:
			//  return SWI.Key.OemAttn;
			//case Key:
			//  return SWI.Key.DbeAlphanumeric;
			//case Key:
			//  return SWI.Key.OemFinish;
			//case Key:
			//  return SWI.Key.DbeKatakana;
			//case Key:
			//  return SWI.Key.OemCopy;
			//case Key:
			//  return SWI.Key.DbeHiragana;
			//case Key:
			//  return SWI.Key.OemAuto;
			//case Key:
			//  return SWI.Key.DbeSbcsChar;
			//case Key:
			//  return SWI.Key.OemEnlw;
			//case Key:
			//  return SWI.Key.DbeDbcsChar;
			//case Key:
			//  return SWI.Key.OemBackTab;
			//case Key:
			//  return SWI.Key.DbeRoman;
			//case Key:
			//  return SWI.Key.Attn;
			//case Key:
			//  return SWI.Key.DbeNoRoman;
			//case Key:
			//  return SWI.Key.CrSel;
			//case Key:
			//  return SWI.Key.DbeEnterWordRegisterMode;
			//case Key:
			//  return SWI.Key.ExSel;
			//case Key:
			//  return SWI.Key.DbeEnterImeConfigureMode;
			//case Key:
			//  return SWI.Key.EraseEof;
			//case Key:
			//  return SWI.Key.DbeFlushString;
			//case Key:
			//  return SWI.Key.Play;
			//case Key:
			//  return SWI.Key.DbeCodeInput;
			//case Key:
			//  return SWI.Key.Zoom;
			//case Key:
			//  return SWI.Key.DbeNoCodeInput;
			//case Key:
			//  return SWI.Key.NoName;
			//case Key:
			//  return SWI.Key.DbeDetermineString;
			//case Key:
			//  return SWI.Key.Pa1;
			//case Key:
			//  return SWI.Key.DbeEnterDialogConversionMode;
			//case Key.Clear:
			//  return SWI.Key.OemClear;
			//case Key:
			//  return SWI.Key.DeadCharProcessed;
			default:
				return SWI.Key.None;
			}
		}
	}
}
