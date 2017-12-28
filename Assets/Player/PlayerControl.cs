using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public enum MovementState
    {
        standard,
        dashing,
        ccd // crowd control
    }


    public float speed = 5f;
    public float dashSpeed = 10f;
    public float dashCooldown = 2f;
    public float dashDuration = 0.5f;
    float lastDash;
    Rigidbody2D body;
    Vector3 lookDirection;
    public TurretBasic turret;
    ParticleSystem.EmissionModule ghostTrail;
    MovementState state;
    Vector2 dashControlVector;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        ghostTrail = GetComponent<ParticleSystem>().emission;
        lastDash = 0f;
        ghostTrail.enabled = false;
        state = MovementState.standard;
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

        /* handle dashing */
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // player wants to dash
            if (state == MovementState.standard && (Time.time - lastDash) > dashCooldown)
            {
                // start a dash
                state = MovementState.dashing;
                lastDash = Time.time;
                dashControlVector = controlVector;
                ghostTrail.enabled = true;
            }
        }
        // if we are dashing, check to see if we are done
        if (state == MovementState.dashing && (Time.time - lastDash) > dashDuration)
        {
            // stop dashing
            state = MovementState.standard;
            ghostTrail.enabled = false;
        }

        /* normalize and apply it to the velocity */
        Vector2 clampedControl;
        if (state == MovementState.standard) {
            clampedControl = controlVector.normalized * speed;
        } else if (state == MovementState.dashing) {
            clampedControl = dashControlVector * dashSpeed;
        } else {
            clampedControl = Vector2.zero;
        }
        body.velocity = clampedControl;

        /* rotate the transform to match the motion */
        if (controlVector.magnitude > 0.05) {
            // only update rotation if there is input
            lookDirection = new Vector3(controlVector.x, controlVector.y, 0);
        }
        //transform.up = Vector3.Lerp(transform.up, lookDirection, 0.5f);
        transform.up = lookDirection;

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
