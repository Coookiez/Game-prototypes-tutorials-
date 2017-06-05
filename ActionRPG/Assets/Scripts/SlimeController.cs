using UnityEngine;

public class SlimeController : MonoBehaviour {

    public float moveSpeed;
    public float timeToMove;
    public float timeBetweenMove;
    public float waitToReload;

    private Rigidbody2D myRigidbody;
    private bool moving;
    private float timeBetweenMoveCounter;
    private float timeToMoveCounter;
    private Vector3 moveDirection;
    private bool reloading;
    private GameObject thePlayer;
	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        //timeBetweenMoveCounter = timeBetweenMove;
        //timeToMoveCounter = timeToMove;
        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeBetweenMove * 1.25f);
	}
	
	// Update is called once per frame
	void Update () {
	    if (moving) {
            timeToMoveCounter -= Time.deltaTime;
            myRigidbody.velocity = moveDirection;
            if(timeToMoveCounter < 0f) {
                moving = false;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
                // timeBetweenMoveCounter = timeBetweenMove;          
            }
        } else {
            timeBetweenMoveCounter -= Time.deltaTime;
            myRigidbody.velocity = Vector2.zero;
            if (timeBetweenMoveCounter < 0f) {
                moving = true;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeBetweenMove * 1.25f);
                //timeToMoveCounter = timeToMove;
                moveDirection = new Vector3(Random.Range(-1f,1f) * moveSpeed,Random.Range(-1f,1f),0f) * moveSpeed;
            }
        }
        if(reloading) {
            waitToReload -= Time.deltaTime;
            if (waitToReload < 0) {
                Application.LoadLevel(Application.loadedLevel);
                thePlayer.SetActive(true);
            }
        }
	}
}
