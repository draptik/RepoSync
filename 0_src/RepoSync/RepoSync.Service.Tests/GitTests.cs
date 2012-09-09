using System;
using NUnit.Framework;
using NSubstitute;
using RepoSync.Service.Config;

namespace RepoSync.Service.Tests
{
	[TestFixture()]
	public class GitTests
	{
		[Test()]
		public void IsGitDir_With_ValidDir_Should_Return_True ()
		{
			String path = ".";
			var service = new GitService();
			bool result = service.IsGitDir(path);
			Assert.IsTrue(result);
		}

		[Test()]
		public void Pull_ValidRepos_UpToDate_Returns_NothingToDoMsg ()
		{
			GitService srv = new GitService();

			Entry entry = Substitute.For<Entry>();
			entry.Name.Returns("dummy");
			entry.Source.Returns("dummy");
			entry.Destination.Returns("dummy");

			ICommandResponse response = srv.Pull(entry);

			Assert.IsFalse(response.Success);
		}
	}
}

