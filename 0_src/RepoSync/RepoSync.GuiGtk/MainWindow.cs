using System;
using Gtk;
//using RepoSync.GuiGtk.Widgets;

namespace RepoSync.GuiGtk
{
	public partial class MainWindow: Gtk.Window
	{	
		private string guiTitle = "RepoSync";

		private ChooseConfigWidget chooseConfigWidget;
		private RepoTreeViewWidget repoTreeViewWidget;
		private SyncActionWidget syncActionWidget;
		private SyncOutputWidget syncOutputWidget;


		private readonly MainController mainController;

		private VBox vBoxMain;
		private VBox vBoxContent;

		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build (); // calls code behind page...

			InitMainWindow();

			mainController= new MainController(
				chooseConfigWidget, 
				repoTreeViewWidget, 
				syncActionWidget, 
				syncOutputWidget);

			ShowAll();
		}
	
		private void InitMainWindow ()
		{
			this.Title = guiTitle;

			InitMainVbox();

			this.Add (vBoxMain);
		}

		private void InitMainVbox ()
		{
			vBoxMain = new VBox();

			// TODO: Add menu and status bar here...

			InitContentVbox();

			vBoxMain.Add (vBoxContent);
		}


		private void InitContentVbox ()
		{
			vBoxContent = new VBox();

			chooseConfigWidget = new ChooseConfigWidget();
			repoTreeViewWidget = new RepoTreeViewWidget();
			syncActionWidget = new SyncActionWidget();
			syncOutputWidget= new SyncOutputWidget();

			vBoxContent.PackStart (chooseConfigWidget, false, false, 0);
			vBoxContent.PackStart (repoTreeViewWidget, true, true, 0);
			vBoxContent.PackStart (syncActionWidget, false, false, 0);
			vBoxContent.PackStart (syncOutputWidget, true, true, 0);
			// TODO: Add more content here
		}





		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
	}

}

