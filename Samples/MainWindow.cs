using System;
using System.Xml;
using Xwt;
using Xwt.Commands;
using Xwt.Drawing;

namespace Samples
{
	public class MainWindow: Window
	{
		TreeViewEx samplesTree;
		TreeStore store;
		Image icon;
		VBox sampleBox;
		Label title;
		Widget currentSample;
		
		DataField<string> nameCol = new DataField<string> ();
		DataField<Sample> widgetCol = new DataField<Sample> ();
		DataField<Image> iconCol = new DataField<Image> ();
		
		StatusIcon statusIcon;
		
		public MainWindow ()
		{
			/* Basic Window Appearance Properties */

			Title = "Xwt Demo Application";
			Width = 500;
			Height = 400;
			InitialLocation = WindowLocation.CenterScreen;

			/* Application Status Icon */

			try {
				statusIcon = Application.CreateStatusIcon ();
				statusIcon.Menu = new Menu ();
				statusIcon.Menu.Items.Add (new MenuItem ("Test"));
				statusIcon.Image = Image.FromResource (GetType (), "package.png");
			} catch {
				Console.WriteLine ("Status icon could not be shown");
			}

			/* Main Menu */

			Menu mainMenu = new Menu ();

			var isMac = Toolkit.CurrentEngine.Type == ToolkitType.Cocoa;

			/* App Menu */

			// only Mac has an application menu
			if (isMac) {
				var appMenu = new MenuItem (); // menu title is automatically assigned
				appMenu.SubMenu = new Menu ();
				mainMenu.Items.Add (appMenu);

				/* App > About App */

				appMenu.SubMenu.Items.Add (Command.App.About);
				appMenu.SubMenu.Items.AddSeparator ();

				/* App > Preferences... */

				appMenu.SubMenu.Items.Add (Command.App.Preferences);
				appMenu.SubMenu.Items.AddSeparator ();

				/* App > Services */

				var appServices = new MenuItem("Services");
				appServices.SubMenu = new Menu ();
				// just create an empty menu and by assigning it to the Window.ServicesMenu property,
				// the OS will take care of populating it
				ServicesMenu = appServices.SubMenu;
				appMenu.SubMenu.Items.Add (appServices);
				appMenu.SubMenu.Items.AddSeparator ();

				/* App > Hide App */
				appMenu.SubMenu.Items.Add (Command.App.Hide);
				
				/* App > Hide Others */
				appMenu.SubMenu.Items.Add (Command.App.HideOthers);
				
				/* App > Show All */
				appMenu.SubMenu.Items.Add (Command.App.UnhideAll);
				appMenu.SubMenu.Items.AddSeparator ();
				
				/* App > Quit App */
				appMenu.SubMenu.Items.Add (Command.App.Quit);
			}

			/* File Menu */

			var fileMenu = new MenuItem ("File");
			fileMenu.SubMenu = new Menu ();
			fileMenu.SubMenu.Items.Add (Command.File.New);
			fileMenu.SubMenu.Items.Add (Command.File.Open);
			var openRecent = new MenuItem ("Open Recent");
			openRecent.SubMenu = new Menu ();
			fileMenu.SubMenu.Items.Add (openRecent);
			fileMenu.SubMenu.Items.AddSeparator ();
			fileMenu.SubMenu.Items.Add (Command.File.Close);
			fileMenu.SubMenu.Items.Add (Command.File.CloseAll);
			fileMenu.SubMenu.Items.Add (Command.File.Save);
			fileMenu.SubMenu.Items.Add (Command.File.Duplicate);
			fileMenu.SubMenu.Items.Add (Command.File.Export);
			fileMenu.SubMenu.Items.Add (Command.File.SaveAll);
			fileMenu.SubMenu.Items.Add (Command.File.Revert);
			fileMenu.SubMenu.Items.AddSeparator ();
			fileMenu.SubMenu.Items.Add (Command.File.Print);
			fileMenu.SubMenu.Items.Add (Command.File.PageSetup);
			if (!isMac) {
				fileMenu.SubMenu.Items.AddSeparator ();
				fileMenu.SubMenu.Items.Add (Command.App.Quit);
			}
			mainMenu.Items.Add (fileMenu);
			
			var editMenu = new MenuItem ("Edit");
			editMenu.SubMenu = new Menu ();
			editMenu.SubMenu.Items.Add (Command.Edit.Undo);
			editMenu.SubMenu.Items.Add (Command.Edit.Redo);
			editMenu.SubMenu.Items.AddSeparator ();
			editMenu.SubMenu.Items.Add (Command.Edit.Cut);
			editMenu.SubMenu.Items.Add (Command.Edit.Copy);
			editMenu.SubMenu.Items.Add (Command.Edit.Paste);
			editMenu.SubMenu.Items.Add (Command.Edit.PasteAsText);
			editMenu.SubMenu.Items.Add (Command.Edit.Delete);
			editMenu.SubMenu.Items.AddSeparator ();
			editMenu.SubMenu.Items.Add (Command.Edit.SelectAll);
			editMenu.SubMenu.Items.AddSeparator ();
			editMenu.SubMenu.Items.Add (Command.Edit.Find);
			editMenu.SubMenu.Items.Add (Command.Edit.FindNext);
			editMenu.SubMenu.Items.Add (Command.Edit.FindPrevious);
			editMenu.SubMenu.Items.Add (Command.Edit.Replace);
			mainMenu.Items.Add (editMenu);

			/* View Menu */

			var viewMenu = new MenuItem ("View");
			viewMenu.SubMenu = new Menu ();
			// TODO: implement Toolbars
			viewMenu.SubMenu.Items.Add (new MenuItem ("Toolbar"));
			// TODO: implement full screen
			mainMenu.Items.Add(viewMenu);

			/* Window Menu */

			var windowMenu = new MenuItem ("Window");
			windowMenu.SubMenu = new Menu ();
			WindowMenu = windowMenu.SubMenu;
			if (isMac) {
				windowMenu.SubMenu.Items.Add (Command.Window.Minimize);
				windowMenu.SubMenu.Items.Add (Command.Window.Maximize);
			}
			mainMenu.Items.Add (windowMenu);

			/* Help Menu */

			var helpMenu = new MenuItem ("Help");
			helpMenu.SubMenu = new Menu ();
			helpMenu.SubMenu.Items.Add (Command.Misc.Help);
			if (!isMac) {
				helpMenu.SubMenu.Items.Add(Command.App.About);
			}
			mainMenu.Items.Add (helpMenu);

			MainMenu = mainMenu;
			
			
			HPaned box = new HPaned ();
			
			icon = Image.FromResource (typeof(App), "document-generic.png");
			
			store = new TreeStore (nameCol, iconCol, widgetCol);
			samplesTree = new TreeViewEx ();
			samplesTree.Columns.Add ("Name", iconCol, nameCol);

			var w = AddSample (null, "Widgets", null);
			AddSample (w, "Boxes", typeof(Boxes));
			AddSample (w, "Buttons", typeof(ButtonSample));
			AddSample (w, "CheckBox", typeof(Checkboxes));
			AddSample (w, "Clipboard", typeof(ClipboardSample));
			AddSample (w, "ColorSelector", typeof(ColorSelectorSample));
			AddSample (w, "ComboBox", typeof(ComboBoxes));
			AddSample (w, "DatePicker", typeof(DatePickerSample));
//			AddSample (null, "Designer", typeof(Designer));
			AddSample (w, "Expander", typeof (ExpanderSample));
			AddSample (w, "Progress bars", typeof(ProgressBarSample));
			AddSample (w, "Frames", typeof(Frames));
			AddSample (w, "Images", typeof(Images));
			AddSample (w, "Labels", typeof(Labels));
			AddSample (w, "ListBox", typeof(ListBoxSample));
			AddSample (w, "LinkLabels", typeof(LinkLabels));
			var listView = AddSample (w, "ListView", typeof(ListView1));
			AddSample (listView, "Editable checkboxes", typeof(ListView2));
			AddSample (w, "Markdown", typeof (MarkDownSample));
			AddSample (w, "Menu", typeof(MenuSamples));
			AddSample (w, "Notebook", typeof(NotebookSample));
			AddSample (w, "Paneds", typeof(PanedViews));
			AddSample (w, "Popover", typeof(PopoverSample));
			AddSample (w, "RadioButton", typeof (RadioButtonSample));
			AddSample (w, "Scroll View", typeof(ScrollWindowSample));
			AddSample (w, "Scrollbar", typeof(ScrollbarSample));
			AddSample (w, "Slider", typeof (SliderSample));
			AddSample (w, "Spinners", typeof (Spinners));
			AddSample (w, "Tables", typeof (Tables));
			AddSample (w, "Text Entry", typeof (TextEntries));
			AddSample (w, "TreeView", typeof(TreeViews));

			var n = AddSample (null, "Drawing", null);
			AddSample (n, "Canvas with Widget (Linear)", typeof (CanvasWithWidget_Linear));
			AddSample (n, "Canvas with Widget (Radial)", typeof (CanvasWithWidget_Radial));
			AddSample (n, "Chart", typeof (ChartSample));
			AddSample (n, "Colors", typeof(ColorsSample));
			AddSample (n, "Figures", typeof(DrawingFigures));
			AddSample (n, "Transformations", typeof(DrawingTransforms));
			AddSample (n, "Images and Patterns", typeof(DrawingPatternsAndImages));
			AddSample (n, "Text", typeof(DrawingText));
			AddSample (n, "Partial Images", typeof (PartialImages));
			AddSample (n, "Custom Drawn Image", typeof (ImageScaling));
			AddSample (n, "Widget Rendering", typeof (WidgetRendering));

			var wf = AddSample (null, "Widget Features", null);
			AddSample (wf, "Drag & Drop", typeof(DragDrop));
			AddSample (wf, "Widget Events", typeof(WidgetEvents));
			AddSample (wf, "Opacity", typeof(OpacitySample));
			AddSample (wf, "Tooltips", typeof(Tooltips));

			AddSample (null, "Windows", typeof(Windows));
			
			AddSample (null, "Screens", typeof (ScreensSample));

			samplesTree.DataSource = store;
			
			box.Panel1.Content = samplesTree;
			
			sampleBox = new VBox ();
			title = new Label ("Sample:");
			sampleBox.PackStart (title);
			
			box.Panel2.Content = sampleBox;
			box.Panel2.Resize = true;
			box.Position = 160;
			
			Content = box;
			
			samplesTree.SelectionChanged += HandleSamplesTreeSelectionChanged;

			CloseRequested += HandleCloseRequested;
		}

