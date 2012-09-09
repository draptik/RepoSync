using System;
using System.Collections.Generic;

namespace RepoSync.Service.Config
{
	public class SyncConfig
	{
		public SyncConfig ()
		{
		}

		public String Name {
			get;
			set;
		}

		public IList<Entry> Entries {
			get;
			set;
		}
	}
}

