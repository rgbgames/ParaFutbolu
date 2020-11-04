using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour {


    public Vector3 camera_first_position;
    public Vector3 distance;

    public GameObject camera_motion; 
    //Kamera bu GameObject'in alt nesnesi olacak ve bunun etrafında dönecek ve hareket edecek.

    public bool rotation_control=false;
    public bool hit1_control = false;
    public bool hit2_control = false;
    public bool hit3_control = false;
    public Vector3 vurus_acisi;
    public Vector3 vurus_vector;
    






    void Start () {

        camera_first_position = new Vector3(0, 2.46f, -11.4f); 
        // Bu vektör topa ilk tıkladığımda kameramın bulunmasını istediğim pozisyonu ifade ediyor.

        camera_motion = new GameObject();
        camera_motion.transform.position= GameObject.Find("top1").transform.position; 
        // camera_motion GameObject'in ilk pozisyonu "top1" GameObject'in pozisyonuna eşitlendi.

        transform.parent = camera_motion.transform; 
        //Main Camera camera_motion'un alt nesnesi yapıldı. Böylelikle Main Camera camera_motion'a bağlı hareket edecek.

        distance = camera_first_position - camera_motion.transform.position; 
        /* "distance" vektörü kamera ile topun arasındaki mesafeyi kontrol edecek. Topa tıkladığımda kameramın bu "distance"
         * vektörü boyunca topa yaklaşmasını istiyorum. */

    }
	
	
	void Update () {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.transform.tag == "ball") 
                {
                    /* Bir raycasthit kodu yazıldı. Yukarıdaki koda göre kameramdan mouse'nin pozisyonuna doğru ışınlar
                     * çıkıyor. "if(Input.GetMouseButtonDown(0))" komutuyla birlikte de topa sol tıkladığım andaki 
                     * ışınları kontrol edeceğim. */
                    
                    camera_motion.transform.position = hit.transform.position; 
                    // camera_motion GameObject'inin pozisyonu "ball" tag'ine sahip toplarımızın pozisyonuna eşitlendi.

                    camera_motion.transform.eulerAngles = new Vector3(0, 0, 0);
                    /* Topumun bakış açısını ayarladıktan sonra topa tekrar tıkladığımda kamera daha önce ayarlamış
                     * olduğum bakış açısında kalıyordu. Bunu düzeltmek için bu kod yazıldı. Artık topa her tıkladığımda
                     * kamera döndürme işlemi sıfırlanıyor ve kameram tam karşıyı gösteriyor. */

                    transform.position = camera_motion.transform.position + distance;
                    /* Bu kodla birlikte kamera topa doğru distance vektörü mesafesinde yaklaşacak. Bir diğer deyişle topumu
                     * distance vektörü mesafesinde takip edecek. */

                    transform.rotation = Quaternion.Euler(22.834f, 0, 0);

                    rotation_control = true; // Kameramın topa tıkladığımda hareket etmesini istediğim için bu kontrol kodu yazıldı.

                    if (hit.transform.name == "top1")
                    {
                        hit1_control = true;
                        hit2_control = false;
                        hit3_control = false;

                        /* "top1" topuma tıkladığımda sadece "top1" e vuruş yapılması için bu kod yazıldı. Aynı durum aşa-
                         * ğıda top2 ve top3 için de yapıldı. */


                    }

                    if (hit.transform.name == "top2")
                    {
                        hit2_control = true;
                        hit1_control = false;
                        hit3_control = false;
                    }

                    if (hit.transform.name == "top3")
                    {
                        hit3_control = true;
                        hit1_control = false;
                        hit2_control = false;
                    }

                }
            }

        }

        if (rotation_control)
        {
            float yatay_hareket = Input.GetAxis("Horizontal");
            camera_motion.transform.eulerAngles = new Vector3(camera_motion.transform.eulerAngles.x, camera_motion.transform.eulerAngles.y + yatay_hareket, camera_motion.transform.eulerAngles.z);
            /* camera_motion GameObject'im şimdi bu kodla birlikte dönüş sağlıyor. camera_motion'a bağlı kameram da doğal 
             * olarak onunla birlikte dönüyor. */

            vurus_acisi = camera_motion.transform.position - transform.position;
            vurus_vector = new Vector3(vurus_acisi.x, 0, vurus_acisi.z);
            /* "vurus_acisi" vektörü kamera açısı ayarlandıktan sonra kamera ile top arasındaki açıyı kontrol eden bir 
             * vektör fakat bu vektör 3 boyutlu. Ben topa 2 boyutta vurmak istiyorum ve y ekseninde kuvvet uygulamak iste-
             * miyorum. Bunu başarmak için "vurus_vector" kodu yazıldı ve "vurus_acisi" vektörünün y bileşeni bertaraf e-
             * dildi. */

        }

        



    }
}
