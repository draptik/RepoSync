using System;
using RepoSync.Service.Config;
using RepoSync.Service.Tests;

namespace RepoSync.Service.Tests
{
	public class BaseTest
	{
		public string createdBaseName = TestConstants.CreatedBaseName;
		public string scriptDirPath = TestConstants.ScriptDirPath;
		public string scriptDirName = TestConstants.ScriptDirName;
		public string createRepoScript = TestConstants.CreateRepoScript;



		public ICommandResponse SetupGitRepos (InitialGitStatus status)
		{
			var argument = string.Empty;
			switch (status) {
			case InitialGitStatus.BareAheadOfHome:
				argument = " bare";
				break;
			case InitialGitStatus.HomeAheadOfBare:
				argument = " home";
				break;
			default:
				break;
			}

			var script = TestConstants.ScriptDirName + TestConstants.CreateRepoScript + argument;
			ICommandRequest request = new CommandRequest ();
			request.WorkingDirectory = TestConstants.ScriptDirPath;
			request.Name = "bash";
			request.Arguments = script;
			var service = new CommandService (new BashGitCommandOutputStrategy ());
			var result = service.Execute (request);
			return result;
		}

		public Entry MakeConfigEntry ()
		{
			return MakeConfigEntry(InitJsonService());
		}

		private Entry MakeConfigEntry(JsonService service)
		{
			var config = service.SyncConfig;
			var configEntry = config.Entries[0];
			return configEntry;
		}

		public JsonService InitJsonService ()
		{
			var service = new JsonService (new IoService ());
			service.Init (@"../../testdata/config_test.json");
			return service;
		}


		public string ToGitDirString(string s)
		{
			return s.EndsWith(".git") ? s : s + "/.git";
		}


	}
}

