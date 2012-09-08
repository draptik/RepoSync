using System;
using Gtk;
using RepoSync.GuiGtk.Widgets;

namespace RepoSync.GuiGtk
{
	public partial class MainWindow: Gtk.Window
	{	
		private string guiTitle = "RepoSync";

		private VBox vBoxMain;
		private VBox vBoxContent;

		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build (); // calls code behind page...

			InitMainWindow();

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

			vBoxContent.Add (new ChooseConfigWidget());
			vBoxContent.Add (new RepoTreeViewWidget());
			vBoxContent.Add (new SyncActionWidget());
			vBoxContent.Add (new SyncOutputWidget());
			// TODO: Add more content here
		}





		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
	}

}

