using System;
using NUnit.Framework;
using NSubstitute;

namespace RepoSync.Service.Tests
{
	[TestFixture()]
	public class CommandServiceTests
	{
		[Test()]
		public void Execute_With_Invalid_WorkindDirectory_Should_ReturnFailure ()
		{
			ICommandRequest request = Substitute.For<ICommandRequest>();
			request.WorkingDirectory.Returns("");
			var service = new CommandService();
			var result = service.Execute(request);
			Assert.IsFalse(result.Success);
		}

	}
}

