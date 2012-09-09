using System;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SyncOutputWidget : Gtk.Bin
	{
		public SyncOutputWidget ()
		{
			this.Build ();
			
			Init ();

			this.ShowAll ();
		}

		private void Init ()
		{
		
			var textView = new Gtk.TextView();

			this.Add (textView);
		}

	}
}

