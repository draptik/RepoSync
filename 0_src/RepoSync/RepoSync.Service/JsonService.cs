using System;
using System.IO;
using RepoSync.Service.Config;
using Newtonsoft.Json;

namespace RepoSync.Service
{
	public class JsonService
	{
		private readonly IoService ioService;

		private SyncConfig config;

		public JsonService (IoService ioService)
		{
			this.ioService = ioService;
			this.config = new SyncConfig();
		}

		public void Init (string fileName)
		{
			if (!this.ioService.FileIsValid(fileName)) {
				return;
			}

			StreamReader reader = new StreamReader(fileName);
			string s = reader.ReadToEnd();
			reader.Close();
			this.config = JsonConvert.DeserializeObject<SyncConfig>(s);
 		}

		public SyncConfig SyncConfig { get { return this.config; } }
	}
}

