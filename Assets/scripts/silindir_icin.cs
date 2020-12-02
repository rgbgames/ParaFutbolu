using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silindir_icin : MonoBehaviour {

    private float deger_x;
    private float deger_z;
    private Rigidbody rb;
    private Vector3 pos;
    private Vector3 hiz_vector;
    

	void Start () {

        rb = GetComponent<Rigidbody>();
        deger_x = Random.Range(-1.0f, 1.0f);
        deger_z = Random.Range(-1.0f, 1.0f);
        hiz_vector = new Vector3(deger_x, 0, deger_z);
        hiz_vector.Normalize();
        rb.velocity = hiz_vector;
    }
	
	
	void Update () {

        pos = transform.position;
        if(pos.x<=-3.5f || pos.x >= 3.5f)
        {
            deger_x = -deger_x;
            hiz_vector = new Vector3(deger_x, 0, deger_z);
            hiz_vector.Normalize();
            rb.velocity = hiz_vector;
        }
        if(pos.z<=-2.0f || pos.z >= 2.0f)
        {
            deger_z = -deger_z;
            hiz_vector = new Vector3(deger_x, 0, deger_z);
            hiz_vector.Normalize();
            rb.velocity = hiz_vector;
        }

    }

    

}
