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

		private void HandleBtnDefaultGitActionForAllStarted ()
		{
			if (this.syncConfig != null) {

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

					syncOutputWidget.Content(response.Msg);
				}
			}
		}

		private void HandleOnSyncConfigChangedStarted (SyncConfig syncConfig)
		{
			this.syncConfig = syncConfig;
			repoTreeViewWidget.Update (syncConfig);
		}
	}
}

