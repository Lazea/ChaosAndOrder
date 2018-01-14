using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Transform target;

    public float followRate;
    Vector3 refVel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, target.position, ref refVel, followRate);
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
