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
	public class GitTests : BaseTest
	{
		[Test]
		public void IsGitDir_With_Invalid_GitDir_Should_Return_False ()
		{
			SetupGitRepos (InitialGitStatus.BareAheadOfHome);
			var configEntry = MakeConfigEntry ();
			configEntry.Local = configEntry.Local + "_invalid";
			var gitService = new GitService ();
			Assert.IsFalse(gitService.IsGitDir(configEntry.Local));
		}

		[Test]
		public void IsGitDir_With_Valid_GitDir_Should_Return_True ()
		{
			SetupGitRepos (InitialGitStatus.BareAheadOfHome);
			var configEntry = MakeConfigEntry ();
			var gitService = new GitService ();
			Assert.IsTrue(gitService.IsGitDir(configEntry.Local));
		}

		[Test]
		public void API_Exploring_Getting_MoreInfos_From_NGitPullResponse()
		{
			SetupGitRepos (InitialGitStatus.BareAheadOfHome);
			var configEntry = MakeConfigEntry ();
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
			SetupGitRepos (InitialGitStatus.BareAheadOfHome);
			var configEntry = MakeConfigEntry ();

			string output = GitOutput(configEntry);
//			Assert.AreEqual("foo" , output);
		}

		/// http://o2platform.wordpress.com/category/github/
		public string GitOutput(Entry entry)
		{
			var git = new Git(new FileRepository(ToGitDirString(entry.Local)));
			//var git = Git.Open(testRepository);
//			var repository = git.GetRepository();
			var stringWriter = new StringWriter();
			var textMonitor = new  TextProgressMonitor(stringWriter);

			var pullCommand= git.Pull();
			pullCommand.SetProgressMonitor(textMonitor);
			var pullResponse = pullCommand.Call();
			return stringWriter.ToString() + " \n\n.........\n\n " + pullResponse.ToString();
		}
	}
}

