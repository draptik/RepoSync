using System;
using System.IO;

namespace RepoSync.Service
{
	public class IoService
	{
		public IoService ()
		{
		}

		public bool DirectoryIsValid (String directory)
		{
			bool result = false;

			if (!String.IsNullOrEmpty(directory)) {
				var directoryInfo = new DirectoryInfo (directory);
				if (directoryInfo.Exists) {
					result = true;
				}
			}	return result;
		}		

		public bool FileIsValid (string fileName)
		{
			bool result = false;

			if (!String.IsNullOrEmpty(fileName)) {
				var fileInfo = new FileInfo (fileName);
				if (fileInfo.Exists) {
					result = true;
				}
			}	return result;
		}
	}
}

