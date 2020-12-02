using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yumruk_duvari : MonoBehaviour {

    private bool ilk_hareket;
    private bool ikinci_hareket;
    private bool ucuncu_hareket;
    private bool ball_tounching_control;

	void Start () {

        ilk_hareket = true;
        ikinci_hareket = false;
        ucuncu_hareket = false;
        ball_tounching_control = false;
	}
	
	
	void FixedUpdate () {

        if (ilk_hareket == true)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(3, 1.3f, -3.676f), 0.1f);
            StartCoroutine(hareket_process());
        }
        
        if (ikinci_hareket == true && ball_tounching_control==false)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-3.95f, 1.3f, -3.676f), 0.1f);
            StartCoroutine(hareket_process2());
        }

        if (ikinci_hareket == true && ball_tounching_control == true)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-3.65f, 1.3f, -3.676f), 0.1f);
            StartCoroutine(hareket_process2());
        }

        if (ucuncu_hareket==true)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(4.05f, 1.3f, -3.676f), 0.1f);
            StartCoroutine(hareket_process3());
        }


	}

    private void OnCollisionEnter(Collision collision)
    {
        string obje_ismi = collision.gameObject.name;
        if(obje_ismi.Equals("top1") || obje_ismi.Equals("top2") || obje_ismi.Equals("top3"))
        {
            ball_tounching_control = true;
        }
    }

    IEnumerator hareket_process()
    {
        yield return new WaitForSeconds(2);
        ikinci_hareket = true;
        ilk_hareket = false;
    }

    IEnumerator hareket_process2()
    {
        yield return new WaitForSeconds(2);
        ikinci_hareket = false;
        ucuncu_hareket = true;
    }

    IEnumerator hareket_process3()
    {
        yield return new WaitForSeconds(2);
        ilk_hareket = true;
        ucuncu_hareket = false;
    }

    
}
