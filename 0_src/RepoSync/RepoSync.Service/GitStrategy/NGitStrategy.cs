using System;
using NGit.Api;
using NGit.Storage.File;
using NGit.Transport;
using Sharpen;
using NGit;

namespace RepoSync.Service
{
	public class NGitStrategy : IGitStrategy
	{
		public NGitStrategy ()
		{
		}

		public bool IsGitDir (string path)
		{
			return RepositoryCache.FileKey.IsGitRepository(ToGitDirString(path), NGit.Util.FS.Detect());
		}

		public ICommandResponse Push (string path)
		{
			Git git = new Git(new FileRepository(ToGitDirString(path)));
			Iterable<PushResult> pushResults = git.Push().Call();
			ICommandResponse result = new NGitPushResultAdapter(pushResults);
			return result;
		}

		public ICommandResponse Pull (string path, string destPath)
		{
			var git = new Git(new FileRepository(ToGitDirString(path)));
			var pullCommand = git.Pull();
			ICommandResponse result = new NGitPullResultAdapter(pullCommand);
			return result;
		}

		private string ToGitDirString(string s)
		{
			return s.EndsWith(".git") ? s : s + "/.git";
		}
	}
}

