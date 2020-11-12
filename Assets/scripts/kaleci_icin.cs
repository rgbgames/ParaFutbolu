using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaleci_icin : MonoBehaviour {

    private GameObject kaleci_motion;
    private Vector3 distance;

    /* Bu script ve "kaleci" GameObject'i şu anda deaktif durumda. Daha sonra top karşı tarafa geçtiğinde aktif hale ge-
     * lecek. Ben şimdilik kalecinin sağa ve sola yatış mekaniğini yazdım... */

	void Start () {

        kaleci_motion = new GameObject();
        kaleci_motion.transform.position = new Vector3(0, 1.06f, -10);
        transform.parent = kaleci_motion.transform;
        distance = transform.position - kaleci_motion.transform.position;

        /* Bizim kalecimiz camera_controller scriptindeki kamera hareketiyle aynı mantıkta çalışıyor. "kaleci" çubuğumun 
         * pivot noktası tam ortada olduğundan olası bir döndürme işleminde çubuk uç noktasından değil de tam orta nokta-
         * sından sağa veya sola dönüyordu. Bu yüzden "kaleci_motion" adında yeni bir GameObject yarattım ve pozisyonunu
         * "kaleci"min pivot noktasına denk gelecek şekilde ayarladım. "distance" vector3'ü ile de kaleci_motion ile kaleci
         * objemin arasındaki mesafeyi kontrol ediyorum. Ayrıca "kaleci"yi kaleci_motion'un alt objesi haline getirdim.
         * Artık kaleci_motion objesine bağlı olarak ve istediğim pivot noktasında "kaleci" objesi hareket edebilecek. */
	}
	
	
	void Update () {


        float y_angle = kaleci_motion.transform.eulerAngles.y;
        if (y_angle > 180)
        {
            y_angle = y_angle - 360;
        }

        /* kaleci_motion objemi negatif açıyla döndürdüğüm zaman aldığım "rotation" değerlerde bir hata oluyordu. 
         * Bunu engellemek ve daha doğru değerler almak için yukarıdaki kod yazıldı. */ 

        if (Input.GetKey(KeyCode.Keypad3))
        {
            if(y_angle>=0 && y_angle < 90)
            {
                kaleci_motion.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
            }
            
        }

        /* Yukarıdaki "if" koduna göre klavyeden "3"e basılı tuttuğumda kaleci objem kaleci_motion pivot noktası etrafında 
         * maksimum 90 derece olacak şekilde sağa dönüyor. */
       

        

        else if (Input.GetKey(KeyCode.Keypad1))
        {
            if (y_angle<=360 && y_angle>-90)
            {
                kaleci_motion.transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime);

            }
            

        }

        /* Yukarıdaki "if" koduna göre klavyeden "1"e basılı tuttuğumda kaleci objem kaleci_motion pivot noktası etrafında 
         * maksimum 90 derece olacak şekilde sola dönüyor. */

        else
        {
            kaleci_motion.transform.eulerAngles = new Vector3(0, 0, 0);
            
        }

        // Döndürme işlemini sonlandırdığım zaman kalecimin yine orta noktada durması için yukarıdaki kod yazıldı.
        

    }
}
