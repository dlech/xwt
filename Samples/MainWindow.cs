using System;
using Xwt;
using Xwt.Drawing;
using System.Xml;

namespace Samples
{
	public class MainWindow: Window
	{
		TreeView samplesTree;
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

				appMenu.SubMenu.Items.Add (StockCommand.About.GetGlobalInstance ().CreateMenuItem ());
				appMenu.SubMenu.Items.Add (new SeparatorMenuItem ());

				/* App > Preferences... */

				appMenu.SubMenu.Items.Add (StockCommand.Preferences.GetGlobalInstance ().CreateMenuItem ());
				appMenu.SubMenu.Items.Add (new SeparatorMenuItem ());

				/* App > Services */

				var appServices = new MenuItem("Services");
				appServices.SubMenu = new Menu ();
				// just create an empty menu and by assigning it to the Window.ServicesMenu property,
				// the OS will take care of populating it
				ServicesMenu = appServices.SubMenu;
				appMenu.SubMenu.Items.Add (appServices);
				appMenu.SubMenu.Items.Add (new SeparatorMenuItem ());

				/* App > Hide App */
				appMenu.SubMenu.Items.Add (StockCommand.HideApplication.GetGlobalInstance ().CreateMenuItem ());
				
				/* App > Hide Others */
				appMenu.SubMenu.Items.Add (StockCommand.HideOtherApplications.GetGlobalInstance ().CreateMenuItem ());
				
				/* App > Show All */
				appMenu.SubMenu.Items.Add (StockCommand.UnhideAllApplications.GetGlobalInstance ().CreateMenuItem ());
				appMenu.SubMenu.Items.Add (new SeparatorMenuItem ());
				
				/* App > Quit App */
				appMenu.SubMenu.Items.Add (StockCommand.Quit.GetGlobalInstance ().CreateMenuItem ());
			}

			/* File Menu */

			var fileMenu = new MenuItem ("File");
			fileMenu.SubMenu = new Menu ();
			fileMenu.SubMenu.Items.Add (StockCommand.New.GetGlobalInstance ().CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommand.Open.GetGlobalInstance ().CreateMenuItem ());
			var openRecent = new MenuItem ("Open Recent");
			openRecent.SubMenu = new Menu ();
			fileMenu.SubMenu.Items.Add (openRecent);
			fileMenu.SubMenu.Items.Add (new SeparatorMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommand.Close.GetGlobalInstance ().CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommand.CloseAll.GetGlobalInstance ().CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommand.Save.GetGlobalInstance ().CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommand.Duplicate.GetGlobalInstance ().CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommand.Export.GetGlobalInstance ().CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommand.SaveAll.GetGlobalInstance ().CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommand.Revert.GetGlobalInstance ().CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (new SeparatorMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommand.Print.GetGlobalInstance ().CreateMenuItem ());
			fileMenu.SubMenu.Items.Add (StockCommand.PageSetup.GetGlobalInstance ().CreateMenuItem ());
			if (!isMac) {
				fileMenu.SubMenu.Items.Add (new SeparatorMenuItem ());
				fileMenu.SubMenu.Items.Add (StockCommand.Quit.GetGlobalInstance ().CreateMenuItem ());
			}
			mainMenu.Items.Add (fileMenu);
			
			var editMenu = new MenuItem ("Edit");
			editMenu.SubMenu = new Menu ();
			editMenu.SubMenu.Items.Add (StockCommand.Undo.GetGlobalInstance ().CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommand.Redo.GetGlobalInstance ().CreateMenuItem ());
			editMenu.SubMenu.Items.Add (new SeparatorMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommand.Cut.GetGlobalInstance ().CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommand.Copy.GetGlobalInstance ().CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommand.Paste.GetGlobalInstance ().CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommand.PasteAsText.GetGlobalInstance ().CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommand.Delete.GetGlobalInstance ().CreateMenuItem ());
			editMenu.SubMenu.Items.Add (new SeparatorMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommand.SelectAll.GetGlobalInstance ().CreateMenuItem ());
			editMenu.SubMenu.Items.Add (new SeparatorMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommand.Find.GetGlobalInstance ().CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommand.FindNext.GetGlobalInstance ().CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommand.FindPrevious.GetGlobalInstance ().CreateMenuItem ());
			editMenu.SubMenu.Items.Add (StockCommand.Replace.GetGlobalInstance ().CreateMenuItem ());
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
				windowMenu.SubMenu.Items.Add (StockCommand.Minimize.GetGlobalInstance ().CreateMenuItem ());
				windowMenu.SubMenu.Items.Add (StockCommand.Maximize.GetGlobalInstance ().CreateMenuItem ());
			}
			mainMenu.Items.Add (windowMenu);

			/* Help Menu */

			var helpMenu = new MenuItem ("Help");
			helpMenu.SubMenu = new Menu ();
			helpMenu.SubMenu.Items.Add (StockCommand.Help.GetGlobalInstance ().CreateMenuItem ());
			if (!isMac) {
				helpMenu.SubMenu.Items.Add(StockCommand.About.GetGlobalInstance ().CreateMenuItem ());
			}
			mainMenu.Items.Add (helpMenu);

			MainMenu = mainMenu;
			
			
			HPaned box = new HPaned ();
			
			icon = Image.FromResource (typeof(App), "document-generic.png");
			
			store = new TreeStore (nameCol, iconCol, widgetCol);
			samplesTree = new TreeView ();
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

