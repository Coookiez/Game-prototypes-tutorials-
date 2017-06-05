using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour {

    public int questNumber;
    private QuestManager theQM;
    public string itemName;
    public CircleCollider2D circleCol2D;
	// Use this for initialization
	void Start () {
        theQM = FindObjectOfType<QuestManager>();
        circleCol2D = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (theQM.quests[questNumber].gameObject.activeSelf) {
            this.circleCol2D.isTrigger = true;
            Debug.Log("T isTrigger = " + this.circleCol2D.isTrigger);
        } else if (theQM.questsCompleted[questNumber] == true) {
            this.circleCol2D.isTrigger = false;
            Debug.Log("F isTrigger = " + this.circleCol2D.isTrigger);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            if(!theQM.questsCompleted[questNumber] && theQM.quests[questNumber].gameObject.activeSelf) {
                theQM.itemCollected = itemName;
                gameObject.SetActive(false);
            }
        }
    }
}
