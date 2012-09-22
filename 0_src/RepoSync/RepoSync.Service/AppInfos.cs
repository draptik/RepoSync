using System;

namespace RepoSync.Service
{
	public static class AppInfos
	{
		private const string currentVersion = "v0.9";

		public static string Version 
		{
			get { return currentVersion; }
		}

		public static string AppName 
		{
			get { return "RepoSync"; }
		}

		public static string WebSite 
		{
			get { return "https://github.com/draptik/RepoSync"; }
		}

		public static string Comments 
		{
			get { return "Simple Git Repo Sync Tool"; }
		}
	}
}

