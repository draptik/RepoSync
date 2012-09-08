using System;

namespace RepoSync.GuiGtk.Widgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SyncActionWidget : Gtk.Bin
	{
		public SyncActionWidget ()
		{
			this.Build ();

			Init ();

			this.ShowAll ();
		}

		private void Init ()
		{
			var hbox = new Gtk.HBox();

			// Pull button
			var btnPull = new Gtk.Button();
			btnPull.Label = "Pull";
			hbox.Add (btnPull);

			// TODO: Add more buttons here...

			this.Add (hbox);
		}

	}
}

