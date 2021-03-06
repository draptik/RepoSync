using System;
using Gtk;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SyncActionWidget : Gtk.Bin
	{
		private Button btnDoDefaultGitActionForAll;
		private CheckButton btnToggleSelection;
		private bool ignoreSetActive = false;

		public event System.Action DefaultGitActionForAllStarted;
		public event System.Action ToggleAllReposStarted;

		public SyncActionWidget ()
		{
			this.Build ();

			Init ();

			IsActive = false;

			this.ShowAll ();
		}
		
		public bool IsActive { set { btnDoDefaultGitActionForAll.Sensitive = value; } }

		public bool IsSelectAllChecked 
		{
			get { return btnToggleSelection.Active; }
			set 
			{
				ignoreSetActive = true; // workaround, because setting Active fires event
				btnToggleSelection.Active = value; // This fires the click/toggle event! Not good!
			} 
		}

		private void Init ()
		{
			var hbox = new Gtk.HBox();

			// Pull button
			btnDoDefaultGitActionForAll = new Gtk.Button();
			btnDoDefaultGitActionForAll.Label = "Execute default git action for all repos";
			btnDoDefaultGitActionForAll.Clicked += OnDefaultGitActionForAllClickedStarted;

			// Toggle button
			btnToggleSelection = new Gtk.CheckButton();
			btnToggleSelection.Label = "Select all";
			btnToggleSelection.Toggled += OnToggleClickStarted;

			hbox.PackStart(btnToggleSelection, false, false, 20);
			hbox.PackStart(btnDoDefaultGitActionForAll, true, true, 0);

			this.Add (hbox);
		}

		private void OnDefaultGitActionForAllClickedStarted (object sender, EventArgs e)
		{
			var handler = this.DefaultGitActionForAllStarted;
			if (handler != null) {
				handler();
			}
		}		

		private void OnToggleClickStarted (object sender, EventArgs e)
		{
			if (!ignoreSetActive) {
				var handler = this.ToggleAllReposStarted;
				if (handler != null) {
					handler ();
				}
			}
			else {
				ignoreSetActive = false;
			}
		}
	}
}

