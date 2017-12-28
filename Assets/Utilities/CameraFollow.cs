using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Rigidbody2D targetBody;
    public float lookAheadDistance = 3f;
    public float smoothness = 0.1f;
    public float currLookAhead;

	// Use this for initialization
	void Start () {
        currLookAhead = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        if (targetBody.velocity.magnitude > 0.1)  {
            currLookAhead = Mathf.Lerp(currLookAhead, lookAheadDistance, 0.1f);
        }
        else {
            currLookAhead = Mathf.Lerp(currLookAhead, 0f, 0.05f);
        }
        //Vector3 velocityComp = new Vector3(target.forward.x, target.forward.y).normalized * currLookAhead;
        Vector3 velocityComp = new Vector3(targetBody.velocity.x, targetBody.velocity.y).normalized * currLookAhead;
        transform.position = Vector3.Lerp(transform.position, desiredPosition + velocityComp, 0.1f);
    }
}
