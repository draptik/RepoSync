using System;
using RepoSync.Service.Config;
using Gtk;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class RepoTreeViewWidget : Gtk.Bin
	{
		private TreeView tv;
		private ListStore listStore;


		public RepoTreeViewWidget ()
		{
			this.Build ();

			Init ();

			this.ShowAll ();
		}

		private void Init ()
		{
			tv = new Gtk.TreeView();

			InitColumns ();

			listStore = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));

			tv.Model = listStore;


			var vbox = new VBox();
			vbox.PackStart(tv, true, true, 0);

			this.Add (vbox);
		}

		private void InitColumns ()
		{
			// TODO: action column must be minimal space right aligned.
			// TODO: action column must be clickable
			// TODO: action column must fire event
			// TODO: action column should be an image or a button

			tv.AppendColumn ("Name", new CellRendererText(), "text", 0);
			tv.AppendColumn ("Local", new CellRendererText(), "text", 1);
			tv.AppendColumn ("Remote", new CellRendererText(), "text", 2);
			tv.AppendColumn ("Action", new CellRendererText(), "text", 3);
		}



		public void Update (SyncConfig syncConfig)
		{
			listStore.Clear ();

			if (syncConfig == null) {
				return;
			}

			foreach (var entry in syncConfig.Entries) {
				listStore.AppendValues (entry.Name, entry.Local, entry.Remote, entry.DefaultGitAction.ToString());
			}
		}

	}
}

