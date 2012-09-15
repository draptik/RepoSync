using System;
using NUnit.Framework;
using System.IO;

using NGit;
using NGit.Api;
using NGit.Storage.File;
using NGit.Transport;
using NGit.Errors;
using NGit.Api.Errors;


namespace RepoSync.Service.Tests
{
	[TestFixture()]
	public class MainIntegrationTests
	{
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
			var actual = new DirectoryInfo (TestHelpers.scriptDirPath + TestHelpers.scriptDirName);
			Assert.IsTrue (actual.Exists);
		}

		[Test()]
		public void CreateScript_Should_ExistAndBeNotReadOnly ()
		{
			var script = TestHelpers.scriptDirPath + TestHelpers.scriptDirName + TestHelpers.createRepoScript;

			var actual = new FileInfo (script);
			Assert.IsTrue (actual.Exists);
			Assert.IsFalse (actual.IsReadOnly);
		}

		[Test()]
		public void Setup_Create_GitRepos ()
		{
			var result = TestHelpers.SetupGitRepos (TestHelpers.InitialGitStatus.BareAheadOfHome);

			Assert.IsTrue (result.Success);

			var sandboxDir = new DirectoryInfo (sandbox);

			DirectoryInfo[] actual = sandboxDir.GetDirectories (TestHelpers.createdBaseName + "*", SearchOption.TopDirectoryOnly);

			Assert.IsNotNull (actual);
			Assert.IsTrue (actual.Length >= 1); // '>=1' instead of '==1' for tests around midnight...
		}

		[Test()]
		public void Pull_With_ValidGitDirs_ShouldNot_ThrowException ()
		{
			var result = TestHelpers.SetupGitRepos (TestHelpers.InitialGitStatus.BareAheadOfHome);

			Assert.IsTrue (result.Success);

			var service = new JsonService (new IoService ());
			service.Init (@"../../testdata/config_test.json");

			var config = service.SyncConfig;

			var gitService = new GitService ();
			foreach (var entry in config.Entries) {
				Assert.DoesNotThrow(() => gitService.Pull (entry));
			}
		}

		[Test]
		public void Push_WithInvalid_GitDirs_Should_ThrowException ()
		{
			var result = TestHelpers.SetupGitRepos (TestHelpers.InitialGitStatus.BareAheadOfHome);

			Assert.IsTrue (result.Success);

			var service = new JsonService (new IoService ());
			service.Init (@"../../testdata/config_test.json");

			var config = service.SyncConfig;

			var gitService = new GitService ();

			var configEntry = config.Entries[0];
			configEntry.Source = configEntry.Source + "_invalid";
			configEntry.Destination = configEntry.Destination;
			var ex = Assert.Throws<JGitInternalException>(() => gitService.Push (config.Entries[0]));
			Assert.That (ex.InnerException is NoRemoteRepositoryException);
		}
	}
}

