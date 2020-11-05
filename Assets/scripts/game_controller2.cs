using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_controller2 : MonoBehaviour {

    private Rigidbody rb;
    private camera_controller code_access;
    public GameObject camera_box;
    private bool control2;

    private Vector3 vector_access;

    private game_controller first_ball_access;
    public GameObject first_ball;

    private game_controller3 third_ball_access;
    public GameObject third_ball;

    public bool aradan_gecis_activity = false;
    private bool aradan_gecis_control = false;

    void Start () {
        code_access = camera_box.GetComponent<camera_controller>();

        first_ball_access = first_ball.GetComponent<game_controller>();

        third_ball_access = third_ball.GetComponent<game_controller3>();
    }

    
    void FixedUpdate()
    {

        control2 = code_access.hit2_control;
        vector_access = code_access.vurus_vector;

        if (Input.GetMouseButtonDown(1))
        {
            if (control2)
            {
                rb = GetComponent<Rigidbody>();
                rb.AddForce(vector_access, ForceMode.Impulse);
                code_access.rotation_control = false;
                code_access.hit2_control = false;
                camera_box.transform.position = new Vector3(0, 4.91f, -13.44f);
                camera_box.transform.eulerAngles = new Vector3(22.834f, 0, 0);
                aradan_gecis_activity = true;
                first_ball_access.aradan_gecis_activity = false;
                third_ball_access.aradan_gecis_activity = false;
            }


        }


    }

    private void OnTriggerExit(Collider other)
    {
        string top_ismi = other.gameObject.name;

        if (aradan_gecis_activity == true && top_ismi.Equals("aradan_gecme"))
        {
            Debug.Log("Secilen top2 gecti.");
            aradan_gecis_control = true;

        }


    }

    private void OnCollisionStay(Collision collision)
    {
        string kale_ismi = collision.gameObject.name;

        if (aradan_gecis_control == true && kale_ismi.Equals("kale"))
        {
            Debug.Log("Gol");
        }
    }
}
