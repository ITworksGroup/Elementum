using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace Elementum {
	public class Peer : ClientPeer {

		public Peer(InitRequest initRequest):base(initRequest) {
			Log.Instance.DebugFormat("New peer. " + initRequest.InitObject);
		}

		protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail) {
			if (Log.Instance.IsDebugEnabled) {
				Log.Instance.DebugFormat("OnDisconnect. ReasonCode: {0}", reasonCode);
			}
		}

		protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters) {
			if (Log.Instance.IsDebugEnabled) {
				Log.Instance.DebugFormat("OnOperationRequest. Code: {0}; Message: {1}", operationRequest.OperationCode, operationRequest.Parameters[1]);
				switch (operationRequest.OperationCode) {
					case 1:
						OperationResponse response = new OperationResponse(1, new Dictionary<byte, object> { { 1, "response 1" } });
						this.SendOperationResponse(response, sendParameters);
						break;
					case 2:
						this.SendEvent(new EventData(1, new Dictionary<byte, object> { { 1, "my event" } }), sendParameters);
						break;
					case 3:
						this.SendMessage("Тест мессаге", sendParameters);
						break;
				}
				
			}
		}
	}
}
