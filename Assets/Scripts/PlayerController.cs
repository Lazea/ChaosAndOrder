using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed = 5f;
    float moveSpeed;

    public bool grounded = false;
    float g = -9.81f;
    float verticalSpeed;
    public float terminalFallSpeed;

    public float jumpPower;
    bool jumping = false;

    public bool blocking;

    Transform cameratarget;
    float cameratargetHeight;

    CharacterController cc;

	// Use this for initialization
	void Start () {
        cameratarget = transform.Find("CameraTarget");

        cc = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        // Player input
        float h = Input.GetAxis("Horizontal");
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool block = Input.GetKey(KeyCode.LeftShift);

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
        if(Physics.SphereCast(transform.position, 0.14f, Vector3.down, out hit, 0.4f))
        {
            grounded = true;
            jumping = false;
        } else
        {
            if (grounded && !jumping)
            {
                verticalSpeed = 0f;
                jumping = false;
            }

            grounded = false;
        }

        // Fall speed
        verticalSpeed += g * Time.deltaTime;
        if (verticalSpeed < terminalFallSpeed)
        {
            verticalSpeed = terminalFallSpeed;
        }

        // Character movement and control flow
        if (grounded)
        {
            if (block)
            {
                moveSpeed = 0;
                blocking = true;
            }
            else if (jump)
            {
                verticalSpeed = jumpPower;

                grounded = false;
                jumping = true;
            }
            else
            {
                moveSpeed = walkSpeed;
                blocking = false;
            }
        }

        // Apply movement
        cc.Move(transform.right * h * moveSpeed * Time.deltaTime);
        cc.Move(transform.up * verticalSpeed * Time.deltaTime);
    }
}
