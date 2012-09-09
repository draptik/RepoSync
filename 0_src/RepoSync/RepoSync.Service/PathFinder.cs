using System;

namespace RepoSync.Service
{
	public class PathFinder
	{
		public string UserHomePath {
			get 
			{
				string result = string.Empty;

				result = (Environment.OSVersion.Platform == PlatformID.Unix || 
				Environment.OSVersion.Platform == PlatformID.MacOSX) 
					? Environment.GetEnvironmentVariable ("HOME")
    				: Environment.ExpandEnvironmentVariables ("%HOMEDRIVE%%HOMEPATH%");

				return result;
			}
		}
	}
}

