using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_controller3 : MonoBehaviour {

    private Rigidbody rb;
    private camera_controller code_access;
    public GameObject camera_box;
    private bool control3;

    private Vector3 vector_access;

    private game_controller first_ball_access;
    public GameObject first_ball;

    private game_controller2 second_ball_access;
    public GameObject second_ball;

    public bool aradan_gecis_activity = false;
    private bool aradan_gecis_control = false;

    public int vurus_sayisi = 0;
    private int toplam_vurus_sayisi;

    private int boost_power = 1;
    public UnityEngine.UI.Button boost_button;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private Vector3 swipevector;



    void Start () {
        code_access = camera_box.GetComponent<camera_controller>();

        first_ball_access = first_ball.GetComponent<game_controller>();

        second_ball_access = second_ball.GetComponent<game_controller2>();
    }

    
    void FixedUpdate()
    {

        control3 = code_access.hit3_control;
        vector_access = code_access.vurus_vector;

        if (control3)
        {
            boost_button.gameObject.SetActive(true);
            first_ball_access.boost_button.gameObject.SetActive(false);
            second_ball_access.boost_button.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (control3)
            {
                rb = GetComponent<Rigidbody>();
                boost_power = 1;
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                aradan_gecis_activity = true;
                first_ball_access.aradan_gecis_activity = false;
                second_ball_access.aradan_gecis_activity = false;
                
            }


        }

        if (Input.GetMouseButtonUp(1))
        {
            if (control3)
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();
                swipevector = new Vector3(currentSwipe.x, 0, currentSwipe.y);
                rb.AddForce((swipevector+vector_access)*boost_power*2, ForceMode.Impulse);
                camera_box.transform.position = new Vector3(0, 4.91f, -13.44f);
                camera_box.transform.eulerAngles = new Vector3(22.834f, 0, 0);
                code_access.rotation_control = false;
                code_access.hit3_control = false;
                vurus_sayisi += 1;
                toplam_vurus_sayisi = vurus_sayisi + first_ball_access.vurus_sayisi + second_ball_access.vurus_sayisi;
                Debug.Log(toplam_vurus_sayisi);
                
            }
            
        }


    }

    private void OnTriggerExit(Collider other)
    {
        string top_ismi = other.gameObject.name;

        if (aradan_gecis_activity == true && top_ismi.Equals("aradan_gecme") && toplam_vurus_sayisi<=4)
        {
            Debug.Log("Secilen top3 gecti.");
            aradan_gecis_control = true;

        }


    }

    private void OnCollisionStay(Collision collision)
    {
        string kale_ismi = collision.gameObject.name;

        if (aradan_gecis_control == true && kale_ismi.Equals("kale") && toplam_vurus_sayisi<=4)
        {
            Debug.Log("Gol");
            code_access.i = 0;
            code_access.j = 0;
            code_access.k = 0;
        }
    }

    public void boost()
    {
        boost_power = 5;

    }


}
