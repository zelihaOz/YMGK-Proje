using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject[] balloonPrefabs; // Balon prefablarý dizisi
    public float spawnInterval = 3f;  // Balonlarýn çýkma aralýðý (saniye)
    public float minX = -15f;            // Spawn X pozisyonunun min sýnýrý
    public float maxX = 15f;             // Spawn X pozisyonunun max sýnýrý
    public float spawnY = -10f;          // Spawn Y pozisyonu (ekranýn altý)

    private float timer = 0f;
    public Manager manager;  // Inspector'dan baðlayacaðýz**

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnBalloon();
            timer = 0f;
        }
    }

    void SpawnBalloon()
    {
        int randomIndex = Random.Range(0, balloonPrefabs.Length);
        float randomX = Random.Range(minX, maxX); // x ekseni rastgele
        Vector3 spawnPosition = new Vector3(randomX, spawnY, -1f);

        // Instantiate edilen objeyi balloon deðiþkenine atýyoruz
        GameObject balloon = Instantiate(balloonPrefabs[randomIndex], spawnPosition, Quaternion.identity);

        BalloonMovement bm = balloon.GetComponent<BalloonMovement>();
        bm.manager = manager;  // Manager script referansýný ver
    }
}
