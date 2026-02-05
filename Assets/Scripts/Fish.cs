using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed = 0.3f;
    public Vector3[] targets;
    public Vector3[][] targetsPosibles = new Vector3[][]
    {
        new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 10),
            new Vector3(5, 0, 10)
        },
        //new Vector3[]
        //{
        //    new Vector3(0, 0, 0),
        //    new Vector3(0, 0, 10),
        //    new Vector3(-40, 0, -40)
        //},
        //new Vector3[]
        //{
        //    new Vector3(0, 0, 0),
        //    new Vector3(20, 0, 0),
        //    new Vector3(40, 0, 40)
        //}
    };

    private bool final = false;
    private int actTarget = 0;
    private float time = 0;
    private float y;
    private float amplitudeFinal = 5;

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

        Vector3 target = targets[actTarget];
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f)
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
    }

    private void InitFish()
    {
        y = transform.position.y;
        int num = Random.Range(0, targetsPosibles.Length);
        targets = targetsPosibles[num];
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
