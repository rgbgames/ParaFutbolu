using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour {


    private Vector3 camera_first_position;
    private Vector3 distance;

    private GameObject camera_motion; 
    //Kamera bu GameObject'in alt nesnesi olacak ve bunun etrafında dönecek ve hareket edecek.

    public bool rotation_control=false;
    public bool transform_control = false;
    public bool hit1_control = false;
    public bool hit2_control = false;
    public bool hit3_control = false;
    private Vector3 vurus_acisi;
    public Vector3 vurus_vector;

    public int i = 0;
    public int j = 0;
    public int k = 0;

    private Vector3 top1_pos;
    private Vector3 top2_pos;
    private Vector3 top3_pos;
    public bool camera_moving_pos=true;

    private Vector3 camera_pos;
    private Vector3 hit_pos;


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
        
        top1_pos = GameObject.Find("top1").transform.position;
        top2_pos = GameObject.Find("top2").transform.position;
        top3_pos = GameObject.Find("top3").transform.position;



        camera_pos = transform.position;

        if (camera_moving_pos)
        {
            if (top1_pos.z <= top2_pos.z && top1_pos.z < top3_pos.z)
            {
                transform.position = Vector3.Lerp(camera_pos, new Vector3(0, 5.92f, top1_pos.z - 4.8f), 0.1f);
                transform.eulerAngles = new Vector3(22.834f, 0, 0);
            }

            else if (top2_pos.z < top1_pos.z && top2_pos.z <= top3_pos.z)
            {
                transform.position = Vector3.Lerp(camera_pos, new Vector3(0, 5.92f, top2_pos.z - 4.8f), 0.1f);
                transform.eulerAngles = new Vector3(22.834f, 0, 0);
            }

            else if (top3_pos.z < top2_pos.z && top3_pos.z <= top1_pos.z)
            {
                transform.position = Vector3.Lerp(camera_pos,new Vector3(0, 5.92f, top3_pos.z - 4.8f), 0.1f);
                transform.eulerAngles = new Vector3(22.834f, 0, 0);
            }


        }

        /* Öncelikle yapmak istediğim şey oyunun ilk başında topa en yukarıdan bakan kameramın sahada kendi kalemize en 
         * yakın olan topa sürekli olarak yaklaşmasını sağlamak. Bunun sebebi ise topa vuruş yaptığımda en yukarıda 
         * bulunan kamera git gide toplardan uzaklaşıyordu ve bu kötü bir görünüme sebep oluyordu. Bu yaklaşma düzeyini ise
         * şu şekilde belirledim: Başlangıçtaki top1 nesnesi ile kameram arasındaki mesafeyi hesapladım. Ardından yukarıda-
         * ki kodlarla birlikte z ekseninde en arkada bulunan topu sürekli olarak kontrol ettim. Ve bu en arkadaki topa 
         * kameramı yaklaştırdım. "Lerp" fonksiyonu ile de pürüzsüz bir geçiş sağladım. */

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            camera_moving_pos = false;
            /* Topa seçim yaptığımda en yukarıdaki kameramın en arkadaki topu takip etmesini bırakmasını istediğim için
             * bu kod yazıldı. */


            if(Physics.Raycast(ray,out hit))
            {
                if (hit.transform.name == "top1" && i==0)
                {

                    /* Bir raycasthit kodu yazıldı. Yukarıdaki koda göre kameramdan mouse'nin pozisyonuna doğru ışınlar
                     * çıkıyor. "if(Input.GetMouseButtonDown(0))" komutuyla birlikte de topa sol tıkladığım andaki 
                     * ışınları kontrol edeceğim. */

                    hit1_control = true;
                    hit2_control = false;
                    hit3_control = false;
                    /* "top1" topuma tıkladığımda sadece "top1" e vuruş yapılması için bu kod yazıldı. Aynı durum aşa-
                     * ğıda top2 ve top3 için de yapıldı. */

                    hit_pos = hit.transform.position;
                    // Tıkladığım topun pozisyonunun vektörel değeri alındı.
                    
                    hit.transform.rotation = Quaternion.Euler(0, 0, 0);
                    /* Bu kodla birlikte vector_access yönünde döndürülmüş olan topumun local axislerini tekrardan sıfır-
                     * lamış oldum. */

                    camera_motion.transform.eulerAngles = new Vector3(0, 0, 0);
                    /* Topumun bakış açısını ayarladıktan sonra topa tekrar tıkladığımda kamera daha önce ayarlamış
                     * olduğum bakış açısında kalıyordu. Bunu düzeltmek için bu kod yazıldı. Artık topa her tıkladığımda
                     * kamera döndürme işlemi sıfırlanıyor ve kameram tam karşıyı gösteriyor. */

                    transform.rotation = Quaternion.Euler(22.834f, 0, 0);
                    // Birkaç top seçimi yaptıktan sonra bakış açılarında ufak bir hata oluyordu. Bu kodla birlikte hata düzeltildi.

                    transform_control = true; 
                    /* Kameramın topa tıkladığımda pürüzsüz bir şekilde toplara yaklaşmasını istiyorum. Bu yaklaşma hare-
                     * ketiyle ilgili kodlar if(transform_control) kod grubunun içine yazıldı. Bu kodların top seçimini yap-
                     * tıktan sonra çalışmasını istediğim için bu kontrol boolean'ı yazıldı. */

                    i = 1;
                    j = 0;
                    k = 0;
                    // Seçim yaptığım topa sadece bir kere tıklayabilmek istiyorum. Bu yüzden yukarıdaki üç kod yazıldı.
                }

                camera_pos = transform.position;
                /* Topa yaklaşan kameranın tekrardan eski yerine dönerken pürüzsüz geçiş yapmasında sorun yaşıyordum. Herbir
                 * if(hit.transform.name) kod grubunun altına bu kodun yazılmasıyla sorun düzeldi. */

                // Yukarıdaki kodlar çok ufak ayar farklılıklarıyla top2 ve top3 için aşağıya da yazıldı.


                if (hit.transform.name == "top2" && j==0)
                {
                    hit2_control = true;
                    hit1_control = false;
                    hit3_control = false;
                    hit_pos = hit.transform.position;
                    camera_motion.transform.eulerAngles = new Vector3(0, 0, 0);
                    transform.rotation = Quaternion.Euler(22.834f, 0, 0);
                    transform_control = true;
                    i = 0;
                    j = 1;
                    k = 0;
                }

                camera_pos = transform.position;

                if (hit.transform.name == "top3" && k==0)
                {
                    hit3_control = true;
                    hit1_control = false;
                    hit2_control = false;
                    hit_pos = hit.transform.position;
                    camera_motion.transform.eulerAngles = new Vector3(0, 0, 0);
                    transform.rotation = Quaternion.Euler(22.834f, 0, 0);
                    transform_control = true;
                    i = 0;
                    j = 0;
                    k = 1;
                }

                camera_pos = transform.position;
            }

        }

        if (transform_control)
        {
            camera_motion.transform.position = Vector3.Lerp(camera_motion.transform.position, hit_pos, 0.1f);
            transform.position = Vector3.Lerp(camera_pos, camera_motion.transform.position + distance, 0.1f);
            StartCoroutine(hareket_process());
            /* Kameramın seçim yapılan toplar arasında pürüzsüz geçiş yapmasını istiyordum. Fakat Update fonksiyonu çok
             * hızlı çalıştığı için "Lerp" yavaş geçiş yapma fonksiyonu bir türlü çalışmıyordu. Aşağıdaki hareket_process()
             * fonksiyonu ile birlikte Update fonksiyonunu yavaşlattım ve toplar arası seçimde camera_motion gameobjectimin 
             * pürüzsüz geçişini sağladım. Kameram da doğal olarak bağlı olduğu camera_motion gameobjecti ile birlikte hare-
             * ket etti. */

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

    IEnumerator hareket_process()
    {
        yield return new WaitForSeconds(0.5f);
        rotation_control = true;
        transform_control = false;
        /* Bu fonksiyonun açıklaması if (transform_control) kod grubunun altında yapıldı. Pürüzsüz kamera geçişi sağlandıktan
         * sonra toplara kameramızın bakış açısını ayarlayabilmemizi istediğim için bu fonksiyonun altına 
         * "rotation_control=true;"  yazıldı. */
    }
}
