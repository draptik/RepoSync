using System;
using NUnit.Framework;
using System.IO;

namespace RepoSync.Service.Tests
{
	[TestFixture()]
	public class MainIntegrationTests
	{
		private enum InitialGitStatus {
			HomeAheadOfBare,
			BareAheadOfHome
		}

		private const string scriptDirPath = "../../";
		private const string scriptDirName = "create_scripts/";
		private const string createRepoScript = "_create_git_repos.sh";
		private const string createdBaseName = "Z_deleteme_git_repos";

		private string sandbox;

		[SetUp]
		public void SetUp ()
		{
			var sandboxDir = new DirectoryInfo (new PathFinder().UserHomePath + "/tmp");
			Assert.IsTrue (sandboxDir.Exists);

			sandbox = sandboxDir.FullName;
		}


		[Test()]
		public void CreateScriptDir_Should_Exist ()
		{
			var actual = new DirectoryInfo (scriptDirPath + scriptDirName);
			Assert.IsTrue (actual.Exists);
		}

		[Test()]
		public void CreateScript_Should_ExistAndBeNotReadOnly ()
		{
			var script = scriptDirPath + scriptDirName + createRepoScript;

			var actual = new FileInfo (script);
			Assert.IsTrue (actual.Exists);
			Assert.IsFalse (actual.IsReadOnly);
		}

		[Test()]
		public void Setup_Create_GitRepos ()
		{
			var result = SetupGitRepos (InitialGitStatus.BareAheadOfHome);

			Assert.IsTrue (result.Success);

			var sandboxDir = new DirectoryInfo (sandbox);

			DirectoryInfo[] actual = sandboxDir.GetDirectories (createdBaseName + "*", SearchOption.TopDirectoryOnly);

			Assert.IsNotNull (actual);
			Assert.IsTrue (actual.Length >= 1); // '>=1' instead of '==1' for tests around midnight...
		}

		[Test()]
		public void Pull ()
		{
			var result = SetupGitRepos (InitialGitStatus.BareAheadOfHome);

			Assert.IsTrue (result.Success);

			var service = new JsonService (new IoService ());
			service.Init (@"../../testdata/config_test.json");

			var config = service.SyncConfig;

			var gitService = new GitService ();
			foreach (var entry in config.Entries) {
				var response = gitService.Pull (entry);
				Assert.IsTrue (response.Success);
			}
		}

		[Test()]
		public void Push ()
		{
			var result = SetupGitRepos (InitialGitStatus.HomeAheadOfBare);

			Assert.IsTrue (result.Success);

			var service = new JsonService (new IoService ());
			service.Init (@"../../testdata/config_test.json");

			var config = service.SyncConfig;

			var gitService = new GitService ();
			foreach (var entry in config.Entries) {
				var response = gitService.Push (entry);
				Assert.IsTrue (response.Success);
			}

		}

		private ICommandResponse SetupGitRepos (InitialGitStatus status)
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

			var script = scriptDirName + createRepoScript + argument;
			ICommandRequest request = new CommandRequest ();
			request.WorkingDirectory = scriptDirPath;
			request.Name = "bash";
			request.Arguments = script;
			var service = new CommandService (new GitCommandOutputStrategy ());
			var result = service.Execute (request);
			return result;
		}
	}
}

