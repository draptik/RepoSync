using System;

namespace RepoSync.Service
{
	public class BashGitCommandOutputStrategy : ICommandOutputStrategy
	{
		public void Execute (System.Diagnostics.Process proc, ICommandResponse response)
		{
			var stderr = proc.StandardError.ReadToEnd();
			var stdout = proc.StandardOutput.ReadToEnd();
			if (IsGitError(stderr)) {
				response.Success = false;
				throw new GitCommandException("FATAL GIT COMMAND Exception." + CreateOutput(proc, stdout, stderr));
			}
			else {
				response.Success = true;
				response.Msg = CreateOutput(proc, stdout, stderr);
			}
		}

		private bool IsGitError (String stderr)
		{
			// We probably have to extend this method in the near futuer, because GIT uses stderr for, well, not errors...
			return stderr.StartsWith("fatal");
		}

		private string CreateOutput (System.Diagnostics.Process proc, string stdout, string stderr)
		{
			return "stdin: " + Environment.NewLine +
					"Current working directory: " + proc.StartInfo.WorkingDirectory + " " +  Environment.NewLine +
					proc.StartInfo.FileName  + " " + proc.StartInfo.Arguments +  Environment.NewLine +
					" stdout: " + Environment.NewLine + stdout +  Environment.NewLine +
					" stderr: " + Environment.NewLine + stderr;
		}
	}
}

