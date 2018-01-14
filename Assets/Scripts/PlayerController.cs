using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed = 5f;
    float moveSpeed;

    public bool grounded = false;
    float g = -9.81f;
    float verticalSpeed;
    public float jumpPower;

    public bool blocking;

    CharacterController cc;

	// Use this for initialization
	void Start () {
        cc = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        // Player input
        float h = Input.GetAxis("Horizontal");
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool block = Input.GetKey(KeyCode.LeftShift);

        RaycastHit hit;
        if(Physics.SphereCast(transform.position, 0.15f, Vector3.down, out hit, 0.45f))
        {
            grounded = true;
        } else
        {
            grounded = false;
        }

        verticalSpeed += g * Time.deltaTime;
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
            }
            else
            {
                moveSpeed = walkSpeed;
                blocking = false;
            }
        }

        cc.Move(transform.right * h * moveSpeed * Time.deltaTime);
        cc.Move(transform.up * verticalSpeed * Time.deltaTime);
    }
}
