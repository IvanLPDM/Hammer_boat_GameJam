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

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularSpeed;
    }



    void FixedUpdate()
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
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(Vector3.up * turnTorque, ForceMode.Force);
        }

        // Limitar velocidad lineal
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
