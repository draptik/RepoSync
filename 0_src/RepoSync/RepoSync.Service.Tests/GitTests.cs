using System;
using NUnit.Framework;
using NSubstitute;
using RepoSync.Service.Config;
using NGit.Api.Errors;
using NGit.Errors;
using NGit.Api;
using System.Collections.Generic;
using System.IO;
using NGit;
using NGit.Storage.File;

namespace RepoSync.Service.Tests
{
	[TestFixture()]
	public class GitTests
	{
		[Test]
		public void IsGitDir_With_Invalid_GitDir_Should_Return_False ()
		{
			TestHelpers.SetupGitRepos (TestHelpers.InitialGitStatus.BareAheadOfHome);
			var service = new JsonService (new IoService ());
			service.Init (@"../../testdata/config_test.json");
			var config = service.SyncConfig;
			var configEntry = config.Entries[0];
			configEntry.Source = configEntry.Source + "_invalid";
			var gitService = new GitService ();
			Assert.IsFalse(gitService.IsGitDir(configEntry.Source));
		}

		[Test]
		public void IsGitDir_With_Valid_GitDir_Should_Return_True ()
		{
			TestHelpers.SetupGitRepos (TestHelpers.InitialGitStatus.BareAheadOfHome);
			var service = new JsonService (new IoService ());
			service.Init (@"../../testdata/config_test.json");
			var config = service.SyncConfig;
			var configEntry = config.Entries[0];
			configEntry.Source = configEntry.Source;
			var gitService = new GitService ();
			Assert.IsTrue(gitService.IsGitDir(configEntry.Source));
		}

		[Test]
		public void API_Exploring_Getting_MoreInfos_From_NGitPullResponse()
		{
			TestHelpers.SetupGitRepos (TestHelpers.InitialGitStatus.BareAheadOfHome);
			var service = new JsonService (new IoService ());
			service.Init (@"../../testdata/config_test.json");
			var config = service.SyncConfig;
			var configEntry = config.Entries[0];
			configEntry.Source = configEntry.Source;
			var gitService = new GitService ();

			// Method under test:
//			PullResult result = gitService.Pull(configEntry);
//
//
//			Assert.IsTrue (result.IsSuccessful());
//
//			// MergeResult
//			MergeCommandResult mergeCommandResult = result.GetMergeResult();
//			// MergeStatus
//			MergeStatus mergeStatus = mergeCommandResult.GetMergeStatus();
//			Assert.IsTrue (mergeStatus.IsSuccessful());
//			// MergeConflicts
////			IDictionary<string, int[][]> conflicts = mergeCommandResult.GetConflicts();
////			//
////			NGit.ObjectId[] mergedCommits = mergeCommandResult.GetMergedCommits();
//
//			// Fetched From
//			string fetchedFrom = result.GetFetchedFrom();
//			Assert.AreEqual ("origin", fetchedFrom);
//
//			// FetchResults
//			Assert.IsNotNull(result.GetFetchResult());
//			NGit.Transport.FetchResult fetchResult = result.GetFetchResult();
//			Assert.IsNotNullOrEmpty(fetchResult.GetMessages());

		}

		[Test]
		public void API_Exploring_OutputParsing()
		{
			TestHelpers.SetupGitRepos (TestHelpers.InitialGitStatus.BareAheadOfHome);
			var service = new JsonService (new IoService ());
			service.Init (@"../../testdata/config_test.json");
			var config = service.SyncConfig;
			var configEntry = config.Entries[0];

			string output = GitOutput(configEntry);
//			Assert.AreEqual("foo" , output);
		}

		/// http://o2platform.wordpress.com/category/github/
		public string GitOutput(Entry entry)
		{
			var git = new Git(new FileRepository(ToGitDirString(entry.Source)));
			//var git = Git.Open(testRepository);
//			var repository = git.GetRepository();
			var stringWriter = new StringWriter();
			var textMonitor = new  TextProgressMonitor(stringWriter);

			var pullCommand= git.Pull();
			pullCommand.SetProgressMonitor(textMonitor);
			var pullResponse = pullCommand.Call();
			return stringWriter.ToString() + " \n\n.........\n\n " + pullResponse.ToString();

		}

		private string ToGitDirString(string s)
		{
			return s.EndsWith(".git") ? s : s + "/.git";
		}
	}
}

