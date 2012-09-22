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
	public class MainIntegrationTests : BaseTest
	{
		private string sandbox;

		[SetUp]
		public void SetUp ()
		{
			var sandboxDir = new DirectoryInfo (new PathFinder().UserHomePath + "/tmp");
			Assert.IsTrue (sandboxDir.Exists);

			sandbox = sandboxDir.FullName;
		}

		[Test]
		public void Setup_Create_GitRepos_HomeAheadOfBare ()
		{
			var result = SetupGitRepos (InitialGitStatus.HomeAheadOfBare);

			Assert.IsTrue (result.Success);

			var sandboxDir = new DirectoryInfo (sandbox);

			DirectoryInfo[] actual = sandboxDir.GetDirectories (createdBaseName + "*", SearchOption.TopDirectoryOnly);

			Assert.IsNotNull (actual);
			Assert.IsTrue (actual.Length >= 1);
		}

		[Test]
		public void Setup_Create_GitRepos_BareAheadOfHome ()
		{
			var result = SetupGitRepos (InitialGitStatus.BareAheadOfHome);

			Assert.IsTrue (result.Success);

			var sandboxDir = new DirectoryInfo (sandbox);

			DirectoryInfo[] actual = sandboxDir.GetDirectories (createdBaseName + "*", SearchOption.TopDirectoryOnly);

			Assert.IsNotNull (actual);
			Assert.IsTrue (actual.Length >= 1);
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
		public void Pull_With_ValidGitDirs_ShouldNot_ThrowException ()
		{
			var result = SetupGitRepos (InitialGitStatus.BareAheadOfHome);
			Assert.IsTrue (result.Success);

			var service = base.InitJsonService();
			var config = service.SyncConfig;

			var gitService = new GitService ();
			foreach (var entry in config.Entries) {
				Assert.DoesNotThrow(() => gitService.Pull (entry));
				Assert.DoesNotThrow(() => gitService.Push (entry));
			}
		}

		[Test]
		public void Push_WithInvalid_GitDirs_Should_ThrowException ()
		{
			var result = SetupGitRepos (InitialGitStatus.BareAheadOfHome);

			Assert.IsTrue (result.Success);

			var configEntry = MakeConfigEntry();
			configEntry.Local = configEntry.Local + "_invalid";
			configEntry.Remote = configEntry.Remote;

			var gitService = new GitService ();
			var ex = Assert.Throws<JGitInternalException>(() => gitService.Push (configEntry));
			Assert.That (ex.InnerException is NoRemoteRepositoryException);
		}

		[Test]
		public void Push_WithValid_Setup_Should_Return_SuccessTrue()
		{
			var result = SetupGitRepos (InitialGitStatus.HomeAheadOfBare);
			Assert.IsTrue (result.Success);

			var service = base.InitJsonService();
			var config = service.SyncConfig;

			var gitService = new GitService ();
			foreach (var entry in config.Entries) {
				var resultFromPush = gitService.Push (entry);
				Assert.That(resultFromPush.Msg.Contains("STATUS: OK"));
				Assert.IsTrue(resultFromPush.Success);
			}
		}
	}
}

