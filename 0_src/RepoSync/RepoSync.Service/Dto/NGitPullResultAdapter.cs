using System;
using NGit.Api;
using NGit.Storage.File;
using NGit.Transport;
using Sharpen;
using NGit;
using System.IO;

namespace RepoSync.Service
{
	class NGitPullResultAdapter : ICommandResponse
	{
		private static string newLine = Environment.NewLine + "* PULL > ";

		public NGitPullResultAdapter (PullCommand pullCommand)
		{
			var stringWriter = new StringWriter();
			var textMonitor = new  TextProgressMonitor(stringWriter);
			pullCommand.SetProgressMonitor(textMonitor);

			var pullResponse = pullCommand.Call();

			Success = pullResponse.IsSuccessful();

			Msg = stringWriter.ToString() + newLine + " ......... "+ newLine
				+ pullResponse.ToString();
			
			string fetchedFrom = pullResponse.GetFetchedFrom();
			Msg += "fetchedFrom: " + fetchedFrom;

			FetchResult fetchResult = pullResponse.GetFetchResult();

			var trackingRefUpdates = fetchResult.GetTrackingRefUpdates ();
			foreach (var trackingRefUpdate in trackingRefUpdates) {
				var oldObjectId = trackingRefUpdate.GetOldObjectId ().Name;
				var newObjectId = trackingRefUpdate.GetNewObjectId ().Name;
				Msg += "Old object id: " + oldObjectId;
				Msg += "New object id: " + newObjectId;

				var refResult = trackingRefUpdate.GetResult ();
				Msg += "refResult: " + refResult;
			}



			var msg = fetchResult.GetMessages();
			if (!string.IsNullOrEmpty(msg)) {
				Msg += newLine + "FetchResult.GetMessages() : " + msg;
			}
		}

		private string msg;
		public string Msg 
		{ 
			get { return msg; } 
			set { msg = !string.IsNullOrEmpty (value) ? newLine + value : string.Empty; } 
		}

		public bool Success { get; set;	}
		public GitCommandException Exception { get; set; }
	}
}

