using System;

namespace RepoSync.Service.Config
{
	public class Entry
	{
		public virtual String Name {
			get;
			set;
		}

		public virtual String Source {
			get;
			set;
		}

		public virtual String Destination {
			get;
			set;
		}

	}
}

