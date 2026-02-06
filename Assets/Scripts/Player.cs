using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float forwardForce = 20f;
    public float maxSpeed = 10f;
    public float forceMinusFishes = 4f;

    [Header("Turning")]
    public float turnTorque = 5f;
    public float maxAngularSpeed = 1.5f;
    public float angularDamping = 10f;

    [Header("Dash")]
    public float dashStatus = 0f;
    public float dashSpeedMax = 50f;
    public bool statusUp = true;
    public float speedStatus = 0.2f;
    public bool dashing = false;
    public float dashTime = 0f;
    public float dashMaxTime = 2f;
    public float dashTimeMinusFishes = 0.2f;

    [Header("Fish")]
    public GameObject fish = null;
    public bool fishing = false;
    public int numOfFishes = 0, maxFishes = 3;
    public GameObject visualFish1 = null, visualFish2 = null, visualFish3 = null;
    private Rigidbody rb;
    private Fish fsAct = null;

    [Header("Floaters")]
    public Floater1 floater1 = null;
    public Floater1 floater2 = null, floater3 = null, floater4 = null;
    public float dragMinusFishes = 0.2f;

    public int GetNumOfFishes() { return numOfFishes; }
    
    public void EntregaPez()
    {
        numOfFishes--;
        FishesFished();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            fsAct = other.gameObject.GetComponent<Fish>();
            fish = other.gameObject;
            fishing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            fish = null;
            fishing = false;
        }
    }

    // Siempre se llama esta funcion cuando cambia el numero de peces
    private void FishesFished()
    {
        bool f1 = false, f2 = false, f3 = false;
        switch (numOfFishes)
        {
            case 1: f1 = true; break;
            case 2: f1 = true; f2 = true; break;
            case 3: f1 = true; f2 = true; f3 = true; break;
        }

        visualFish1.gameObject.SetActive(f1);
        visualFish2.gameObject.SetActive(f2);
        visualFish3.gameObject.SetActive(f3);

        floater1.ChangeWaterDrag(dragMinusFishes * numOfFishes);
        floater2.ChangeWaterDrag(dragMinusFishes * numOfFishes);
        floater3.ChangeWaterDrag(dragMinusFishes * numOfFishes);
        floater4.ChangeWaterDrag(dragMinusFishes * numOfFishes);
    }

    private void Pescar()
    {
        if (Input.GetMouseButtonDown(1) && fishing && numOfFishes < maxFishes)
        {
            if (fsAct != null)
            {
                bool inv = fsAct.GetInvulnerability();
                if (!inv)
                {
                    fish.gameObject.SetActive(false);
                    fishing = false;
                    numOfFishes++;
                    FishesFished();
                }
            }

        }
    }

    private void Movement()
    {
        float force = forwardForce - forceMinusFishes * numOfFishes;
        // Avanzar
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * forwardForce, ForceMode.Force);
        }

        // Girar
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(Vector3.up * -turnTorque, ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(Vector3.up * turnTorque, ForceMode.Force);
        }
        else
        {
            rb.angularVelocity = Vector3.Lerp(
                rb.angularVelocity,
                Vector3.zero,
                angularDamping * Time.fixedDeltaTime
            );
        }

        // Limitar velocidad angular máxima
        if (rb.angularVelocity.magnitude > maxAngularSpeed)
        {
            rb.angularVelocity = rb.angularVelocity.normalized * maxAngularSpeed;
        }

        //if (!dashing)
        //{
            // Limitar velocidad lineal
            //if (rb.velocity.magnitude > maxSpeed)
            //{

            //    Vector3 excess = rb.velocity.normalized * (rb.velocity.magnitude - maxSpeed);
            //    rb.AddForce(-excess * 0.1f, ForceMode.VelocityChange);
            //    //rb.velocity = Vector3.Lerp(rb.velocity, rb.velocity.normalized * maxSpeed, Time.deltaTime * 5f);
            //}
        //}
    }

    private void Dash()
    {
        if (Input.GetMouseButtonDown(0) && !dashing)
        {
            dashing = true;
            dashTime = dashStatus * dashMaxTime;
            dashTime -= dashTimeMinusFishes;
            if (dashTime < 0) dashTime = 0;
        }

        float time = Time.deltaTime * speedStatus;
        if (!dashing)
        {
            if (statusUp)
            {
                dashStatus += time;
                if (dashStatus >= 1)
                {
                    statusUp = false;
                }
            }
            else
            {
                dashStatus -= time;
                if (dashStatus <= 0)
                {
                    statusUp = true;
                }
            }
        }
        else
        {
            rb.AddForce(transform.forward * dashSpeedMax * dashStatus, ForceMode.Force);
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                dashTime = 0;
                dashing = false;
                dashStatus = 0f;
                statusUp = true;
            }
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
        Pescar();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularSpeed;
        FishesFished();
    }
}
