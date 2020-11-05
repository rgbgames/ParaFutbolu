using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cizgi_icin : MonoBehaviour {

    private camera_controller cameracode_access;
    public GameObject kamera_kutu;

    private bool ara_control1 = false;
    private bool ara_control2 = false;
    private bool ara_control3 = false;

    public Transform target1;
    public Transform target2;

    // Çizgi için kullanılmak üzere "aradan_gecme" isminde bir küp oluşturuldu.

	void Start () {

        cameracode_access = kamera_kutu.GetComponent<camera_controller>();
        //Bu kod "main camera" objesinin scriptine erişmek için yazıldı.
		
	}
	
	
	void Update () {

        ara_control1 = cameracode_access.hit1_control;
        ara_control2 = cameracode_access.hit2_control;
        ara_control3 = cameracode_access.hit3_control;
        /* Seçtiğim topa göre çizgi farklı pozisyonlar almalı. Seçtiğim topu kontrol edebilmek için de yukarıdaki kodlar
         * yazıldı. Bu kodlara göre hangi topa vurmak istediğimi öğreneceğim. */

        if (ara_control1)
        {
            Vector3 konum = GameObject.Find("top2").transform.position + GameObject.Find("top3").transform.position;
            transform.position = konum / 2;
            /* Eğer top1'e vuruş yapacaksam objemin pozisyonu top3 ve top2'nin tam ortasında olmalı. Bunu ayarlamak için
             * yukarıdaki iki kod yazıldı. */

            transform.LookAt(target2);
            /* Buradaki target2 top2 veya top3 olabilirdi. Ben top2'yi seçtim. Bu koda göre çizgi için oluşturduğum obje
             * top2'ye bakabilmek için bir dönüş yaptı. */

            float distance1 = Vector3.Distance(GameObject.Find("top2").transform.position, GameObject.Find("top3").transform.position);
            transform.localScale = new Vector3(0.01f, 0.1f, 0.01f + distance1);
            /* Küp top2'ye doğru dönüş yaptıktan sonra onun boyunu top2 ve top3 objeleri arasındaki mesafe kadar uzatmam
             * gerekiyordu. Bunu başarmak için de top2 ve top3 arasındaki mesafe boyu hesaplandı. Ardından bu boy kadar 
             * küp z ekseni boyunca uzatıldı. Artık top1 seçildiği zaman top2 ve top3 arasına çekilmiş bir çizgim var. 
             * Aynı işlemler aşağıdaki "if" komutları altında başka toplara vurulacağı zaman çizginin farklı pozisyon al-
             * ması için yine yazıldı. */


        }

        if (ara_control2)
        {
            Vector3 konum2= GameObject.Find("top1").transform.position + GameObject.Find("top3").transform.position;
            transform.position = konum2 / 2;
            transform.LookAt(target1);
            float distance2 = Vector3.Distance(GameObject.Find("top1").transform.position, GameObject.Find("top3").transform.position);
            transform.localScale = new Vector3(0.01f, 0.1f, 0.01f + distance2);
        }

        if (ara_control3)
        {
            Vector3 konum3 = GameObject.Find("top1").transform.position + GameObject.Find("top2").transform.position;
            transform.position = konum3 / 2;
            transform.LookAt(target2);
            float distance3 = Vector3.Distance(GameObject.Find("top1").transform.position, GameObject.Find("top2").transform.position);
            transform.localScale = new Vector3(0.01f, 0.1f, 0.01f + distance3);

        }

	}

   
}
