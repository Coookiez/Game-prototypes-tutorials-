using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {

    public string levelToLoad;
    public string exitPoint;
    public PlayerController thePlayer;
    public Camera theCam;
    void Start() {
        thePlayer = FindObjectOfType<PlayerController>();
        theCam = FindObjectOfType<Camera>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Player") {
            CameraController theCC = theCam.GetComponent<CameraController>();
            if (levelToLoad == "HouseInside") {
                theCam.orthographicSize = 2;
            } else {
                theCam.orthographicSize = 5;
            }
            theCC.AdjustCameraOnLoadScene();
            SceneManager.LoadScene(levelToLoad);
            thePlayer.startPoint = this.exitPoint;
        }
    }
}
