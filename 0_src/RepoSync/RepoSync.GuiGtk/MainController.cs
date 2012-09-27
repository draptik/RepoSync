using System;
using RepoSync.Service.Config;
using RepoSync.Service;

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
			syncActionWidget.DefaultGitActionForAllStarted += HandleBtnDefaultGitActionForAllStarted;
		}

		private bool IsSyncConfigPresent 
		{ 
			get { return syncConfig != null && syncConfig.Entries.Count > 0; } 
		}

		private void HandleBtnDefaultGitActionForAllStarted ()
		{
			if (this.syncConfig != null) {

				syncOutputWidget.ClearContent ();

				var gitService = new GitService ();
				foreach (var entry in syncConfig.Entries) {

					ICommandResponse response = null;
					switch (entry.DefaultGitAction) {
					case DefaultGitAction.Pull:
						response = gitService.Pull (entry);
						break;
					case DefaultGitAction.Push:
						response = gitService.Push (entry);
						break;
					}

					var title = MakeTitle(entry);
					syncOutputWidget.Content(response.Success, entry.Name, title + response.Msg + EntryFooter);
				}
			}
		}

		private void HandleOnSyncConfigChangedStarted (SyncConfig syncConfig)
		{
			this.syncConfig = syncConfig;
			repoTreeViewWidget.Update (syncConfig);

			syncActionWidget.IsActive = IsSyncConfigPresent;
		}



		private const int LENGTH_LINE = 80;

		private string MakeTitle(Entry entry)
		{
			var dashLine = "* " + new String('=', LENGTH_LINE);
			return Environment.NewLine + dashLine + Environment.NewLine + 
				"* Repo: " + entry.Name + Environment.NewLine +
				dashLine + Environment.NewLine;
		}

		private string EntryFooter 
		{ 
			get 
			{ 
				var singleDasLine = "* "  + new String('-', LENGTH_LINE);
				return Environment.NewLine + singleDasLine + Environment.NewLine; 
			}
		}


	}
}

