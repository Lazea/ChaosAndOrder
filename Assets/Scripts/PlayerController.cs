using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Movement
    public float walkSpeed = 5f;
    float moveSpeed;

    // Grounded checks
    bool grounded = false;
    float g = -9f;
    float verticalSpeed;
    float terminalFallSpeed = -6f;

    // Jumping
    public float jumpPower;
    bool jumping = false;

    // Blocking
    bool blocking;
    public bool dash;
    public bool dashReady = true;

    // Player stats
    public int maxHealth = 100;
    int health;
    bool hit;

    // Components and transforms
    Transform cameratarget;
    float cameratargetHeight;

    Animator animator;
    CharacterController cc;



    public bool bwalk;
    public bool bjump;
    public bool bfall;
    public bool bswitch;

    // Use this for initialization
    void Start () {
        health = maxHealth;

        cameratarget = transform.Find("CameraTarget");

        animator = transform.Find("PlayerSprites").gameObject.GetComponent<Animator>();
        cc = gameObject.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        // Player input
        float h = Input.GetAxis("Horizontal");
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool block = Input.GetKey(KeyCode.LeftShift);
        bool weaponSwitch = Input.GetKeyDown(KeyCode.Q);

        // Flip Character in direction of motion
        Vector3 playerScale = transform.localScale;
        if(h > 0)
        {
            playerScale.x = 1;
        } else if(h < 0)
        {
            playerScale.x = -1;
        }

        transform.localScale = playerScale;

        // Ground check
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, 0.14f, Vector3.down, out hit, 0.2f))
        {
            grounded = true;
        } else
        {
            if (grounded)
            {
                verticalSpeed = 0f;
            }

            grounded = false;
        }

        // Fall speed
        verticalSpeed += g * Time.deltaTime;
        if (verticalSpeed < terminalFallSpeed)
        {
            verticalSpeed = terminalFallSpeed;
        }

        animator.SetBool("Dash", false);
        // Character movement and control flow
        if (grounded)
        {

            jumping = false;
            if (block)
            {
                moveSpeed = 0;
                blocking = true;

                // Dash logic
            }
            else if (jump)
            {
                verticalSpeed = jumpPower;

                grounded = false;
                jumping = true;
            }
            else
            {
                jumping = false;
                moveSpeed = walkSpeed;
                blocking = false;
            }
        }

        // Apply movement
        cc.Move(transform.right * h * moveSpeed * Time.deltaTime);
        cc.Move(transform.up * verticalSpeed * Time.deltaTime);

        // Animation update
        if (Mathf.Abs(h) > 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        animator.SetBool("Fall", !grounded);
        animator.SetBool("Jump", jumping);
        animator.SetBool("Block", blocking);
        animator.SetBool("Switch", weaponSwitch);


        bwalk = animator.GetBool("Walk");
        bjump = animator.GetBool("Jump");
        bfall = animator.GetBool("Fall");
        bswitch = animator.GetBool("Switch");
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Death();
        }

        hit = true;
        animator.SetBool("Hit", true);
    }

    void Heal(int healthPoints)
    {
        health += healthPoints;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        animator.SetInteger("Health", health);
    }

    void Death()
    {
        // Display game over screen
    }

    public int GetHealth()
    {
        return health;

        animator.SetInteger("Health", health);
    }
}
