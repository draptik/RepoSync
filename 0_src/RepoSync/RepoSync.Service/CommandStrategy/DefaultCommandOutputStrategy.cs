using System;
using System.Diagnostics;

namespace RepoSync.Service
{
	public class DefaultCommandOutputStrategy : ICommandOutputStrategy
	{
		public void Execute (Process proc, ICommandResponse response)
		{
			string stderr = proc.StandardError.ReadToEnd();
			string stdout = proc.StandardOutput.ReadToEnd();

			if (!String.IsNullOrEmpty(stderr)) {
				response.Success = false;
				throw new GitCommandException(stdout + "| " + stderr);
			}
		}
	}


}

