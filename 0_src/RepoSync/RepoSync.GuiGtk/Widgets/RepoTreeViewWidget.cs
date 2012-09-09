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

			this.Add (tv);
		}

		private void InitColumns ()
		{
			// TODO: action column must be minimal space right aligned.
			// TODO: action column must be clickable
			// TODO: action column must fire event
			// TODO: action column should be an image or a button

			var nameColumn = new TreeViewColumn { Title = "Name" };
			var srcColumn = new TreeViewColumn { Title = "Source" };
			var destColumn = new TreeViewColumn { Title = "Destination" };
			var actionColumn = new TreeViewColumn { Title = "Action" };

			tv.AppendColumn (nameColumn);
			tv.AppendColumn (srcColumn);
			tv.AppendColumn (destColumn);
			tv.AppendColumn (actionColumn);

			// CellRenderers...
			var nameCell = new CellRendererText ();
			nameColumn.PackStart (nameCell, true);

			var srcCell = new CellRendererText ();
			srcColumn.PackStart (srcCell, true);

			var destCell = new CellRendererText ();
			destColumn.PackStart (destCell, true);

			var actionCell = new CellRendererText ();
			actionColumn.PackStart (actionCell, true);

			// Tell the Cell Renderers which items in the model to display (mapping to listStore)
			nameColumn.AddAttribute (nameCell, "text", 0);
			srcColumn.AddAttribute (srcCell, "text", 1);
			destColumn.AddAttribute (destCell, "text", 2);
			actionColumn.AddAttribute (actionCell, "text", 3);
		}



		public void Update (SyncConfig syncConfig)
		{
			if (syncConfig == null) {
				return;
			}

			var p = "pull";
			foreach (var entry in syncConfig.Entries) {
				listStore.AppendValues (entry.Name, entry.Source, entry.Destination, p);
			}
		}

	}
}

