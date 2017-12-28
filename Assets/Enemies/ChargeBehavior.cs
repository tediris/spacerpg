using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBehavior : MonoBehaviour {

    public Transform target;
    public float chargeTime;
    public float chargeSpeed;
    public float chargePastDistance = 3f;
    public float coolDownTime;

    float aimTime;
    float coolTime;

    Vector2 lockPosition;
    Rigidbody2D body;

    public GameObject lockPrefab;

    public enum ChargeState {
        aiming,
        charging,
        cooldown
    }

    public ChargeState state;

	// Use this for initialization
	void Start () {
        state = ChargeState.cooldown;
        coolTime = 0f;
        body = GetComponent<Rigidbody2D>();
	}

    void DrawAimingLock() {
        GameObject lockSprite = GameObject.Instantiate(lockPrefab, target.position, Quaternion.identity);
        lockSprite.GetComponent<AimLock>().Init(target, chargeTime);
    }

    Vector2 Vec2FromVec3(Vector3 vec) {
        return new Vector2(vec.x, vec.y);
    }

    void ChargeAtTarget() {
        Vector2 targetDiff = Vec2FromVec3(target.position) - Vec2FromVec3(transform.position);
        // add chargePastDistance to this vector
        lockPosition = Vec2FromVec3(target.position) + targetDiff.normalized * chargePastDistance;
        body.velocity = new Vector2(targetDiff.x, targetDiff.y).normalized * chargeSpeed;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (state == ChargeState.cooldown) {
            /* if we are down with our CD, then go into the aiming state */
            if (Time.time - coolTime > coolDownTime) {
                aimTime = Time.time;
                DrawAimingLock();
                state = ChargeState.aiming;
            }
        } else if (state == ChargeState.aiming) {
            if (Time.time - aimTime > chargeTime) {
                /* start the charge */
                ChargeAtTarget();
                state = ChargeState.charging;
            }
        } else if (state == ChargeState.charging) {
            Vector2 diffVec = lockPosition - Vec2FromVec3(transform.position);
            float projection = Vector2.Dot(body.velocity, diffVec);
            
            if (projection < 0) {
                /* start a cooldown */
                coolTime = Time.time;
                body.velocity = Vector2.zero;
                state = ChargeState.cooldown;
            }
        }
	}
}
