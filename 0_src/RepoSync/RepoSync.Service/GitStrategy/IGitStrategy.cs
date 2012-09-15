using System;

namespace RepoSync.Service
{
	public interface IGitStrategy
	{
		bool IsGitDir (string path);
		ICommandResponse Push (string path);
		ICommandResponse Pull (string srcPath, string destPath);
	}
}

