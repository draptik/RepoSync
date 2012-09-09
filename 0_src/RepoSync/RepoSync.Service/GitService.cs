using System;
using RepoSync.Service.Config;

namespace RepoSync.Service
{
	public class GitService
	{
		public bool IsGitDir (string path)
		{
			ICommandRequest commandRequest = new GitRequest {
				Arguments = "log", // "ls-files"
				WorkingDirectory = path
			};

			var response = new CommandService(new GitCommandOutputStrategy()).Execute(commandRequest);
			return response.Success;
		}

		public ICommandResponse Pull (Entry entry)
		{
			ICommandRequest gitRequest = new GitRequest {
				Arguments = "pull " + entry.Source,
				WorkingDirectory = entry.Destination
			};

			return new CommandService(new GitCommandOutputStrategy()).Execute(gitRequest);
		}
	}
}

