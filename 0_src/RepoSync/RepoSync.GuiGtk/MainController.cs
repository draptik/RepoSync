using System;
using RepoSync.Service.Config;
using RepoSync.Service;
using System.Collections.Generic;

namespace RepoSync.GuiGtk
{
	public class MainController
	{
		private SyncConfig syncConfig;
		private readonly ChooseConfigWidget chooseConfigWidget;
		private readonly RepoTreeViewWidget repoTreeViewWidget;
		private readonly SyncActionWidget syncActionWidget;
		private readonly SyncOutputWidget syncOutputWidget;

		public MainController (
			ChooseConfigWidget chooseConfig,
			RepoTreeViewWidget repoTreeView,
		 	SyncActionWidget syncAction,
		  	SyncOutputWidget syncOutput)
		{
			this.chooseConfigWidget = chooseConfig;
			this.repoTreeViewWidget = repoTreeView;
			this.syncActionWidget = syncAction;
			this.syncOutputWidget = syncOutput;

			// Register events
			chooseConfigWidget.OnSyncConfigChangedStarted += HandleOnSyncConfigChangedStarted;
			repoTreeViewWidget.SingleSelectionChangeStarted += HandleSingleSelectionChangeStarted;
			syncActionWidget.DefaultGitActionForAllStarted += HandleBtnDefaultGitActionForAllStarted;
			syncActionWidget.ToggleAllReposStarted += HandleBtnToggleAllReposStarted;
		}

		private bool IsSyncConfigPresent { 
			get { return syncConfig != null && syncConfig.Entries.Count > 0; } 
		}

		#region Event Handlers

		private void HandleBtnToggleAllReposStarted ()
		{
			repoTreeViewWidget.ToggleAll (syncActionWidget.IsSelectAllChecked);
			syncActionWidget.IsActive = AreAnyReposSelected();
		}

		private void HandleBtnDefaultGitActionForAllStarted ()
		{
			if (this.syncConfig != null) {

				syncOutputWidget.ClearContent ();

				var entries = repoTreeViewWidget.GetCheckedEntries ();

				if (entries.Count > 0) {
					var gitService = new GitService ();

					foreach (var entry in entries) {
						ICommandResponse response = null;
						switch (entry.DefaultGitAction) {
						case DefaultGitAction.Pull:
							response = gitService.Pull (entry);
							break;
						case DefaultGitAction.Push:
							response = gitService.Push (entry);
							break;
						}

						syncOutputWidget.Content (response.Success, 
					                         entry.Name, 
					                         MakeTitle (entry) + response.Msg + EntryFooter);
					}
				}
			}
		}

		private void HandleOnSyncConfigChangedStarted (SyncConfig syncConfig)
		{
			this.syncConfig = syncConfig;
			repoTreeViewWidget.Update (syncConfig);

			syncActionWidget.IsSelectAllChecked = IsSyncConfigPresent;
			syncActionWidget.IsActive = AreAnyReposSelected();
		}

		private void HandleSingleSelectionChangeStarted (bool areAllEntriesSelected)
		{
			syncActionWidget.IsSelectAllChecked = areAllEntriesSelected;
			syncActionWidget.IsActive = AreAnyReposSelected();

		}

		#endregion

		private bool AreAnyReposSelected ()
		{
			var entries = repoTreeViewWidget.GetCheckedEntries ();
			return entries != null && entries.Count > 0;
		}

		private const int LENGTH_LINE = 80;

		private string MakeTitle (Entry entry)
		{
			var dashLine = "* " + new String ('=', LENGTH_LINE);
			return Environment.NewLine + dashLine + Environment.NewLine + 
				"* Repo: " + entry.Name + Environment.NewLine +
				dashLine + Environment.NewLine;
		}

		private string EntryFooter { 
			get { 
				var singleDasLine = "* " + new String ('-', LENGTH_LINE);
				return Environment.NewLine + singleDasLine + Environment.NewLine; 
			}
		}
	}
}

