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
        // Bu kod "camera_controller" scriptine erişmek için yazıldı.

        second_ball_access = second_ball.GetComponent<game_controller2>();
        third_ball_access = third_ball.GetComponent<game_controller3>();
        // Yukarıdaki iki kod top2 ve top3'ün scriptlerine erişmek için kullanıldı.

    }
	
	
	void FixedUpdate () {

        control1 = code_access.hit1_control;
        // Bu kodla birlikte "camera_controller" scriptindeki "hit1_control" boolean'ına erişim sağlandı.

        if (control1)
        {
            boost_button.gameObject.SetActive(true);
            second_ball_access.boost_button.gameObject.SetActive(false);
            third_ball_access.boost_button.gameObject.SetActive(false);
            /* Herbir top için ayrı boost butonları oluşturuldu ve hepsi başlangıçta deaktif bir halde. Yukarıdaki "if" 
             * içindeki kodlar her top için ayrı ayrı yazıldı. Böylelikle boost butonları seçilen topa göre aktif oluyor
             * ve o topa yüksek güç uygulayabiliyor. */
        }

        vector_access = code_access.vurus_vector;
        // Bu kodla birlikte "camera_controller" scriptindeki "vurus_vector" vektörüne erişim sağlandı.

        if (Input.GetMouseButtonDown(1))
        {
            if (control1)
            {
                rb = GetComponent<Rigidbody>();
                // Eğer hit1_control "true" ise ve sağ tıkladıysak artık top1 objemize vuruş yapıyoruz.

                boost_power = 1;
                /* Boost butonuna tıkladığımda bir defaya mahsus olarak topa sert vurmak istiyorum. Bu yüzden  yukarıdaki
                 * kodu yazarak boost_power'ı tekrardan eski konumuna getirdim. */

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
                // Mouse'ye tıklamayı bıraktığım andaki pozisyonu belirledim.

                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                // Mouse'ye ilk tıkladığım ve tıklamayı bıraktığım anlardaki pozisyonlar arası vektörü belirledim.

                currentSwipe.Normalize();
                /* Current Swipe vektörünün uzunluğunu 1 birim yapmak için yukarıdaki kod yazıldı. Aksi halde istemsiz
                 * kuvvetlerde topa vuruşlar gözlenebiliyordu. */

                swipevector = new Vector3(currentSwipe.x, 0, currentSwipe.y);
                /* Ben topa x ve z eksenlerinde vurmak istediğim için currentSwipe vektörünün x ve y bileşenini 
                 * swipevector vektörünün x ve z bileşenlerine eşitledim. */

                rb.AddForce((swipevector+vector_access)*boost_power*2, ForceMode.Impulse);
                // Bu kodla birlikte kameramın bakış açısını da hesaba katarak sürüklediğim yönde vuruş yapabiliyorum.

                camera_box.transform.position = new Vector3(0, 4.91f, -13.44f);
                camera_box.transform.eulerAngles = new Vector3(22.834f, 0, 0);
                // Topa vurduktan sonra kameramın açısını ve pozisyonunu oyunun ilk başladığı pozisyona ayarladım.

                code_access.rotation_control = false;
                code_access.hit1_control = false;
                /* Topa vurduktan sonra kameramın hala topa bakış açısını ayarlıyor olmasını ve hiçbir top seçimi ve 
                 * kamera ayarı yapmadan tekrar sağ tıkladığımda vuruş yapmayı istemiyorum. Bunu engellemek için 
                 * camera_controller scriptindeki rotation_control'ü ve hit1_control'ü "false" yaptım. */

                vurus_sayisi += 1;
                toplam_vurus_sayisi = vurus_sayisi + second_ball_access.vurus_sayisi + third_ball_access.vurus_sayisi;
                Debug.Log(toplam_vurus_sayisi);
                /* Yukarıdaki üç kodla birlikte topa vuruş sayısı kısıtlandı. OnTriggerExit ve OnCollisionStay metodlarının
                 * kodlarında da yazdığı üzere toplam 4 vuruş hakkımız olacak. 4 vuruşu geçtiğimiz anda aradan geçme ve
                 * gol olma durumları gerçekleşmeyecek. */
            }


        }

        

    }

    private void OnTriggerExit(Collider other)
    {
        string obje_ismi = other.gameObject.name;

        if (aradan_gecis_activity==true && obje_ismi.Equals("aradan_gecme") && toplam_vurus_sayisi<=4)
        {
            Debug.Log("Secilen top1 gecti.");

            code_access.i = 0;
            code_access.j = 0;
            code_access.k = 0;
            // Eğer aradan geçme gerçekleştiyse seçtiğim topu bir kez daha seçebilmeliyim. Bu yüzden yukarıdaki kodlar yazıldı.

            aradan_gecis_control = true;
        }
        
        /* OnTriggerExit metoduna ve altındaki kodlara göre top1 "aradan_gecme" ismindeki objenin içinden çıktığı anda
         * top1'in geçtiğini kontrol edebiliyor. */
    }

    private void OnCollisionStay(Collision collision)
    {
        string kale_ismi = collision.gameObject.name;

        if(aradan_gecis_control==true && kale_ismi.Equals("kale") && toplam_vurus_sayisi<=4)
        {
            Debug.Log("Gol");
        }

        /* Bu metoda göre topumuz aradan_gecis_control boolean'ını sağlayıp diğer iki topun arasından geçtiyse, toplam
         * vuruş sayısının altındaysa ve "kale"nin alanına girdiyse gol atma işlemimiz tamamlanmış oluyor */
    }

    

    public void boost()
    {
        boost_power = 5;
        /* Boost butonuna tıkladığımda artık top1'in vuruş gücü artıyor. Bu aşamadan sonra "Boost" butonunu top2 ve top3
         * için de aktif edeceğim... */
    }

    /* game_controller scriptinin çok benzeri game_controller2 ve game_controller3 adı altında top2 ve top3'ün durumları
     * için de yazıldı. Bu üç scriptin arasında hiçbir fark yok. Sadece birisi top1'in, diğeri top2'nin, en sonuncusu da
     * top3 topa vurma, aradan geçme ve gol olma durumlarını kontrol ediyor. */
}

