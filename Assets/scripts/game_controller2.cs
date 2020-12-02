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

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private Vector3 swipevector;
    private Vector3 force_vector;

    private float start_time;

    private Vector3 firlatma;
    private float x;
    private float z;
    private bool absorption_control;
    private bool firlatma_control;


    void Start () {
        code_access = camera_box.GetComponent<camera_controller>();
        first_ball_access = first_ball.GetComponent<game_controller>();
        third_ball_access = third_ball.GetComponent<game_controller3>();
        absorption_control = false;
        firlatma_control = false;
       

    }

    
    void FixedUpdate()
    {

        control2 = code_access.hit2_control;
        vector_access = code_access.vurus_vector;
        transform.rotation = Quaternion.LookRotation(vector_access);
        x = Random.Range(-1.0f, 1.0f);
        z = Random.Range(-1.0f, 1.0f);

        if (Input.GetMouseButtonDown(1))
        {
            if (control2)
            {
                rb = GetComponent<Rigidbody>();
                start_time = Time.time;
                rb.drag = 0.2f;
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                aradan_gecis_activity = true;
                first_ball_access.aradan_gecis_activity = false;
                third_ball_access.aradan_gecis_activity = false;
                
            }


        }

        if (Input.GetMouseButtonUp(1))
        {
            if (control2)
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();
                swipevector = new Vector3(currentSwipe.x, 0, currentSwipe.y);
                swipevector /= (Time.time - start_time);
                rb.AddRelativeForce(swipevector*3 , ForceMode.Impulse);
                code_access.rotation_control = false;
                code_access.hit2_control = false;
                StartCoroutine(top_drag());
                code_access.camera_moving_pos = true;
                
            }
            
        }

        if (absorption_control)
        {
            Vector3 absorber_position = new Vector3(GameObject.Find("ball_absorber").transform.position.x, 1.02f, GameObject.Find("ball_absorber").transform.position.z);
            transform.position = Vector3.Lerp(transform.position, absorber_position, 0.1f);
            firlatma = new Vector3(x, 0, z);
            firlatma.Normalize();
            StartCoroutine(absorption_engel());
        }

        if (firlatma_control)
        {
            absorption_control = false;
            rb.drag = 0.2f;
            rb.AddForce(firlatma, ForceMode.Impulse);
            firlatma_control = false;
        }


    }

    private void OnTriggerEnter(Collider engel)
    {
        string engel_ismi = engel.gameObject.name;
        if (engel_ismi.Equals("ball_absorber"))
        {
            GameObject.Find("ball_absorber").GetComponent<ballabsorber>().enabled = false;
            rb.velocity = new Vector3(0, 0, 0);
            absorption_control = true;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        string engel_ismi = other.gameObject.name;
        if (engel_ismi.Equals("camur"))
        {
            rb.drag = 5;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string top_ismi = other.gameObject.name;

        if (aradan_gecis_activity == true && top_ismi.Equals("aradan_gecme"))
        {
            Debug.Log("Secilen top2 gecti.");
            aradan_gecis_control = true;
            code_access.i = 0;
            code_access.j = 0;
            code_access.k = 0;

        }

        if (top_ismi.Equals("ball_absorber"))
        {
            GameObject.Find("ball_absorber").GetComponent<ballabsorber>().enabled = true;

        }


    }

    private void OnCollisionStay(Collision collision)
    {
        string obje_ismi = collision.gameObject.name;

        if (aradan_gecis_control == true && obje_ismi.Equals("kale"))
        {
            Debug.Log("Gol");
            code_access.i = 0;
            code_access.j = 0;
            code_access.k = 0;
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        string obje_ismi = collision.gameObject.name;

        if (obje_ismi.Equals("yumruk"))
        {

            transform.parent = collision.transform;
        }

    }


    IEnumerator top_drag()
    {
        yield return new WaitForSeconds(0.4f);
        rb.drag = 2.4f;
    }

    IEnumerator absorption_engel()
    {
        yield return new WaitForSeconds(1.5f);
        firlatma_control = true;


    }

}
