using System;

namespace RepoSync.Service
{
	public class CommandResponse : ICommandResponse
	{
		public String Msg {
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

