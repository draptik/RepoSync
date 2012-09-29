using System;

namespace RepoSync.Service.Config
{
	public class Entry
	{
		public Entry ()
		{
			Id = System.Guid.NewGuid();
		}

		public virtual Guid Id { get; private set; }

		public virtual String Name {
			get;
			set;
		}

		public virtual String Local {
			get;
			set;
		}

		public virtual String Remote {
			get;
			set;
		}

		public virtual DefaultGitAction DefaultGitAction {
			get;
			set;
		}
	}
}

