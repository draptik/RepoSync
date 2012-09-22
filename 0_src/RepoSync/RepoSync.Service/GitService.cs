using System;
using RepoSync.Service.Config;

namespace RepoSync.Service
{
	public class GitService
	{
		private readonly IGitStrategy gitStrategy;

		public GitService () : this(new NGitStrategy())
		{
		}

		public GitService (IGitStrategy gitStrategy)
		{
			this.gitStrategy = gitStrategy;
		}

		public bool IsGitDir (string path)
		{
			return gitStrategy.IsGitDir(path);
		}

		public ICommandResponse Pull (Entry entry)
		{
			return gitStrategy.Pull(entry.Local, entry.Remote);
		}

		public ICommandResponse Push (Entry entry)
		{
			return gitStrategy.Push(entry.Local);
		}
	}
}

