using System;
//using RepoSync.GuiGtk.Widgets;
using RepoSync.Service.Config;

namespace RepoSync.GuiGtk
{
	public class MainController
	{
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
		}

		private void HandleOnSyncConfigChangedStarted (SyncConfig syncConfig)
		{
			repoTreeViewWidget.Update(syncConfig);
		}
	}
}

