using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour {

    //Flip Gravity Sound
    public AudioClip flipGravitySound;
    private AudioSource playerAudioSource;

    //Declaring Variables
    [HideInInspector] public bool facingDown = true;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool flipGravity = false;

    private bool isRunning = false;

    //Creates Static Gravity Variable
    public static float GravityModifier = 2.5f;

    public float runForce = 365f;
    public float maxSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rb;

	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //Resets Gravity Each Time Scene Is Loaded
        GravityModifier = 2.5f;

        //Gets Audio Sources
        playerAudioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {

        //Universal Jump Input
        if (Input.GetButtonDown("Jump"))
        {
            flipGravity = true;
        }

        //Sets Player Gravity to GravityModifier Static Float
        rb.gravityScale = GravityModifier;

        //Sends Information to Animator
        animator.SetBool("isRunning", isRunning);
        animator.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("speedY", Mathf.Abs(rb.velocity.y));
    }

    // Fixed Update is Update but accounts for frame rate

    void FixedUpdate ()
    {

        //Gets Horizontal Input Axis (A and D Keys or Left and Right Keys, Controller Joysticks, etc...)
        float horizontal = Input.GetAxisRaw("Horizontal");

        //Player Can Only Move Left Or Right If They're Not In Midair
        if (rb.velocity.y == 0)
        {

            if (horizontal != 0)
            {
                //Changes velocity based on Horizontal Input Axis
                rb.velocity = new Vector2(horizontal * maxSpeed, rb.velocity.y);

                isRunning = true;
            } else
            {
                isRunning = false;
            }

        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            isRunning = false;
        }

        //Makes Sure Player Faces Correct Direction
        if (horizontal > 0 && !facingRight)
        {
            Flip();
        } else if (horizontal < 0 && facingRight)
        {
            Flip();
        }

        if (GravityModifier > 0 && rb.transform.localScale.y == -1)
        {
            FlipVertical ();
        }

        else if (GravityModifier < 0 && rb.transform.localScale.y == 1)
        {
            FlipVertical();
        }

        if (flipGravity)
        {
            animator.SetTrigger("flipGravityTrigger");
            GravityModifier *= -1;
            FlipVertical();
            flipGravity = false;

            //Play Audio
            playerAudioSource.clip = flipGravitySound;
            playerAudioSource.Play();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 rbLocalScaleX = GetComponent<Transform>().localScale;
        rbLocalScaleX.x *= -1;
        GetComponent<Transform>().localScale = rbLocalScaleX;
    }

    void FlipVertical()
    {
        facingDown = !facingDown;

        Vector3 rbLocalScaleY = GetComponent<Transform>().localScale;
        rbLocalScaleY.y *= -1;
        GetComponent<Transform>().localScale = rbLocalScaleY;
    }
}