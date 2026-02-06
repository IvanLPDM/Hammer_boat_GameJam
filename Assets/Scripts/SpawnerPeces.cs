using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPeces : MonoBehaviour
{
    public GameObject[] peces;
    public bool isSpawning = false;
    public bool isSpawned = false;
    public float timeOfSpawn = 3f;
    public float remainTime = 0f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSpawning = true;
            remainTime = timeOfSpawn;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSpawning = false;
            isSpawned = false;
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
                int num = Random.Range(0, peces.Length - 1);
                Vector3 pos = transform.position;
                pos.x += 15; pos.z -= 15;
                //int posChange = Random.Range(0, 3);
                //switch (posChange)
                //{
                //    case 1: pos.x += 30; break;
                //    case 2: pos.z -= 30; break;
                //    case 3: pos.x += 30; pos.z -= 30; break;
                //    //default: pos.x += 30; pos.z -= 30; break;
                //}
                Instantiate(peces[num], pos, Quaternion.identity);
            }
        }
    }
}
