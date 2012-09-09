using System;
using RepoSync.Service;

namespace RepoSync.Cli
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args != null && args.Length != 1) {
				throw new ArgumentOutOfRangeException("Provide config file name!");
			}

			string configFilename = args[0];

			// get configuration...
			var service = new JsonService(new IoService());
			service.Init(configFilename);

			var config = service.SyncConfig;

			// git pull...
			var gitService = new GitService();
			foreach (var entry in config.Entries) {
				var response = gitService.Pull(entry);
				Console.WriteLine ("response was: " + response.Msg);
			}

		}
	}
}
