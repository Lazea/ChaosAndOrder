using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeControl : MonoBehaviour {

    public float hSensitivity = 20f;
    public float vSensitivity = 20f;

    public bool xRotation = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(xRotation)
        {
            transform.eulerAngles += Vector3.up * h * hSensitivity * Time.deltaTime;
        } else
        {
            transform.eulerAngles += Vector3.forward * -v * vSensitivity * Time.deltaTime;
        }
    }
}
