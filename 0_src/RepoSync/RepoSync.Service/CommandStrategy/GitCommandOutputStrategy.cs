using System;

namespace RepoSync.Service
{
	public class GitCommandOutputStrategy : ICommandOutputStrategy
	{
		public void Execute (System.Diagnostics.Process proc, ICommandResponse response)
		{
			string stdin = proc.StandardInput.ToString();
			string stderr = proc.StandardError.ReadToEnd();
			string stdout = proc.StandardOutput.ReadToEnd();
			if (IsGitError(stderr)) {
				response.Success = false;
				throw new GitCommandException("FATAL GIT COMMAND Exception." + "stdin: " + stdin + " | stdout: " + stdout + " | stderr: " + stderr);
			}
			else {
				response.Success = true;
				response.Msg = "stdin: " + stdin + " stdout: " + stdout + " stderr: " + stderr;
			}

		}

		private bool IsGitError (String stderr)
		{
			// We probably have to extend this method in the near futuer, because GIT uses stderr for, well, not errors...
			return stderr.StartsWith("fatal");
		}
	}
}

