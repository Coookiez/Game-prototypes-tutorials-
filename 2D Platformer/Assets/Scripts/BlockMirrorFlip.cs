using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMirrorFlip : MonoBehaviour {
    public GameObject player;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        } else {
            Vector3 tempTransform = player.transform.position;
            tempTransform.y += 1.47f;
            this.transform.position = tempTransform;
        }
    }
}
