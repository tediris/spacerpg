using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageListener {
    void OnDamage(float health);
    void OnKilled();
}

public class Health : MonoBehaviour {

    public float maxHealth;
    public float health;
    public List<IDamageListener> listeners = new List<IDamageListener>();

    void Start() {
        health = maxHealth;
    }

	// Use this for initialization
    public void RegisterListener(IDamageListener listener) {
        listeners.Add(listener);
    }
	
	public void Damage(float damage) {
        health -= damage;
        if (health <= 0) {
            health = 0f;
            // call any dead listeners
            for (int i = 0; i < listeners.Count; i++) {
                listeners[i].OnDamage(health);
                listeners[i].OnKilled();
            }
        } else {
            // call any damage listeners
            for (int i = 0; i < listeners.Count; i++) {
                listeners[i].OnDamage(health);
            }
        }

    }
}
