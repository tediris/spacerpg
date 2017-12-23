using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBasic : MonoBehaviour {

    public Transform target;
    public Vector3 lookDirection;
    public float radiusSize = 2f;
    public GameObject bullet;
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;
    Vector3 aim;
    float lastFire;

	// Use this for initialization
	void Start () {
        lastFire = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        aim = lookDirection.normalized;
        aim.z = 0;
        aim = aim.normalized;

        /* set the position */
        transform.position = target.position + aim * radiusSize;

        /* set the rotation */
        transform.up = aim;
	}

    public void Fire()
    {
        if (Time.time - lastFire < fireRate) {
            // we are still on cooldown
            return;
        }
        lastFire = Time.time;
        GameObject newProjectile = GameObject.Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D projBody = newProjectile.GetComponent<Rigidbody2D>();
        projBody.velocity = aim * bulletSpeed;
    }
}
