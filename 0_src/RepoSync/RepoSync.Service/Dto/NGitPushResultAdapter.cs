using System;
using NGit.Api;
using NGit.Storage.File;
using NGit.Transport;
using Sharpen;
using NGit;

namespace RepoSync.Service
{
	class NGitPushResultAdapter : ICommandResponse
	{
		public NGitPushResultAdapter (Iterable<PushResult> pushResults)
		{
			foreach (var pushResult in pushResults) {
				var remoteUpdates = pushResult.GetRemoteUpdates ();
				foreach (var remoteUpdate in remoteUpdates) {

					// TODO Add ISSUCCESFULL etc
					// TODO Add NGitCommandOutputStrategy
					Msg += remoteUpdate.GetStatus ();
				}
			}
		}

		public string Msg {
			get;
			set;
		}

		public bool Success {
			get;
			set;
		}

		public GitCommandException Exception {
			get;
			set;
		}
	}

}

