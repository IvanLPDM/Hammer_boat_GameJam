using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fish : MonoBehaviour
{
    public float timeInvulnerable = 1f;
    public float speed = 0.3f, zigzagSpeed = 8f, zigzagAmplitude = 1f;
    public Vector3[] targets;
    public Vector3[][] targetsPosibles = new Vector3[][]
    {
        new Vector3[]
        {
            new Vector3(100, 0, 350),
            new Vector3(350, 0, 350),
            new Vector3(400, 0, 400)
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
    public bool invulnerable = true;
    public float timeInvulenarbleAct = 0;
    
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
