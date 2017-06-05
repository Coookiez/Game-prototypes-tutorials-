using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMovement : MonoBehaviour {

	public float moveSpeed;
	private Vector2 minWalkPoint;
	private Vector2 maxWalkPoint;
	private Rigidbody2D myRigidbody;
	public bool isWalking;
	public float walkTime;
	public float waitTime;
	private float walkCounter;
	private float waitCounter;
	public Collider2D walkZone;
	private int walkDirection;
	private bool hasWalkZone = false;
    [Header("Stop when talking")]
    public bool canMove;
    private DialogueManager theDM;
	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
        theDM =FindObjectOfType<DialogueManager>();
        waitCounter = waitTime;
		walkCounter = walkTime;

		ChooseDirection ();
		if (walkZone != null) {
			minWalkPoint = walkZone.bounds.min;
			maxWalkPoint = walkZone.bounds.max;
			hasWalkZone = true;
		} else {
			Debug.LogError ("walkZone equals null! Probably not assigned!");
		}			
	}
	
	// Update is called once per frame
	void Update () {
        if(!theDM.dialogueActive) {
            canMove = true;
        }
        if (!canMove) {
            myRigidbody.velocity = Vector2.zero;
            return;
        }
		if (isWalking) {
			walkCounter -= Time.deltaTime;
			if (walkCounter < 0) {
				isWalking = false;
				waitCounter = waitTime;
			}
			switch (walkDirection) {
				case 0:
					{
						myRigidbody.velocity = new Vector2 (0, moveSpeed);
						if (hasWalkZone && transform.position.y > maxWalkPoint.y ) {
							isWalking = false;
							waitCounter = waitTime;
						}
						break;
					}
				case 1:
					{
						myRigidbody.velocity = new Vector2 (moveSpeed, 0);
						if (hasWalkZone && transform.position.x > maxWalkPoint.x ) {
							isWalking = false;
							waitCounter = waitTime;
						}
						break;
					}
				case 2:
					{
						myRigidbody.velocity = new Vector2 (0, -moveSpeed);
						if (hasWalkZone && transform.position.y < minWalkPoint.y ) {
							isWalking = false;
							waitCounter = waitTime;
						}
						break;
					}
				case 3:
					{
						myRigidbody.velocity = new Vector2 (-moveSpeed, 0);
						if (hasWalkZone && transform.position.x < minWalkPoint.x ) {
							isWalking = false;
							waitCounter = waitTime;
						}
						break;
					}
            }
        } else {
			waitCounter -= Time.deltaTime;
			myRigidbody.velocity = Vector2.zero;
			if (waitCounter < 0) {
				ChooseDirection ();
				isWalking = true;
			}
		}
	}

	public void ChooseDirection () {
		walkDirection = Random.Range (0, 4);
		isWalking = true;
		walkCounter = walkTime;
	}
}
