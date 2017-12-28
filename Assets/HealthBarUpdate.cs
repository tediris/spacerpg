using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUpdate : MonoBehaviour, IDamageListener {

    Health health;
    public GameObject parent;
    public float start_width = 1f;
    public float start_height = 1f;
    public float vert_offset = 1f;
    Vector3 goalScale;

	// Use this for initialization
	void Start () {
        /* get the parent health object */
        health = parent.GetComponent<Health>();
        transform.position = parent.transform.position + Vector3.up * vert_offset;
        transform.localScale = new Vector3(start_width, start_height, transform.localScale.z);
        goalScale = transform.localScale;
        health.RegisterListener(this);
    }

    // Update is called once per frame
    void Update () {
        transform.localScale = Vector3.Lerp(transform.localScale, goalScale, 0.2f);
	}

    public void OnDamage(float currHealth) {
        float fraction = currHealth / health.maxHealth;
        goalScale = new Vector3(fraction * start_width, start_height, transform.localScale.z);
    }

    public void OnKilled() {
        /* Do Nothing */
    }
}
