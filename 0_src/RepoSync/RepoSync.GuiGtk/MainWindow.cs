using System;
using Gtk;

namespace RepoSync.GuiGtk
{
	public partial class MainWindow: Gtk.Window
	{	
		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build (); // calls code behind page...
		}
	
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
	}

}

