using System;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SyncActionWidget : Gtk.Bin
	{

		public event Action PullStarted;

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
			btnPull.Clicked += PullClickedStarted;

			hbox.Add (btnPull);

			// TODO: Add more buttons here...

			this.Add (hbox);
		}

		private void PullClickedStarted (object sender, EventArgs e)
		{
			var handler = this.PullStarted;
			if (handler != null) {
				handler();
			}
		}

	}
}

