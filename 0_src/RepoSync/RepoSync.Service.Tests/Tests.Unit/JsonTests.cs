using System;
using NUnit.Framework;

namespace RepoSync.Service.Tests
{
	[TestFixture()]
	public class JsonTests
	{
		[Test()]
		public void Read_Json_From_ValidFile_Returns_Valid_SyncConfig ()
		{
			var service = new JsonService (new IoService ());
			String fileName = @"../../testdata/config_test.json";
			service.Init (fileName);
			Assert.IsNotNull (service.SyncConfig);
			Assert.AreEqual ("config_test.json", service.SyncConfig.Name);
			Assert.AreEqual (DefaultGitAction.Push, service.SyncConfig.Entries [0].DefaultGitAction);
		}

		[Test()]
		public void Read_Json_From_ValidFile_Returns_Valid_Entries ()
		{
			var service = new JsonService (new IoService ());
			String fileName = @"../../testdata/config_test.json";
			service.Init (fileName);
			Assert.IsNotNull (service.SyncConfig);
			Assert.That (service.SyncConfig.Entries.Count > 0);
			foreach (var entry in service.SyncConfig.Entries) {
				Assert.That (entry.Id is System.Guid);
				Assert.IsNotNullOrEmpty (entry.Local);
				Assert.IsNotNullOrEmpty (entry.Name);
				Assert.IsNotNullOrEmpty (entry.Remote);
			}
		}
	}
}

