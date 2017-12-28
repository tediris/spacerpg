using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour {

    public float damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter2D(Collision2D collision) {
        /* get the other object */
        GameObject other = collision.collider.gameObject;
        // check if the object has a status
        Status status;
        if ((status = other.GetComponent<Status>()) != null) {
            // check if it is vulnerable
            if (status.state != Status.State.invulnerable) {
                /* apply damage */
                other.GetComponent<Health>().Damage(damage);
                /* apply any status effects */
            }
        }
        /* handle any other cases here */

        /* destroy the projectile? TODO: be smarter about this */
        Destroy(this.gameObject);

    }
}
