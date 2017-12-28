using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLock : MonoBehaviour {

    public Transform target;
    public float lockTime;
    Vector3 initialScale;
    SpriteRenderer renderer;
    public float fadeTime = 1f;

    float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        initialScale = transform.localScale;
        renderer = GetComponent<SpriteRenderer>();
	}

    public void Init(Transform target, float lockTime) {
        this.target = target;
        this.lockTime = lockTime;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float timeElapsed = Time.time - startTime;
        float fraction = (lockTime - timeElapsed) / lockTime;
        if (timeElapsed < lockTime) {
            /* keep shrinking */
            transform.localScale = initialScale * (0.2f + 0.8f * fraction);

            /* follow the target */
            transform.position = Vector3.Lerp(transform.position, target.position, 0.3f);
        } else {
            /* detach from the target */
            //Color currColor = renderer.color;
            //Color newColor = new Color(currColor.r, currColor.g, currColor.b, currColor.a * 1.1f);
            if (timeElapsed > lockTime + fadeTime) {
                Destroy(this.gameObject);
            }
        }

	}
}
