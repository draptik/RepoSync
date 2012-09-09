using System;

namespace RepoSync.Service
{
	public class CommandRequest : ICommandRequest
	{
		public CommandRequest ()
		{
		}


		public String Name {
			get;
			set;
		}

		public String Arguments {
			get;
			set;
		}

		public String WorkingDirectory {
			get;
			set;
		}
	}
}

