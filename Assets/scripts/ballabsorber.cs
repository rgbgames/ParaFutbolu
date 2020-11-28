using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballabsorber : MonoBehaviour {

    private bool saga_donus;
    private bool sola_donus;
    

    void Start () {

        saga_donus = true;
        sola_donus = false;
        
    }
	
	
	void Update () {

        if(saga_donus==true && transform.position.x < 3.5f)
        {
            transform.Translate(Vector3.right * Time.deltaTime*3);
            if (transform.position.x >= 3.4f)
            {
                saga_donus = false;
                sola_donus=true;
            }
        }

        if (sola_donus == true && transform.position.x > -3.5f)
        {
            transform.Translate(Vector3.left * Time.deltaTime*3);
            if (transform.position.x <= -3.4f)
            {
                saga_donus = true;
                sola_donus=false;
            }
        }

    }

    

   
}
