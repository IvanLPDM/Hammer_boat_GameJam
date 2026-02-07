using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater1 : MonoBehaviour
{
    public Rigidbody rb;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public float gravity = 9.8f;
    public int floaterCount = 4;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;
    public float debug = -500;
    public float waterDragAct = 0.99f;
    public float gravityMult = 1f;
    public float maxForceUp = 20f;

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / floaterCount * gravityMult, transform.position, ForceMode.Acceleration);
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x, transform.position.z);
        debug = waveHeight;
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            float displacement = Mathf.Abs(Physics.gravity.y) * displacementMultiplier;
            if (displacement > maxForceUp) displacement = maxForceUp;

            rb.AddForceAtPosition(new Vector3(0f, displacement, 0f), transform.position, ForceMode.Acceleration);
            rb.AddForce(displacementMultiplier * -rb.velocity * waterDragAct * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity* waterDragAct * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    public void ChangeWaterDrag(float dragChange)
    {
        waterDragAct = waterDrag + dragChange;
    }

    void Awake()
    {
        waterDragAct = waterDrag;
    }
}
