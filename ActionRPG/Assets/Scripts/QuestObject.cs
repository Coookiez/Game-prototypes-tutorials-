using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour {

    public int questNumber;
    public QuestManager theQM;
    public string startText;
    public string endText;
    public bool isItemQuest;
    public bool isEnemyQuest;
    public string targetItem;
    public string targetEnemy;
    public int enemiesToKill;
    private int enemyKillCounter;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (isItemQuest) {
            if (isItemQuest) {
                if (theQM.itemCollected == targetItem) {
                    theQM.itemCollected = null;
                    EndQuest();
                }
            }
        } else if (isEnemyQuest) {
            if (theQM.enemyKilled == targetEnemy) {
                theQM.enemyKilled = null;
                enemyKillCounter++;
            }
            if (enemyKillCounter >= enemiesToKill) {
                EndQuest();
            }
        }

    }

    public void StartQuest() {
        theQM.ShowQuestText(startText);
    }

    public void EndQuest() {
        theQM.ShowQuestText(endText);
        theQM.questsCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}
