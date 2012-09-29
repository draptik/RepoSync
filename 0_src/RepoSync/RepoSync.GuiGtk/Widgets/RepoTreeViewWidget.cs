using System;
using RepoSync.Service.Config;
using Gtk;
using Entry = RepoSync.Service.Config.Entry;
using System.Collections.Generic;
using RepoSync.Service;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class RepoTreeViewWidget : Gtk.Bin
	{
		private const int COLINDEX_TOGGLE = 0;
		private const int COLINDEX_ENTRY = 1;
		private TreeView tv;
		private ListStore model;
		private ListStore modelGitActions;

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
				model.AppendValues (defaultActivationState, entry);
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

		public List<Entry> GetCheckedEntries ()
		{
			List<Entry> result = new List<Entry>();
			TreeIter iter;
			if (model.GetIterFirst (out iter)) {
				do {
					bool isSelected = (bool) model.GetValue (iter, COLINDEX_TOGGLE);
					if (isSelected) {
						result.Add ((Entry) model.GetValue (iter, COLINDEX_ENTRY));
					}
				} while (model.IterNext (ref iter));
			}
			return result;
		}

		private void Init ()
		{
			tv = new Gtk.TreeView ();
			tv.HeightRequest = 100;

			modelGitActions = new ListStore (typeof(string));
			modelGitActions.AppendValues (DefaultGitAction.Pull.ToString ());
			modelGitActions.AppendValues (DefaultGitAction.Push.ToString ());

			InitColumns ();

			model = new ListStore (typeof(bool), typeof(Entry));
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

			var nameColumn = new TreeViewColumn { Title = "Name" };
			var nameCell = new CellRendererText ();
			nameColumn.PackStart (nameCell, true);
			nameColumn.SetCellDataFunc (nameCell, new TreeCellDataFunc (RenderEntryName));

			var localColumn = new TreeViewColumn { Title = "Local" };
			var localCell = new CellRendererText ();
			localColumn.PackStart (localCell, true);
			localColumn.SetCellDataFunc (localCell, new TreeCellDataFunc (RenderEntryLocal));

			var remoteColumn = new TreeViewColumn { Title = "Remote" };
			var remoteCell = new CellRendererText ();
			remoteColumn.PackStart (remoteCell, true);
			remoteColumn.SetCellDataFunc (remoteCell, new TreeCellDataFunc (RenderEntryRemote));

			var actionColumn = new TreeViewColumn { Title = "Action" };
			var actionCell = new CellRendererCombo ();
			actionColumn.PackStart (actionCell, true);
			actionColumn.AddAttribute (actionCell, "text", COLINDEX_ENTRY);
			actionColumn.SetCellDataFunc (actionCell, new TreeCellDataFunc (RenderEntryAction));

			tv.AppendColumn (nameColumn);
			tv.AppendColumn (localColumn);
			tv.AppendColumn (remoteColumn);
			tv.AppendColumn (actionColumn);
		}

		private void RenderEntryName (TreeViewColumn column, CellRenderer cell, TreeModel treemodel, TreeIter iter)
		{
			Entry entry = (Entry) treemodel.GetValue (iter, COLINDEX_ENTRY);
			(cell as CellRendererText).Text = entry.Name;
		}

		private void RenderEntryLocal (TreeViewColumn column, CellRenderer cell, TreeModel treemodel, TreeIter iter)
		{
			Entry entry = (Entry) treemodel.GetValue (iter, COLINDEX_ENTRY);
			(cell as CellRendererText).Text = entry.Local;
		}

		private void RenderEntryRemote (TreeViewColumn column, CellRenderer cell, TreeModel treemodel, TreeIter iter)
		{
			Entry entry = (Entry) treemodel.GetValue (iter, COLINDEX_ENTRY);
			(cell as CellRendererText).Text = entry.Remote;
		}

		private void RenderEntryAction (TreeViewColumn column, CellRenderer cell, TreeModel treemodel, TreeIter iter)
		{
			Entry entry = (Entry) treemodel.GetValue (iter, COLINDEX_ENTRY);
			var cellRendererCombo = cell as CellRendererCombo;
			cellRendererCombo.Model = modelGitActions;
			cellRendererCombo.Editable = true;
			cellRendererCombo.TextColumn = 0;
			cellRendererCombo.Text = entry.DefaultGitAction.ToString ();
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

