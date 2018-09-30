using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Declaring Variables
    [HideInInspector]
    public bool facingDown = true;
    public bool facingRight = true;

    private Animator animator;
    private Rigidbody2D rb;

    public float runForce = 365f;
    public float maxSpeed = 5f;

    Transform placeholderTransform;

    public LevelManagement LevelManager;

    //Enemy List
    public static List<Enemy> enemies;

    // Use this for initialization
    void Awake () {
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //Adds itself to enemies list
        if (enemies == null)
            enemies = new List<Enemy>();
        enemies.Add(this);

        //Gets LevelManager
        LevelManager = GameObject.FindObjectOfType<LevelManagement>();

        //Flips Enemy
        if (!facingRight)
        {
            Vector3 rbLocalScaleX = GetComponent<Transform>().localScale;
            rbLocalScaleX.x *= -1;
            GetComponent<Transform>().localScale = rbLocalScaleX;
        }
    }
	
	// Update is called once per frame
	void Update () {
        rb.gravityScale = PlatformerController.GravityModifier;
        if (rb.velocity.y != 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    // Called When GameObject is Disabled
    void OnDisable()
    {
        enemies.Remove(this);
        LevelManager.isEnemiesEmpty();
    }

    // Fixed Update is Update but accounts for frame rate
    void FixedUpdate ()
    {
        if (PlatformerController.GravityModifier > 0 && rb.transform.localScale.y == -1)
        {
            FlipVertical();
        }

        else if (PlatformerController.GravityModifier < 0 && rb.transform.localScale.y == 1)
        {
            FlipVertical();
        }

    }

    void FlipVertical()
    {
        facingDown = !facingDown;

        Vector3 rbLocalScaleY = GetComponent<Transform>().localScale;
        rbLocalScaleY.y *= -1;
        GetComponent<Transform>().localScale = rbLocalScaleY;
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 rbLocalScaleX = GetComponent<Transform>().localScale;
        rbLocalScaleX.x *= -1;
        GetComponent<Transform>().localScale = rbLocalScaleX;
    }
}