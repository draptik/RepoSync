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
		private static string newLine = Environment.NewLine + "* PUSH > ";

		public NGitPushResultAdapter (Iterable<PushResult> pushResults)
		{
			try {
				foreach (var pushResult in pushResults) {

					Msg += newLine + "PushResult......";

					var remoteUpdates = pushResult.GetRemoteUpdates ();
					foreach (var remoteUpdate in remoteUpdates) {

						var oldObjectId = remoteUpdate.GetTrackingRefUpdate ().GetOldObjectId ().Name;
						var newObjectId = remoteUpdate.GetTrackingRefUpdate ().GetNewObjectId ().Name;
						Msg += newLine + "Old object id: " + oldObjectId;
						Msg += newLine + "New object id: " + newObjectId;

						var refResult = remoteUpdate.GetTrackingRefUpdate ().GetResult ();
						Msg += newLine + "refResult: " + refResult;

						var status = remoteUpdate.GetStatus ();
						Msg += newLine + "STATUS: " + status;

						UpdateSuccessStatus (status);

						if (remoteUpdate.GetMessage () != null) {
							Msg += newLine + remoteUpdate.GetMessage ();
						}

					}
				}
			} catch (System.Exception ex) {
				this.Exception = new GitCommandException ("Error NGitPushResultAdapter", ex);
			}
		}


		public string Msg { get; set; }
		public bool Success { get; set; }
		public GitCommandException Exception { get; set; }

		private void UpdateSuccessStatus (RemoteRefUpdate.Status status)
		{
			if (status == RemoteRefUpdate.Status.OK || status == RemoteRefUpdate.Status.UP_TO_DATE) {
				Success = true;
			} else {
				Success = false;
			}
		}
	}

}

