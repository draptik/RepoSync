using System;

namespace RepoSync.Service
{
	public class GitCommandException : Exception
	{
		public GitCommandException ()
		{
		}

		public GitCommandException (String message) : base(message)
		{
		}

		public GitCommandException (String message, Exception inner) : base(message, inner)
		{
		}
	}
}

