using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_controller : MonoBehaviour {

   
    private Rigidbody rb;

    private camera_controller code_access;
    public GameObject camera_box;

    private game_controller2 second_ball_access;
    public GameObject second_ball;

    private game_controller3 third_ball_access;
    public GameObject third_ball;

    private bool control1;
    public bool aradan_gecis_activity=false;
    private bool aradan_gecis_control=false;
    
    private Vector3 vector_access;
    
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private Vector3 swipevector;

    private float start_time;
    
    
    private Vector3 firlatma;
    private float x;
    private float z;
    private bool absorption_control;
    private bool firlatma_control;
    

    void Start () {

        code_access = camera_box.GetComponent<camera_controller>();
        // Bu kod "camera_controller" scriptine erişmek için yazıldı.

        second_ball_access = second_ball.GetComponent<game_controller2>();
        third_ball_access = third_ball.GetComponent<game_controller3>();
        // Yukarıdaki iki kod top2 ve top3'ün scriptlerine erişmek için kullanıldı.

        absorption_control = false;
        firlatma_control = false;
        

    }
	
	
	void FixedUpdate () {

        control1 = code_access.hit1_control;
        // Bu kodla birlikte "camera_controller" scriptindeki "hit1_control" boolean'ına erişim sağlandı.

        vector_access = code_access.vurus_vector;
        // Bu kodla birlikte "camera_controller" scriptindeki "vurus_vector" vektörüne erişim sağlandı.

        x = Random.Range(-1.0f, 1.0f);
        z = Random.Range(-1.0f, 1.0f);

        transform.rotation = Quaternion.LookRotation(vector_access);
        /* Kamera ile topa bakış açımı ayarladıktan sonra bakış açımın tam karşısının +z ekseni; bu bakış yönünün sağının
         * +x ekseni olmasını istiyorum. Eğer bunu sağlarsam topa sürükleme hareketiyle vuruş yaptığımda kendi bakış açımla
         * vuruş yapabileceğim. Bu kodla birlikte topumun axislerini camera_controller içindeki vector_access yönünde değiş-
         * tirdim ve yapmak istediğim sonuca ulaştım. Bu kod yazılmadan önce bakış açımı ayarlayıp vuruş yapmak istediğimde
         * unity benim bakış açımın karşısını +z olarak almak yerine oyunun kendi axislerindeki +z eksenini kabul ediyordu.
         * Bu da istediğim yöne vuruş yapma şansımı ortadan kaldırıyordu. */



        if (Input.GetMouseButtonDown(1))
        {
            if (control1)
            {
                rb = GetComponent<Rigidbody>();
                // Eğer hit1_control "true" ise ve sağ tıkladıysak artık top1 objemize vuruş yapıyoruz.

                start_time = Time.time;
                // Topa tıkladığım anda bir zaman sayacı başlattım. 

                rb.drag = 0.2f;
                // Topa tıkladığım andaki sürtünme kuvvetini belirledim. 

                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                // Topa tıkladığım andaki mouse'nin pozisyonunu belirledim.

                aradan_gecis_activity = true;
                second_ball_access.aradan_gecis_activity = false;
                third_ball_access.aradan_gecis_activity = false;
                /* Topa tıkladığımda sadece top1'in aradan geçip geçmediğini kontrol etmek istiyorum. Bu yüzden diğer
                 * topların aradan geçme boolean'larına eriştim ve onları "false" yaptım. */

            }

            
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (control1)
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                // Mouse'a tıklamayı bıraktığım andaki pozisyonu belirledim.

                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                // Mouse'ye ilk tıkladığım ve tıklamayı bıraktığım anlardaki pozisyonlar arası vektörü belirledim.

                currentSwipe.Normalize();
                /* Current Swipe vektörünün uzunluğunu 1 birim yapmak için yukarıdaki kod yazıldı. Aksi halde istemsiz
                 * kuvvetlerde topa vuruşlar gözlenebiliyordu. */

                swipevector = new Vector3(currentSwipe.x, 0, currentSwipe.y);
                /* Ben topa x ve z eksenlerinde vurmak istediğim için currentSwipe vektörünün x ve y bileşenini 
                 * swipevector vektörünün x ve z bileşenlerine eşitledim. */

                swipevector /= (Time.time - start_time);
                // Sürükleme işleminin hızına göre vuruş vektörümün kuvvetini belirlemek için yukarıdaki kod yazıldı.

                rb.AddRelativeForce(swipevector*3, ForceMode.Impulse);
                // Bu kodla birlikte kameramın bakış açısını da hesaba katarak sürüklediğim yönde vuruş yapabiliyorum.
                // Swipevector'u en düşük olarak 2 ile çarpacağız. Top geliştikçe bu değer 3'e kadar çıkacak...

                code_access.rotation_control = false;
                code_access.hit1_control = false;
                /* Topa vurduktan sonra kameramın hala topa bakış açısını ayarlıyor olmasını ve oyunun top1'e vuruş izni 
                 * vererek kalmasını istemiyorum. Bu yüzden yukarıdaki kodlar yazıldı. */

                StartCoroutine(top_drag());
                /* Topa vurma işlemi gerçekleştikten sonra önce akıcı gitmesini; ardından bir süre sonra hızlı bir şekilde
                 * yavaşlamasını istediğim için top_drag() fonksiyonu yazıldı. Böylece topa vurulduktan bir süre sonra 
                 * drag force arttırıldı. */

                code_access.camera_moving_pos = true;
                /* Topa tıkladıktan sonra kameramın en arkada kalan topu takip etmesi için camera_controller scriptindeki 
                 * camera_moving_pos boolean'ı aktif hale getirildi. */

                

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
        string obje_ismi = other.gameObject.name;

        if (aradan_gecis_activity==true && obje_ismi.Equals("aradan_gecme"))
        {
            Debug.Log("Secilen top1 gecti.");

            code_access.i = 0;
            code_access.j = 0;
            code_access.k = 0;
            // Eğer aradan geçme gerçekleştiyse seçtiğim topu bir kez daha seçebilmeliyim. Bu yüzden yukarıdaki kodlar yazıldı.

            aradan_gecis_control = true;
        }

        if (obje_ismi.Equals("ball_absorber"))
        {
           GameObject.Find("ball_absorber").GetComponent<ballabsorber>().enabled = true;
           
        }

        /* OnTriggerExit metoduna ve altındaki kodlara göre top1 "aradan_gecme" ismindeki objenin içinden çıktığı anda
         * top1'in geçtiğini kontrol edebiliyor. */

    }

    private void OnCollisionStay(Collision collision)
    {
        string obje_ismi = collision.gameObject.name;

        if(aradan_gecis_control==true && obje_ismi.Equals("kale"))
        {
            Debug.Log("Gol");
            code_access.i = 0;
            code_access.j = 0;
            code_access.k = 0;
        }

        /* Bu metoda göre topumuz aradan_gecis_control boolean'ını sağlayıp diğer iki topun arasından geçtiyse ve 
         * "kale"nin alanına girdiyse gol atma işlemimiz tamamlanmış oluyor */

        
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
    
       

    /* game_controller scriptinin çok benzeri game_controller2 ve game_controller3 adı altında top2 ve top3'ün durumları
     * için de yazıldı. Bu üç scriptin arasında hiçbir fark yok. Sadece birisi top1'in, diğeri top2'nin, en sonuncusu da
     * top3 topa vurma, aradan geçme ve gol olma durumlarını kontrol ediyor. */
}

