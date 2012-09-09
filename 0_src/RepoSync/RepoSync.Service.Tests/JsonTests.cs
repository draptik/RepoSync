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
			var service = new JsonService(new IoService());
			String fileName = @"../../testdata/config_test.json";
			service.Init(fileName);
			Assert.IsNotNull(service.SyncConfig);
			Assert.AreEqual("config_test.json", service.SyncConfig.Name);
		}
	}
}

