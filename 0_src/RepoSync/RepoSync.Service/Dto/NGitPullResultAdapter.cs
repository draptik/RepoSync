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
		public NGitPullResultAdapter (PullCommand pullCommand)
		{
			var stringWriter = new StringWriter();
			var textMonitor = new  TextProgressMonitor(stringWriter);
			pullCommand.SetProgressMonitor(textMonitor);

			var pullResponse = pullCommand.Call();

			Success = pullResponse.IsSuccessful();

			// TODO Add NGitCommandOutputStrategy
			Msg = stringWriter.ToString() + " \n\n.........\n\n " + pullResponse.ToString();
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

