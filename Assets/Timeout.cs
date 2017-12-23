using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeout : MonoBehaviour {

    float timeCreated;
    public float timeTolive = 3f;

	// Use this for initialization
	void Start () {
        timeCreated = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.time - timeCreated > timeTolive)
        {
            Destroy(this.gameObject);
        }
	}
}
