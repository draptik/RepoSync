using System;
using RepoSync.Service.Config;
using Gtk;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class RepoTreeViewWidget : Gtk.Bin
	{
		private const int COLINDEX_TOGGLE = 0;
		private const int COLINDEX_NAME = 1;
		private const int COLINDEX_LOCAL = 2;
		private const int COLINDEX_REMOTE = 3;
		private const int COLINDEX_ACTION = 4;
		private TreeView tv;
		private ListStore model;

		public event System.Action<bool> SingleSelectionChangeStarted;

		private void OnSingleSelectionChangeStarted (bool areAllEntriesSelected)
		{
			var handler = this.SingleSelectionChangeStarted;
			if (handler != null) {
				handler(areAllEntriesSelected);
			}
		}

		public RepoTreeViewWidget ()
		{
			this.Build ();

			Init ();

			this.ShowAll ();
		}

		public void Update (SyncConfig syncConfig)
		{
			model.Clear ();

			if (syncConfig == null) {
				return;
			}

			foreach (var entry in syncConfig.Entries) {
				var defaultActivationState = true;
				model.AppendValues (defaultActivationState, 
				                    entry.Name, 
				                    entry.Local, 
				                    entry.Remote, 
				                    entry.DefaultGitAction.ToString ());
			}
		}

		public void ToggleAll (bool isSelectAllChecked)
		{
			TreeIter iter;
			if (model.GetIterFirst (out iter)) {
				do {
					model.SetValue (iter, COLINDEX_TOGGLE, isSelectAllChecked);
				} while (model.IterNext (ref iter));
			}
		}

		private void Init ()
		{
			tv = new Gtk.TreeView ();
			tv.HeightRequest = 100;

			InitColumns ();

			model = new ListStore (typeof(bool), typeof(string), typeof(string), typeof(string), typeof(string));

			tv.Model = model;


			var vbox = new VBox ();
			vbox.PackStart (tv, true, true, 0);

			this.Add (vbox);
		}

		private void InitColumns ()
		{
			var toggle = new CellRendererToggle ();
			toggle.Toggled += HandleRepoToggled;

			tv.AppendColumn ("", toggle, "active", COLINDEX_TOGGLE);
			tv.AppendColumn ("Name", new CellRendererText (), "text", COLINDEX_NAME);
			tv.AppendColumn ("Local", new CellRendererText (), "text", COLINDEX_LOCAL);
			tv.AppendColumn ("Remote", new CellRendererText (), "text", COLINDEX_REMOTE);
			tv.AppendColumn ("Action", new CellRendererText (), "text", COLINDEX_ACTION);
		}

		private void HandleRepoToggled (object o, ToggledArgs args)
		{
			var currentCellRendererToggle = o as CellRendererToggle;

			if (currentCellRendererToggle == null) {
				return;
			}

			var viewPathIndex = new TreePath (args.Path);

			// check if model has the view index
			TreeIter iterModelIndex;
			if (!model.GetIter (out iterModelIndex, viewPathIndex)) {
				return; 
			}

			// update toggle state
			model.SetValue (iterModelIndex, COLINDEX_TOGGLE, !currentCellRendererToggle.Active);

			OnSingleSelectionChangeStarted (AreAllEntriesSelected);
		}

		private bool AreAllEntriesSelected 
		{
			get 
			{ 
				bool areAllSelected = true;
				TreeIter iter;
				if (model.GetIterFirst (out iter)) {
					do {
						areAllSelected = (bool) model.GetValue (iter, COLINDEX_TOGGLE);
					} while (areAllSelected && model.IterNext (ref iter));
				}
				return areAllSelected;
			}
		}
	}
}

