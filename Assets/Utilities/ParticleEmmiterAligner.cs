using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmmiterAligner : MonoBehaviour
{

    ParticleSystem ps;

    // Use this for initialization
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var main = ps.main;
        //main.startRotation = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        main.startRotation = Mathf.Atan2(-transform.up.y, transform.up.x) - Mathf.PI / 2;
    }

}
