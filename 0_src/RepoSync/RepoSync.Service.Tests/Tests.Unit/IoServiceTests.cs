using System;
using NUnit.Framework;

namespace RepoSync.Service.Tests
{
	[TestFixture()]
	public class IoServiceTests
	{
		[Test()]
		[TestCase(null)]
		[TestCase("")]
		public void DirectoryIsValid_With_EmptyOrNull_Dir_Should_Return_False (String input)
		{
			var service = new IoService();
			bool result = service.DirectoryIsValid(input);
			Assert.IsFalse(result);
		}

		[Test()]
		public void DirectoryIsValid_With_Invalid_Dir_Should_Return_False ()
		{
			var service = new IoService();
			bool result = service.DirectoryIsValid("invalid/path");
			Assert.IsFalse(result);
		}

		[Test()]
		public void DirectoryIsValid_With_Valid_Dir_Should_Return_True ()
		{
			var service = new IoService();
			bool result = service.DirectoryIsValid("../../testdata");
			Assert.IsTrue(result);
		}
	}
}

