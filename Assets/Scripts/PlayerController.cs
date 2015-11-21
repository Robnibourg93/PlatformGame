using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : Entity {

    //Player Handling
    public float gravity = 20;
    public float walkSpeed = 8;
    public float runSpeed = 12;
    public float acceleration = 40;
    public float jumpHeight = 12;
    public float slidingDeceleration = 10;

    private float initiateSlideThreshold = 9;
    
    //System
    private float animationSpeed;
    private float currentSpeed;
    private float targetSpeed;
    private Vector2 amountToMove;
    private float moveDirX;

    //States
    private bool jumping;
    private bool sliding;
    private bool wallHolding;
    private bool stopSliding;

    //Components
    private Animator animator;
    private PlayerPhysics playerPhycis;
    private GameManager manager;

	// Use this for initialization
	void Start () {
        playerPhycis = GetComponent<PlayerPhysics>();
        animator = GetComponent<Animator>();
        manager = Camera.main.GetComponent<GameManager>();
        animator.SetLayerWeight(1,1);
	}

    // Update is called once per frame
    void Update() {

        if (this.transform.position.y < -20f)
        {
            TakeDamage(10);
        }

        if (playerPhycis.movementStopped) {
            targetSpeed = 0;
            currentSpeed = 0;
        }

        // If player is touching the ground
        if (playerPhycis.grounded)
        {
            amountToMove.y = 0;

            if (wallHolding) {
                wallHolding = false;
                animator.SetBool("Wall Hold", false);
            }

            if (jumping)
            {
                jumping = false;
                animator.SetBool("Jumping", false);
            }

            if (sliding)
            {
                if (Mathf.Abs(currentSpeed) < .25f || stopSliding)
                {
                    stopSliding = false;
                    sliding = false;
                    animator.SetBool("Sliding", false);

                    playerPhycis.ResetCollider();
                }
            }

            //Slide input
            if (Input.GetButtonDown("Slide"))
            {

                if (Mathf.Abs(currentSpeed) > initiateSlideThreshold)
                {
                    sliding = true;
                    animator.SetBool("Sliding", true);
                    targetSpeed = 0;

                    playerPhycis.SetCollider(new Vector3(10.3f, 1.5f, 3), new Vector3(0.35f, 0.75f, 0));
                }
            }
        }
        else {
            if (!wallHolding) {
                if (playerPhycis.canWallHold) {
                    wallHolding = true;
                    animator.SetBool("Wall Hold", true);
                }
            }
        }
        //Set animator parameters
        animationSpeed = incrementTowards(animationSpeed, Mathf.Abs(targetSpeed), acceleration);
        animator.SetFloat("Speed", animationSpeed);


        //input
        moveDirX = Input.GetAxisRaw("Horizontal");
        if (!sliding)
        {
            float speed = (Input.GetButton("Run")) ? runSpeed : walkSpeed;
            targetSpeed = moveDirX * speed;
            currentSpeed = incrementTowards(currentSpeed, targetSpeed, acceleration);

            //Face direction
            if (moveDirX != 0 && !wallHolding)
            {
                transform.eulerAngles = (moveDirX > 0) ? Vector3.up * 180 : Vector3.zero;
            }

        }
        else {
            currentSpeed = incrementTowards(currentSpeed, targetSpeed, slidingDeceleration);
        }
        //Jump Input
        if (Input.GetButtonDown("Jump"))
        {
            if (sliding) {
                stopSliding = true;

            }

            if (playerPhycis.grounded || wallHolding) {
                amountToMove.y = jumpHeight;
                jumping = true;
                animator.SetBool("Jumping", true);

                if (wallHolding) {
                    wallHolding = false;
                    animator.SetBool("Wall Hold",false);
                }
            }
        }
        //set amount to move
        amountToMove.x = currentSpeed;
        if (wallHolding) {
            amountToMove.x = 0;
            if (Input.GetAxisRaw("Vertical") != -1) {
                amountToMove.y = 0;
            }
        }
        amountToMove.y -= gravity * Time.deltaTime;
        playerPhycis.Move(amountToMove * Time.deltaTime, moveDirX);

    }

    void OnTriggerEnter(Collider c) {
        if (c.tag == "Checkpoint") {
            manager.SetCheckpoint(c.transform.position);
        }
        if (c.tag == "Finish") {
            manager.EndLevel();
        }
    }

    private float incrementTowards(float n, float target, float a) {

        if (n == target)
        {
            return n;
        }
        else {
            float dir = Mathf.Sign(target - n);
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }
}
