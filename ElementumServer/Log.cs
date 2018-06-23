using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using System;
using System.IO;

namespace Elementum {
	public class Log {
		private static ILogger instance = LogManager.GetCurrentClassLogger();
		
		public static void Init(string configPath = "", string outPath = "") {
			string binaryDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@"file:\", "");
			if (string.IsNullOrWhiteSpace(configPath)) {				
				configPath = Path.Combine(binaryDirectoryPath, "log4net.config");
			}

			if (string.IsNullOrWhiteSpace(outPath)) {
				outPath = Path.Combine(binaryDirectoryPath, "log");
			}

			log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = outPath;

			var file = new FileInfo(configPath);
			if (file.Exists) {
				LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
				XmlConfigurator.ConfigureAndWatch(file);
			}
			else {
				throw new Exception("Log config file doesnt exist! File path: " + file.FullName);
			}
		}

		public static ILogger Instance {
			get { return Log.instance; }
		}
	}
}
