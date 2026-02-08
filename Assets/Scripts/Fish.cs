using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fish : MonoBehaviour
{
    public int fishSpawnerType = 1;
    public int fishType = 1;
    public float timeInvulnerable = 1f;
    public float speed = 0.3f, zigzagSpeed = 8f, zigzagAmplitude = 1f;
    public Vector3[] targets;
    public Vector3[][] targetsPosibles1 = new Vector3[][]
    {
        new Vector3[]
        {
            new Vector3(236, 0, 123),
            new Vector3(137, 0, 240),
            new Vector3(10, 0, 193),
            new Vector3(-82, 0, 334),
            new Vector3(22, 0, 406),
            new Vector3(176, 0, 405),
        },
        new Vector3[]
        {
            new Vector3(492, 0, 10),
            new Vector3(584, 0, 98),
            new Vector3(435, 0, 181),
            new Vector3(613, 0, 353),
            new Vector3(489, 0, 443),
            new Vector3(369, 0, 316)
        }
    };
    public Vector3[][] targetsPosibles2 = new Vector3[][]
    {
        new Vector3[]
        {
            new Vector3(202, 0, -354),
            new Vector3(413, 0, -276),
            new Vector3(498, 0, -459),
            new Vector3(611, 0, -530),
            new Vector3(487, 0, -685),
            new Vector3(623, 0, -802),
            new Vector3(508, 0, -978),
            new Vector3(313, 0, -1014),
        },
        new Vector3[]
        {
            new Vector3(156, 0, -704),
            new Vector3(270, 0, -840),
            new Vector3(107, 0, -936),
            new Vector3(-41, 0, -908),
            new Vector3(-145, 0, -806),
            new Vector3(-14, 0, -736),
            new Vector3(-182, 0, -642),
        }
    };

    private bool final = false;
    private int actTarget = 0;
    private float time = 0;
    private float y;
    private float amplitudeFinal = 5;
    public bool invulnerable = true;
    public float timeInvulenarbleAct = 0;

    public int GetFishType() { return fishType; }
    public bool GetInvulnerability() { return invulnerable; }

    private void FishBehaviour()
    {
        if (final)
        {
            time += Time.deltaTime;

            if (time <= 0.3333333f)
            {
                Vector3 dir = new Vector3(targets[actTarget - 1].x, amplitudeFinal, targets[actTarget - 1].z) - transform.position;
                transform.rotation = Quaternion.LookRotation(dir);
            }
            else
            {
                Vector3 dir = new Vector3(targets[actTarget - 1].x, -amplitudeFinal, targets[actTarget - 1].z) - transform.position;
                transform.rotation = Quaternion.LookRotation(dir);
            }

            transform.position = new Vector3(targets[actTarget-1].x, Mathf.Sin(time * 3 * Mathf.PI / 2) * amplitudeFinal, targets[actTarget-1].z);
            if (time >= 1) Destroy(this.gameObject);
            return;
        }

        //Mientras no llegue al final, que siga los targets

        Vector3 target = targets[actTarget];
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        Vector3 ddir = (target - transform.position).normalized;

        // Perpendicular
        Vector3 side = Vector3.Cross(Vector3.up, ddir);
        float zigzag = Mathf.Sin(Time.time * zigzagSpeed) * zigzagAmplitude;
        Vector3 finalDir = (ddir + side * zigzag).normalized;
        transform.position += finalDir * speed * Time.deltaTime;


        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            actTarget++;

            if (actTarget >= targets.Length)
            {
                final = true;
            }
            else
            {
                targets[actTarget].y = y;
                Vector3 dir = targets[actTarget] - transform.position;
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }

        if (invulnerable)
        {
            timeInvulenarbleAct -= Time.deltaTime;
            if (timeInvulenarbleAct < 0)
            {
                timeInvulenarbleAct = 0;
                invulnerable = false;
            }
        }
    }

    private void InitFish()
    {
        invulnerable = true;
        timeInvulenarbleAct = timeInvulnerable;
        y = transform.position.y;
        if (fishSpawnerType == 1)
        {
            int num = Random.Range(0, targetsPosibles1.Length);
            targets = targetsPosibles1[num];
        }
        else if (fishSpawnerType == 2)
        {
            int num = Random.Range(0, targetsPosibles2.Length);
            targets = targetsPosibles2[num];
        }

        targets[0].y = y;
        Vector3 dir = targets[0] - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    void Awake()
    {
        InitFish();
    }

    void Update()
    {
        FishBehaviour();
    }
}
