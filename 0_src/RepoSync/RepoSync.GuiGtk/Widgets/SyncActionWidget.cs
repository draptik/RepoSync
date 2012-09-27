using System;
using Gtk;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SyncActionWidget : Gtk.Bin
	{
		private Button btnDoDefaultGitActionForAll;

		public event System.Action DefaultGitActionForAllStarted;

		public SyncActionWidget ()
		{
			this.Build ();

			Init ();

			IsActive = false;

			this.ShowAll ();
		}
		
		public bool IsActive 
		{
			get { return btnDoDefaultGitActionForAll.Sensitive; }
			set { btnDoDefaultGitActionForAll.Sensitive = value; }
		}

		private void Init ()
		{
			var hbox = new Gtk.HBox();

			// Pull button
			btnDoDefaultGitActionForAll = new Gtk.Button();
			btnDoDefaultGitActionForAll.Label = "Execute default git action for all repos";
			btnDoDefaultGitActionForAll.Clicked += DefaultGitActionForAllClickedStarted;

			hbox.PackStart(btnDoDefaultGitActionForAll, true, true, 0);

			// TODO: Add more buttons here...


			this.Add (hbox);
		}

		private void DefaultGitActionForAllClickedStarted (object sender, EventArgs e)
		{
			var handler = this.DefaultGitActionForAllStarted;
			if (handler != null) {
				handler();
			}
		}

	}
}

