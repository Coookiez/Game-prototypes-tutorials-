using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour {

    [Header("Volume Settings")]
    private AudioSource theAudioSource;
    public float defaultAudio;

	// Use this for initialization
	void Start () {
        theAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAudioLevel(float volume) {
        if (theAudioSource == null) {
            theAudioSource = GetComponent<AudioSource>();
        }
        theAudioSource.volume = defaultAudio * volume;
    }
}
