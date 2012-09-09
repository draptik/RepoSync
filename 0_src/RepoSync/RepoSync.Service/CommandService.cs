using System;
using System.Diagnostics;

namespace RepoSync.Service
{
	public class CommandService
	{
		private readonly IoService ioService;
		private readonly ICommandOutputStrategy outputStrategy;

		public CommandService () 
			: this(new DefaultCommandOutputStrategy())
		{
		}

		public CommandService (ICommandOutputStrategy outputStrategy)
		{
			this.ioService = new IoService();	
			this.outputStrategy = outputStrategy;
		}

		public ICommandResponse Execute(ICommandRequest commandRequest)
		{
			var result = new CommandResponse();

			if (!this.ioService.DirectoryIsValid(commandRequest.WorkingDirectory)) {
				result.Msg = "Directory not found.";
				result.Success = false;
				return result;
			}

//			var pwd = new DirectoryInfo(commandRequest.WorkingDirectory);
//			var pwdCurrent = Directory.GetCurrentDirectory();

			var startInfo = new ProcessStartInfo {
				WorkingDirectory = commandRequest.WorkingDirectory,
				FileName = commandRequest.Name,
				Arguments = commandRequest.Arguments,
				RedirectStandardInput = true,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				UseShellExecute = false
			};

			try {
				Process proc = new Process{StartInfo = startInfo};
				proc.Start();

				outputStrategy.Execute(proc, result);

				proc.WaitForExit();
			} catch (Exception ex) {
				var msg = "Error in executing process.";
				throw new GitCommandException(msg, ex);
			}

			return result;
		}
	}

}

