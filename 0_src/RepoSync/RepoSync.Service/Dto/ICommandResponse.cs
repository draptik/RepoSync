using System;

namespace RepoSync.Service
{
	public interface ICommandResponse
	{
		String Msg { get; set; }
		bool Success { get;	set; }
		GitCommandException Exception {	get; set; }
	}
}

