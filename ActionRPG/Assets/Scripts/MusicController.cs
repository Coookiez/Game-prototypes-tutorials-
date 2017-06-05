using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    [Header("Playing tracks")]
    public static bool mcExists;
    public AudioSource[] musicTracks;
    public int currentTrack;
    public bool musicCanPlay;

    // Use this for initialization
    void Start() {
        if (!mcExists) {
            mcExists = true;
            DontDestroyOnLoad(transform.root.gameObject);
            DontDestroyOnLoad(transform.gameObject);
        } else {
            Debug.Log("DESTROY MUSIC CONTROLLER");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        if (musicCanPlay) {
            if (!musicTracks[currentTrack].isPlaying) {
                Debug.Log("Nowy play");
                musicTracks[currentTrack].Play();
            }
        } else {
            Debug.Log("Nowy stop");
            musicTracks[currentTrack].Stop();
        }
        Debug.Log("musicTracks[currentTrack].isPlaying: " + musicTracks[currentTrack].isPlaying);
    }
    
    public void SwitchTrack(int newTrack) {
        musicTracks[currentTrack].Stop();
        currentTrack = newTrack;
        musicTracks[currentTrack].Play();
    }
}
