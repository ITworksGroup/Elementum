using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonServer : MonoBehaviour, IPhotonPeerListener {

	private const string connectionString = "127.0.0.1:5055";
	private const string appName = "Elementum";

	private static PhotonServer instance;
	public static PhotonServer Instance {
		get { return PhotonServer.instance; }
	}

	private PhotonPeer Peer { get; set; } 

	void Awake() {
		if (PhotonServer.instance != null) {
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);

		Application.runInBackground = true;

		PhotonServer.instance = this;
	}

	public void DebugReturn(DebugLevel level, string message) {
		Debug.LogFormat("Debug return! Level: {0}; Message: {1}", level, message);
	}

	public void OnEvent(EventData eventData) {
		Debug.LogFormat("Event! Code: {0}; Data: {1}", eventData.Code, eventData.Parameters[1]);
	}

	public void OnOperationResponse(OperationResponse operationResponse) {
		switch (operationResponse.OperationCode) {
			case 1: {
					if (operationResponse.Parameters.ContainsKey(1)) {
						Debug.Log("Recieved: " + operationResponse.Parameters[1]);
					}
					break;
				}
			default: {
					Debug.Log("Unknown operation code: " + operationResponse.OperationCode);
					break;
				}
		}
	}

	public void OnStatusChanged(StatusCode statusCode) {
		Debug.Log("Status: " + statusCode);
	}

	private void Connect() {
		if (this.Peer != null) {
			this.Peer.Connect(PhotonServer.connectionString, PhotonServer.appName, "Antonio");
		}
	}
	private void Disconnect() {
		if (this.Peer != null) {
			this.Peer.Disconnect();
		}
	}

	// Use this for initialization
	void Start () {
		this.Peer = new PhotonPeer(this, ConnectionProtocol.Udp);
		this.Connect();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.Peer != null) {
			this.Peer.Service();
		}
	}

	void OnApplicationQuit() {
		this.Disconnect();
	}

	public void SendOperation1() {
		if (this.Peer != null) {
			this.Peer.OpCustom(1, new Dictionary<byte, object> { { 1, "operation 1"} }, true);
		}
	}

	public void SendOperation2() {
		if (this.Peer != null) {
			this.Peer.OpCustom(2, new Dictionary<byte, object> { { 1, "operation 2" } }, true);
		}
	}

	public void SendOperation3() {
		if (this.Peer != null) {
			this.Peer.OpCustom(3, new Dictionary<byte, object> { { 1, "operation 3" } }, true);
		}
	}
}
