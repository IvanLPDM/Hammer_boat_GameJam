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
    public float[] dashesSecs = { 0.36f, 0.77f, 0.92f }; // 0.0 - 0.36 - 0.77 - 0.92 - 1.0

    [Header("Floaters")]
    public Floater1 floater1 = null;
    public Floater1 floater2 = null, floater3 = null, floater4 = null;
    public float dragMinusFishes = 0.2f;

    [Header("Fish")]
    public GameObject fish = null;
    public bool fishing = false;
    public int numOfFishes = 0, maxFishes = 3;
    private int[] fishes = { -1, -1, -1 };
    public GameObject visualFish1 = null, visualFish2 = null, visualFish3 = null;
    public GameObject visualFish1G = null, visualFish2G = null, visualFish3G = null;
    private Rigidbody rb;
    private Fish fsAct = null;

    [Header("Coins")]
    public int numCoins = 0;

    [Header("VFX")]
    public ParticleSystem foamParticles;
    public float minSpeedToEmit = 2f;
    public float dashParticleDuration;

    public ParticleSystem fail_dash;
    public ParticleSystem green_dash;
    public ParticleSystem blue_dash;
    public ParticleSystem purple_dash;

    private MusicManager musicManager;
    private ItemsUI itemsUI;

    public bool GetIsDashing() { return dashing; }
    public float GetDashTime() {  return dashStatus; }
    public int GetNumOfFishes() { return numOfFishes; }
    
    public void EntregaPez()
    {
        numOfFishes--;
        numCoins += ((fishes[numOfFishes] == 2) ? 3 : 1);
        fishes[numOfFishes] = -1;
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
        //Solucion ultra fea pero funcional
        bool f1 = false, f2 = false, f3 = false;
        bool f1G = false, f2G = false, f3G = false;

        if (fishes[0] == 1) { f1 = true; }
        else if (fishes[0] == 2) { f1G = true; }

        if (fishes[1] == 1) { f2 = true; }
        else if (fishes[1] == 2) { f2G = true; }

        if (fishes[2] == 1) { f3 = true; }
        else if (fishes[2] == 2) { f3G = true; }

        visualFish1.gameObject.SetActive(f1);
        visualFish2.gameObject.SetActive(f2);
        visualFish3.gameObject.SetActive(f3);

        visualFish1G.gameObject.SetActive(f1G);
        visualFish2G.gameObject.SetActive(f2G);
        visualFish3G.gameObject.SetActive(f3G);

        floater1.ChangeWaterDrag(dragMinusFishes * numOfFishes);
        floater2.ChangeWaterDrag(dragMinusFishes * numOfFishes);
        floater3.ChangeWaterDrag(dragMinusFishes * numOfFishes);
        floater4.ChangeWaterDrag(dragMinusFishes * numOfFishes);

        itemsUI.ActualizeUI(numOfFishes, numCoins);
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
                    int type = fsAct.GetFishType();
                    fishes[numOfFishes] = type;

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
            //for (int i = dashesSecs.Length - 1; i >= 0; i--)
            //{
            //    float actDash = dashesSecs[i];
            //    if (dashStatus < actDash)
            //    {
            //        if (dashesSecs )
            //        break;
            //    }
            //}
            if (dashStatus > dashesSecs[dashesSecs.Length - 1]) musicManager.PlayDash();
            else musicManager.PlayHammer();

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

            if (dashTime < 2.0f)
            {
                StartCoroutine(PlayForOneSecond(green_dash));
            }
            if (dashTime > 2.0f && dashTime < 3.0f)
            {
                StartCoroutine(PlayForOneSecond(green_dash));
            }
            else if (dashTime >= 3.0f && dashTime < 4.0f)
            {
                StartCoroutine(PlayForOneSecond(blue_dash));
            }
            else if (dashTime >= 4.0f)
            {
                StartCoroutine(PlayForOneSecond(purple_dash));
            }

            if (dashTime <= 0)
            {
                dashTime = 0;
                dashing = false;
                dashStatus = 0f;
                statusUp = true;

                
            }

            
        }
    }

    IEnumerator PlayForOneSecond(ParticleSystem ps)
    {
        ps.Play();
        yield return new WaitForSeconds(dashParticleDuration);
        ps.Stop();
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


        //Trail SFX
        if (rb.velocity.magnitude >= minSpeedToEmit)
        {
            if (!foamParticles.isPlaying)
                foamParticles.Play();
        }
        else
        {
            if (foamParticles.isPlaying)
                foamParticles.Stop();
        }
    }

    void Awake()
    {
        green_dash.Stop();
        fail_dash.Stop();
        blue_dash.Stop();
        purple_dash.Stop();

        musicManager = FindObjectOfType<MusicManager>();
        itemsUI = FindObjectOfType<ItemsUI>();

        fishes = new int[] { -1, -1, -1 };
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularSpeed;
        FishesFished();
    }
}
