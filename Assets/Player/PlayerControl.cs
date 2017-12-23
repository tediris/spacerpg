using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {


    public float speed = 5f;
    Rigidbody2D body;
    Vector3 lookDirection;
    public TurretBasic turret;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
	}

    float getHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    float getVerticallInput()
    {
        return Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate () {
        /* get the desired movement vector */
        float horizontalInput = getHorizontalInput();
        float verticalInput = getVerticallInput();
        Vector2 controlVector = new Vector2(horizontalInput, verticalInput);

        /* normalize and apply it to the velocity */
        Vector2 clampedControl = controlVector.normalized * speed;
        body.velocity = clampedControl;

        /* rotate the transform to match the motion */
        if (controlVector.magnitude > 0.1) {
            // only update rotation if their is input
            lookDirection = new Vector3(controlVector.x, controlVector.y, 0);
        }
        transform.up = Vector3.Lerp(transform.up, lookDirection, 0.3f);

        /* handle moving the turret */
        Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimVector = mouseLocation - transform.position;
        turret.lookDirection = aimVector;

        /* handle shooting */
        if (Input.GetButton("Fire1")) {
            turret.Fire();
        }
    }
}
