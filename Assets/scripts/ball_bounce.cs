using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_bounce : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 last_velocity;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

   
    void Update () {
        last_velocity = rb.velocity;
	}

    private void OnCollisionEnter(Collision collision)
    {
        string obje_ismi = collision.gameObject.name;
        if(obje_ismi.Equals("duvar") || obje_ismi.Equals("duvar (1)") || obje_ismi.Equals("duvar (2)") || obje_ismi.Equals("duvar (3)") || obje_ismi.Equals("duvar (4)") || obje_ismi.Equals("duvar (5)")  || obje_ismi.Equals("yumruk_duvari"))
        {
            var speed = last_velocity.magnitude;
            var direction = Vector3.Reflect(last_velocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }
}
