using System;

namespace RepoSync.Service
{
	public class GitRequest : CommandRequest, ICommandRequest
	{
		public GitRequest ()
		{
			this.Name = "git";
		}
	}
}

