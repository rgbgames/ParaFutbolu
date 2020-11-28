using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rakip_kaleci : MonoBehaviour {

    private GameObject kaleci_motion;
    private Vector3 distance;

    private bool saga_donus;
    private bool sola_donus;

    
    void Start () {

        kaleci_motion = new GameObject();
        kaleci_motion.transform.position = new Vector3(0, 1.06f, 8);
        transform.parent = kaleci_motion.transform;
        distance = transform.position - kaleci_motion.transform.position;
        saga_donus = true;
        sola_donus = false;

    }
	
	
	void Update () {

        float y_angle = kaleci_motion.transform.eulerAngles.y;

        if (y_angle > 180)
        {
            y_angle = y_angle - 360;
        }

        if(saga_donus==true)
        {
            kaleci_motion.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
            if (y_angle  >= 88)
            {
                saga_donus = false;
                sola_donus = true;
            }
        }

        if(sola_donus==true)
        {
            kaleci_motion.transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime);
            if (y_angle <-88)
            {
                saga_donus = true;
                sola_donus = false;
            }
        }
    }
}
