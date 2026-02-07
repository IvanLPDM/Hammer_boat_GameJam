using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{

    public Rigidbody rb;
    public ParticleSystem particlePrefab;
    public Transform foamPoint;

    [Header("Spawn Control")]
    public float minTimeBetweenSpawns = 0.5f; // segundos
    public float speedThreshold = 2f;

    float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > speedThreshold &&
            Time.time >= lastSpawnTime + minTimeBetweenSpawns)
        {
            SpawnParticles(foamPoint);
            lastSpawnTime = Time.time;
        }
    }

    public void SpawnParticles(Transform point)
    {
        ParticleSystem ps = Instantiate(
            particlePrefab,
            point.position,
            point.rotation
        );

        ps.Play();
        Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
    }
}
