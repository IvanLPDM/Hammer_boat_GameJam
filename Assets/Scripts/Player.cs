using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float forwardForce = 20f;
    public float maxSpeed = 10f;

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

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularSpeed;
    }

    private void Movement()
    {
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

        if (!dashing)
        {
            // Limitar velocidad lineal
            //if (rb.velocity.magnitude > maxSpeed)
            //{

            //    Vector3 excess = rb.velocity.normalized * (rb.velocity.magnitude - maxSpeed);
            //    rb.AddForce(-excess * 0.1f, ForceMode.VelocityChange);
            //    //rb.velocity = Vector3.Lerp(rb.velocity, rb.velocity.normalized * maxSpeed, Time.deltaTime * 5f);
            //}
        }
    }

    private void Dash()
    {
        if (Input.GetMouseButtonDown(0) && !dashing)
        {
            dashing = true;
            dashTime = dashStatus * dashMaxTime;
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dash();

    }
}
