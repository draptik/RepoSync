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

			var parsed = pullResponse.ToString()
				.Replace("with base", Environment.NewLine + "with base")
				.Replace("using", Environment.NewLine + "using");

			Msg = stringWriter.ToString() + newLine + " ......... "+ newLine
				+ parsed;

//			string fetchedFrom = pullResponse.GetFetchedFrom();
//			Msg += newLine + "fetchedFrom: " + fetchedFrom;

			FetchResult fetchResult = pullResponse.GetFetchResult();
//
//			var trackingRefUpdates = fetchResult.GetTrackingRefUpdates ();
//			foreach (var trackingRefUpdate in trackingRefUpdates) {
//				var oldObjectId = trackingRefUpdate.GetOldObjectId ().Name;
//				var newObjectId = trackingRefUpdate.GetNewObjectId ().Name;
//				Msg += newLine + "Old object id: " + oldObjectId;
//				Msg += newLine + "New object id: " + newObjectId;
//
//				var refResult = trackingRefUpdate.GetResult ();
//				Msg += newLine + "refResult: " + refResult;
//			}

			var msg = fetchResult.GetMessages();
			if (!string.IsNullOrEmpty(msg)) {
				Msg += newLine + "Result: " + newLine + msg;
			}
		}

		public string Msg { get; set; }
		public bool Success { get; set;	}
		public GitCommandException Exception { get; set; }
	}
}

