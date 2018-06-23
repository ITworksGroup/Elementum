using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnMouseDown() {
		print("Click!");
		PhotonServer.Instance.SendOperation1();
	}

	private void OnMouseEnter() {
		print("Enter!");
		PhotonServer.Instance.SendOperation3();
	}
}
