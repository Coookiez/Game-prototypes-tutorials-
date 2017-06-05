using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Transform playerSpawnPoints;
	public GameObject landingAreaPrefab;
	// The parent of the spawn points

	private bool reSpawn = false;
	private Transform[] spawnPoints;
	private bool lastToggle = false;
	private AudioSource innerVoice;

	// Use this for initialization
	void Start () {
		spawnPoints = playerSpawnPoints.GetComponentsInChildren<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (lastToggle != reSpawn) {
			Respawn ();
			reSpawn = false;
		} else {
			lastToggle = reSpawn;
		}
	}

	private void Respawn () {
		int i = Random.Range (1, spawnPoints.Length);
		transform.position = spawnPoints[i].transform.position;
	}

	void OnFindClearArea () {
		Invoke ("DropFlare", 3f);
	}

	void DropFlare() {
		Instantiate (landingAreaPrefab, transform.position, transform.rotation);
		// Drop a flare
	}
}
