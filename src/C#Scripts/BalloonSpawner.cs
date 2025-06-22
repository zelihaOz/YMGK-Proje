using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject[] balloonPrefabs; // Balon prefablar� dizisi
    public float spawnInterval = 3f;  // Balonlar�n ��kma aral��� (saniye)
    public float minX = -15f;            // Spawn X pozisyonunun min s�n�r�
    public float maxX = 15f;             // Spawn X pozisyonunun max s�n�r�
    public float spawnY = -10f;          // Spawn Y pozisyonu (ekran�n alt�)

    private float timer = 0f;
    public Manager manager;  // Inspector'dan ba�layaca��z**

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

        // Instantiate edilen objeyi balloon de�i�kenine at�yoruz
        GameObject balloon = Instantiate(balloonPrefabs[randomIndex], spawnPosition, Quaternion.identity);

        BalloonMovement bm = balloon.GetComponent<BalloonMovement>();
        bm.manager = manager;  // Manager script referans�n� ver
    }
}
