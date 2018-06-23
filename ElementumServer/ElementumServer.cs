using ExitGames.Logging;
using Photon.SocketServer;
using System.Collections.Generic;
using System.IO;

namespace Elementum {
	public class ElementumServer : ApplicationBase {
		private static readonly ILogger log = LogManager.GetCurrentClassLogger();

		private List<Peer> peers;

		private void InitLog() {
			Log.Init(
				Path.Combine(this.BinaryPath, "log4net.config"),
				Path.Combine(this.ApplicationRootPath, "log")			
			);
		}

		protected override PeerBase CreatePeer(InitRequest initRequest) {
			Peer newPeer = new Peer(initRequest);
			this.peers.Add(newPeer);

			return newPeer;
		}

		protected override void Setup() {
			this.InitLog();
			peers = new List<Peer>();
		}

		protected override void TearDown() {

		}
	}
}
