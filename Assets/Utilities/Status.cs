using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public enum State {
        vulnerable,
        invulnerable
    }

    public State state;

	// Use this for initialization
	void Start () {
        state = State.vulnerable;
	}
}
