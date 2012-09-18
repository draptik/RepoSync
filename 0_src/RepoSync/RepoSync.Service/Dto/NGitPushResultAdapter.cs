using System;
using NGit;
using NGit.Api;
using NGit.Storage.File;
using NGit.Transport;
using Sharpen;

namespace RepoSync.Service
{
	class NGitPushResultAdapter : ICommandResponse
	{
		public NGitPushResultAdapter (Iterable<PushResult> pushResults)
		{
			foreach (var pushResult in pushResults) {
				var remoteUpdates = pushResult.GetRemoteUpdates ();
				foreach (var remoteUpdate in remoteUpdates) {

					Msg += remoteUpdate.GetStatus ();
				}
			}
		}

		public string Msg {	get; set; }
		public bool Success { get; set; }
		public GitCommandException Exception { get; set; }
	}

}

