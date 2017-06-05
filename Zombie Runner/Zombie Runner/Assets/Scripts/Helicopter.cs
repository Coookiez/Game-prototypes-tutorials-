using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {

	private bool called = false;
	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent <Rigidbody> ();
	}

	void OnDispatchHelicopter () {
		if (!called) {
			Debug.Log ("Helicopter called");
			rigidBody.velocity = new Vector3 (0, 0, 1500f);
			called = true;
		}
	}
}
