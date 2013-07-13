using System;
using Xwt;
using Xwt.Drawing;
using System.Xml;

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
			StartPosition = WindowPosition.CenterScreen;

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

				appMenu.SubMenu.Items.Add (StockCommands.About.CreateMenuItem ());
				appMenu.SubMenu.Items.AddSeparator ();

				/* App > Preferences... */

				appMenu.SubMenu.Items.Add (StockCommands.Preferences.CreateMenuItem ());
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
				appMenu.SubMenu.Items.Add (StockCommands.HideApplication.CreateMenuItem ());
				
				/* App > Hide Others */
				appMenu.SubMenu.Items.Add (StockCommands.HideOtherApplications.CreateMenuItem ());
				
				/* App > Show All */
				appMenu.SubMenu.Items.Add (StockCommands.UnhideAllApplications.CreateMenuItem ());
				appMenu.SubMenu.Items.AddSeparator ();
				
				/* App > Quit App */
				appMenu.SubMenu.Items.Add (StockCommands.Quit.CreateMenuItem ());
			}

			/* File Menu */

			var fileMenu = new MenuItem ("File");
			fileMenu.SubMenu = new Menu ();
			fileMenu.SubMenu.Items.Add (StockCommands.New.CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommands.Open.CreateMenuItem ());
			var openRecent = new MenuItem ("Open Recent");
			openRecent.SubMenu = new Menu ();
			fileMenu.SubMenu.Items.Add (openRecent);
			fileMenu.SubMenu.Items.AddSeparator ();
			fileMenu.SubMenu.Items.Add (StockCommands.Close.CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommands.CloseAll.CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommands.Save.CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommands.Duplicate.CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommands.Export.CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommands.SaveAll.CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommands.Revert.CreateMenuItem ());
			fileMenu.SubMenu.Items.AddSeparator ();
			fileMenu.SubMenu.Items.Add (StockCommands.Print.CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommands.PageSetup.CreateMenuItem ());
			if (!isMac) {
				fileMenu.SubMenu.Items.AddSeparator ();
				fileMenu.SubMenu.Items.Add (StockCommands.Quit.CreateMenuItem ());
			}
			mainMenu.Items.Add (fileMenu);
			
			var editMenu = new MenuItem ("Edit");
			editMenu.SubMenu = new Menu ();
			editMenu.SubMenu.Items.Add (StockCommands.Undo.CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommands.Redo.CreateMenuItem ());
			editMenu.SubMenu.Items.AddSeparator ();
			editMenu.SubMenu.Items.Add (StockCommands.Cut.CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommands.Copy.CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommands.Paste.CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommands.PasteAsText.CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommands.Delete.CreateMenuItem ());
			editMenu.SubMenu.Items.AddSeparator ();
			editMenu.SubMenu.Items.Add (StockCommands.SelectAll.CreateMenuItem ());
			editMenu.SubMenu.Items.AddSeparator ();
			editMenu.SubMenu.Items.Add (StockCommands.Find.CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommands.FindNext.CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommands.FindPrevious.CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommands.Replace.CreateMenuItem ());
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
				windowMenu.SubMenu.Items.Add (StockCommands.Minimize.CreateMenuItem ());
				windowMenu.SubMenu.Items.Add (StockCommands.Maximize.CreateMenuItem ());
			}
			mainMenu.Items.Add (windowMenu);

			/* Help Menu */

			var helpMenu = new MenuItem ("Help");
			helpMenu.SubMenu = new Menu ();
			helpMenu.SubMenu.Items.Add (StockCommands.Help.CreateMenuItem ());
			if (!isMac) {
				helpMenu.SubMenu.Items.Add(StockCommands.About.CreateMenuItem ());
			}
			mainMenu.Items.Add (helpMenu);

			MainMenu = mainMenu;
			
			
			HPaned box = new HPaned ();
			
			icon = Image.FromResource (typeof(App), "document-generic.png");
			
			store = new TreeStore (nameCol, iconCol, widgetCol);
			samplesTree = new TreeViewEx ();
			samplesTree.Columns.Add ("Name", iconCol, nameCol);
			
			AddSample (null, "Boxes", typeof(Boxes));
			AddSample (null, "Buttons", typeof(ButtonSample));
			AddSample (null, "CheckBox", typeof(Checkboxes));
			AddSample (null, "Clipboard", typeof(ClipboardSample));
			AddSample (null, "ColorSelector", typeof(ColorSelectorSample));
			AddSample (null, "ComboBox", typeof(ComboBoxes));
			AddSample (null, "DatePicker", typeof(DatePickerSample));
//			AddSample (null, "Designer", typeof(Designer));
			AddSample (null, "Drag & Drop", typeof(DragDrop));
			
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

			AddSample (null, "Expander", typeof (ExpanderSample));
			AddSample (null, "Progress bars", typeof(ProgressBarSample));
			AddSample (null, "Frames", typeof(Frames));
			AddSample (null, "Images", typeof(Images));
			AddSample (null, "Labels", typeof(Labels));
			AddSample (null, "ListBox", typeof(ListBoxSample));
			AddSample (null, "LinkLabels", typeof(LinkLabels));
			AddSample (null, "ListView", typeof(ListView1));
			AddSample (null, "Markdown", typeof (MarkDownSample));
			AddSample (null, "Menu", typeof(MenuSamples));
			AddSample (null, "Notebook", typeof(NotebookSample));
			AddSample (null, "Paneds", typeof(PanedViews));
			AddSample (null, "Popover", typeof(PopoverSample));
			AddSample (null, "RadioButton", typeof (RadioButtonSample));
			AddSample (null, "Screens", typeof (ScreensSample));
			AddSample (null, "Scroll View", typeof(ScrollWindowSample));
			AddSample (null, "Scrollbar", typeof(ScrollbarSample));
			AddSample (null, "Slider", typeof (SliderSample));
			AddSample (null, "Spinners", typeof (Spinners));
			AddSample (null, "Tables", typeof (Tables));
			AddSample (null, "Text Entry", typeof (TextEntries));
			AddSample (null, "Tooltips", typeof(Tooltips));
			AddSample (null, "TreeView", typeof(TreeViews));
			AddSample (null, "WidgetEvents", typeof(WidgetEvents));
			AddSample (null, "Windows", typeof(Windows));
			
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

		//[CommandHandler ("Open")]
		public void HandleOpenCommand ()
		{
			MessageDialog.ShowMessage ("Open Sesame!");
		}

		class TreeViewEx : TreeView
		{
			[CommandHandler ("Open")]
			public void HandleOpenCommand ()
			{
				MessageDialog.ShowMessage ("Open Sesame!");
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

