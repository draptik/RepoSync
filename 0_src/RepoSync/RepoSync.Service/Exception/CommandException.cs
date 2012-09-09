using System;

namespace RepoSync.Service
{
	public class CommandException : Exception
	{
		public CommandException ()
		{
		}

		public CommandException (String message) : base(message)
		{
		}

		public CommandException (String message, Exception inner) : base(message, inner)
		{
		}
	}
}

