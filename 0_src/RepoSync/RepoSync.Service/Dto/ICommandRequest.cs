using System;

namespace RepoSync.Service
{
	public interface ICommandRequest
	{
		String Name {
			get;
			set;
		}

		String Arguments {
			get;
			set;
		}

		String WorkingDirectory {
			get;
			set;
		}
	}
}

