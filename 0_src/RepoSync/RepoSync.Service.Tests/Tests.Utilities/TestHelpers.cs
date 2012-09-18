using System;

namespace RepoSync.Service.Tests
{
	public class TestHelpers
	{
		public const string scriptDirPath = "../../";
		public const string scriptDirName = "create_scripts/";
		public const string createRepoScript = "_create_git_repos.sh";
		public const string createdBaseName = "Z_deleteme_git_repos";

		public TestHelpers ()
		{
		}

//		public static ICommandResponse SetupGitRepos (InitialGitStatus status)
//		{
//			var argument = string.Empty;
//			switch (status) {
//			case InitialGitStatus.BareAheadOfHome:
//				argument = " bare";
//				break;
//			case InitialGitStatus.HomeAheadOfBare:
//				argument = " home";
//				break;
//			default:
//				break;
//			}
//
//			var script = scriptDirName + createRepoScript + argument;
//			ICommandRequest request = new CommandRequest ();
//			request.WorkingDirectory = scriptDirPath;
//			request.Name = "bash";
//			request.Arguments = script;
//			var service = new CommandService (new BashGitCommandOutputStrategy ());
//			var result = service.Execute (request);
//			return result;
//		}
	}
}

