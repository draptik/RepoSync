using System;
using Gtk;
using RepoSync.Service;
using RepoSync.Service.Config;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ChooseConfigWidget : Gtk.Bin
	{
		private SyncConfig syncConfig;
		private Gtk.FileChooserButton btnFileChooser;

		public event Action<SyncConfig> OnSyncConfigChangedStarted;

		public ChooseConfigWidget ()
		{
			this.Build ();

			Init ();

			this.ShowAll ();
		}

		public SyncConfig SyncConfig { get { return this.syncConfig; } }

		private void Init ()
		{
			btnFileChooser = new Gtk.FileChooserButton (
				"Please choose a file!", Gtk.FileChooserAction.Open);

			btnFileChooser.SelectionChanged += HandleSelectionChanged;

			var vbox = new VBox();
			vbox.PackStart(btnFileChooser, true, true, 0);
			this.Add (vbox);
		}

		private void HandleSelectionChanged (object sender, EventArgs e)
		{
			var configFilename = btnFileChooser.Filename;

			var jsonService = new JsonService (new IoService ());
			jsonService.Init (configFilename);

			syncConfig = jsonService.SyncConfig;

			InvokeSyncConfigChanged (syncConfig);
		}

		private void InvokeSyncConfigChanged (SyncConfig config)
		{
			var handler = this.OnSyncConfigChangedStarted;
			if (handler != null) {
				handler (config);
			}
		}
	}
}