		[CommandHandler (StockCommands.File.Close)]
		public void OnCloseCommand ()
		{
			MessageDialog.ShowMessage ("Main Window!");
		}

		class TreeViewEx : TreeView
		{
			[CommandHandler (StockCommands.File.Close)]
			public void OnCloseCommand ()
			{
				MessageDialog.ShowMessage ("Tree View!");
			}

			[CommandStatusRequestHandler (StockCommands.File.Close)]
			public bool OnCloseCommandStatusRequested ()
			{
				return SelectedRow != null;
			}
		}

		void HandleCloseRequested (object sender, CloseRequestedEventArgs args)
		{
			args.Handled = !MessageDialog.Confirm ("Samples will be closed");
		}
		
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			
			if (statusIcon != null) {
				statusIcon.Dispose ();
			}
		}

		void HandleSamplesTreeSelectionChanged (object sender, EventArgs e)
		{
			if (samplesTree.SelectedRow != null) {
				if (currentSample != null)
					sampleBox.Remove (currentSample);
				Sample s = store.GetNavigatorAt (samplesTree.SelectedRow).GetValue (widgetCol);
				if (s.Type != null) {
					if (s.Widget == null)
						s.Widget = (Widget)Activator.CreateInstance (s.Type);
					sampleBox.PackStart (s.Widget, true);
				}

			//	Console.WriteLine (System.Xaml.XamlServices.Save (s.Widget));
				currentSample = s.Widget;
				Dump (currentSample, 0);
			}
		}
		
		void Dump (IWidgetSurface w, int ind)
		{
			if (w == null)
				return;
			var s = w.GetPreferredSize ();
			Console.WriteLine (new string (' ', ind * 2) + " " + w.GetType ().Name + " " + s.Width + " " + s.Height);
			foreach (var c in w.Children)
				Dump (c, ind + 1);
		}
		
		TreePosition AddSample (TreePosition pos, string name, Type sampleType)
		{
			//if (page != null)
			//	page.Margin.SetAll (5);
			return store.AddNode (pos).SetValue (nameCol, name).SetValue (iconCol, icon).SetValue (widgetCol, new Sample (sampleType)).CurrentPosition;
		}
	}
	
	class Sample
	{
		public Sample (Type type)
		{
			Type = type;
		}

		public Type Type;
		public Widget Widget;
	}
}

