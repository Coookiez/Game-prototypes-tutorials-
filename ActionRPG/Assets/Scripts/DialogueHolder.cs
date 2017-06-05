using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour {

    [Header("Dialogue manager")]
	public string dialogue;
	private DialogueManager dManager;

    public string[] dialogueLines;

	// Use this for initialization
	void Start () {
		dManager = FindObjectOfType<DialogueManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.name == "Player") {
			if (Input.GetKeyUp (KeyCode.Space)) {
				//dManager.ShowBox (dialogue);
                if(!dManager.dialogueActive) {
                    dManager.dialogueLines = dialogueLines;
                    dManager.currentLine = 0;
                    dManager.ShowDialogue();
                }
                if(GetComponentInParent<VillagerMovement>() != null) {
                    GetComponentInParent<VillagerMovement>().canMove = false;
                }
                Debug.Log("holder");
            }
        }
	}
}
