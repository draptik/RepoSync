using System;

//using RepoSync.GuiGtk.Widgets;
using RepoSync.Service.Config;
using RepoSync.Service;

namespace RepoSync.GuiGtk
{
	public class MainController
	{
		private SyncConfig syncConfig;
		private ChooseConfigWidget chooseConfigWidget;
		private RepoTreeViewWidget repoTreeViewWidget;
		private SyncActionWidget syncActionWidget;
		private SyncOutputWidget syncOutputWidget;

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

			syncAction.PullStarted += HandleBtnPullStarted;
		}

		void HandleBtnPullStarted ()
		{
			// todo...
			if (this.syncConfig != null) {

				var gitService = new GitService ();
				foreach (var entry in syncConfig.Entries) {
					var response = gitService.Pull (entry);
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

