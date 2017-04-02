using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour {

    AudioManager audioManager;

    [SerializeField]
    string mouseHoverSound = "ButtonHover";
    [SerializeField]
    string buttonPressSound = "ButtonPress";

    private void Start() {
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("no audioManager in Player script");
        }
    }
    public void Quit() {
        Debug.Log("App quit!");
        Application.Quit();
    }
    
    public void Retry () {
        audioManager.PlaySound(buttonPressSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnMouseOver() {
        audioManager.PlaySound(mouseHoverSound);
    }
}
