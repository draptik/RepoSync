using System;
using RepoSync.Service.Config;

namespace RepoSync.Service
{
	public class BashGitStrategy : IGitStrategy
	{
		public BashGitStrategy ()
		{
		}

		public bool IsGitDir (string path)
		{
			ICommandRequest commandRequest = new GitRequest {
				Arguments = "log", // "ls-files"
				WorkingDirectory = path
			};

			var response = new CommandService (new BashGitCommandOutputStrategy ()).Execute (commandRequest);
			return response.Success;
		}

		public ICommandResponse Push (string path)
		{
			ICommandRequest gitRequest = new GitRequest {
                               Arguments = "push ",
                               WorkingDirectory = path
                       };
			return new CommandService (new BashGitCommandOutputStrategy ()).Execute (gitRequest);
//			return null;
		}

		public ICommandResponse Pull (string src, string dest)
		{
			ICommandRequest gitRequest = new GitRequest {
                               Arguments = "pull " + src,
                               WorkingDirectory = dest
                       };

			return new CommandService (new BashGitCommandOutputStrategy ()).Execute (gitRequest);
//			return null;
		}
	}
}

