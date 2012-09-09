using System;
using System.Diagnostics;

namespace RepoSync.Service
{
	public interface ICommandOutputStrategy
	{
		void Execute (Process proc, ICommandResponse response);
	}
}

