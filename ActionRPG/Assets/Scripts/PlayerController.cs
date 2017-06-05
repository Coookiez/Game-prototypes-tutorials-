using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private float currentMoveSpeed;
    //public float diagonalMoveModifier;
    private Animator animator;
    private Rigidbody2D myRigidbody;
    public Vector2 lastMove;
    private Vector2 moveInput;
    public float attackTime;
    public string startPoint;
    private bool playerMoving;
    private bool attacking;
    private float attackTimeCounter;
    private static bool playerExists;
    [Header("Move while talking")]
    public bool canMove;
    [Header("Sound manager")]
    private SFXManager sfxMan;
    // Use this for initialization
    void Start() {
        canMove = true;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        if (!playerExists) {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else {
            Destroy(gameObject);
        }
        lastMove = new Vector2(0, -1f);
        sfxMan = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update() {
        playerMoving = false;
        if (!canMove) {
            myRigidbody.velocity = Vector2.zero;
            return;
        }
        if (!attacking) {
            //if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f) {
            //    //transform.Translate (new Vector3 (Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            //    myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentMoveSpeed, myRigidbody.velocity.y);
            //    playerMoving = true;
            //    lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            //}
            //if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f) {
            //    //transform.Translate (new Vector3 (0f, Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime, 0f)); 
            //    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * currentMoveSpeed);
            //    playerMoving = true;
            //    lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            //}
            //if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f) {
            //    myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            //}
            //if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f) {
            //    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
            //}

            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if (moveInput != Vector2.zero) {
                myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
                playerMoving = true;
                lastMove = moveInput;
            } else {
                myRigidbody.velocity = Vector2.zero;
            }

            if (Input.GetKeyDown(KeyCode.J)) {
                attackTimeCounter = attackTime;
                attacking = true;
                myRigidbody.velocity = Vector2.zero;
                animator.SetBool("Attack", true);

                sfxMan.playerAttack.Play();
            }


            //if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f) {
            //    currentMoveSpeed = moveSpeed * diagonalMoveModifier;
            //} else {
            //    currentMoveSpeed = moveSpeed * diagonalMoveModifier;
            //}

        }
        if (attackTimeCounter > 0) {
            attackTimeCounter -= Time.deltaTime;
        } else if (attackTimeCounter <= 0) {
            attacking = false;
            animator.SetBool("Attack", false);
        }
        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        Debug.Log("Horizontal: " + Input.GetAxisRaw("Horizontal"));
        Debug.Log("Vertical: " + Input.GetAxisRaw("Vertical"));
        animator.SetBool("PlayerMoving", playerMoving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
    }
}
