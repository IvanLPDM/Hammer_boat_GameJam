using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPeces : MonoBehaviour
{
    public GameObject[] peces;
    public float timeOfSpawn = 3f;
    public float spawnY = 0f;

    private float remainTime = 0f;
    private bool isSpawning = false;
    private bool isSpawned = false;
    private bool firstFish = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSpawning = true;
            remainTime = timeOfSpawn;
            SliderBarra.instance.SetTimeToFill(timeOfSpawn);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSpawning = false;
            isSpawned = false;
            SliderBarra.instance.SetTimeToFill(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning && !isSpawned)
        {
            remainTime -= Time.deltaTime;
            if (remainTime < 0)
            {
                remainTime = 0;
                isSpawned = true;
                int num = 0;
                if (firstFish) firstFish = false;
                else num = Random.Range(0, peces.Length);

                Vector3 pos = transform.position;
                pos.y = spawnY;
                pos.x += 15; pos.z -= 15; // Desde el medio
                Instantiate(peces[num], pos, Quaternion.identity);
            }
        }
    }
}
