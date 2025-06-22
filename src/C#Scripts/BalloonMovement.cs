using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




public class BalloonMovement : MonoBehaviour
{
    public float speed = 2f;
    public Manager manager;  // Manager script referansý**
    public GameObject popEffectPrefab;

    public UnityEvent OnBalloonPopped;  // Balon patlayýnca tetiklenecek event

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        Vector3 screenTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (transform.position.y > screenTop.y)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        manager.GenerateOperation();  
        //Destroy(gameObject);           // Balonu yok et**
        PopBalloon(); //önce efekt çalýþsýn
    }

    void PopBalloon()
    {
        // Buraya patlama efekti veya ses koyabiliriz
        if (popEffectPrefab != null)
        {
            Vector3 effectPosition = new Vector3(transform.position.x, transform.position.y, 0f); //effectPosition tanýmlandý.
            Instantiate(popEffectPrefab, transform.position, Quaternion.identity);
        }

        if (OnBalloonPopped != null)
            OnBalloonPopped.Invoke();
        //..
        if (manager != null && manager.IsReadyForNewOperation())  // Kontrol fonksiyonu ekleyeceðiz
        {
            manager.GenerateOperation();
            manager.MarkOperationStarted(); // Yeni iþlem baþladýðýný bildir
        }

        Destroy(gameObject);
    }
}
