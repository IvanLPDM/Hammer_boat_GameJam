using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End : MonoBehaviour
{
    public GameObject gate1, gate2;
    public GameObject image;
    public TextMeshProUGUI textCoins;
    public float entregaTime = 1.5f;
    public int winCoins = 5;
    public float timeOfGiraje = 3f;

    public bool win = false;
    public float actTimeOfGiraje = 0f;
    private int endCoins = 0;
    private bool entregando = false;
    private Player pl = null;
    private float remainTime = 0f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pl = other.gameObject.GetComponent<Player>();
            if (pl != null && !win)
            {
                int numCoins = pl.GetNumOfCoins();
                if (numCoins > 0)
                {
                    entregando = true;
                    remainTime = entregaTime;
                    SliderBarra.instance.SetTimeToFill(entregaTime);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            entregando = false;
            remainTime = 0f;
            SliderBarra.instance.SetTimeToFill(0);
        }
    }

    private void GirarPuertas()
    {
        if (actTimeOfGiraje < timeOfGiraje)
        {
            actTimeOfGiraje += Time.deltaTime;
            float y = actTimeOfGiraje * 90 / timeOfGiraje;
            gate1.gameObject.transform.rotation = Quaternion.Euler(0, -y, 0);
            gate2.gameObject.transform.rotation = Quaternion.Euler(0, y, 0);

        }
        else actTimeOfGiraje = timeOfGiraje;

    }

    private void EntregaMonedas()
    {
        if (entregando)
        {
            if (pl == null) return;

            remainTime -= Time.deltaTime;
            if (remainTime <= 0)
            {
                pl.SubstractCoins(1);
                ++endCoins;
                textCoins.text = endCoins.ToString() + " / " + winCoins;

                if (endCoins == winCoins)
                {
                    win = true;
                    entregando = false;
                    SliderBarra.instance.SetTimeToFill(0);
                }
                else
                {
                    int numCoins = pl.GetNumOfCoins();
                    if (numCoins > 0)
                    {
                        remainTime = entregaTime;
                        SliderBarra.instance.SetTimeToFill(entregaTime);
                    }
                    else
                    {
                        entregando = false;
                    }
                }
            }
        }
    }

    private void RotarUI()
    {
        Vector3 direction = Camera.main.transform.position - image.gameObject.transform.position;
        direction.y = 0f;

        image.gameObject.transform.rotation = Quaternion.LookRotation(-direction);
    }

    // Start is called before the first frame update
    void Awake()
    {
        pl = FindObjectOfType<Player>();
        textCoins.text = "0 / " + winCoins;
        actTimeOfGiraje = 0f;
    }
    void LateUpdate()
    {
        if (win) GirarPuertas();
        EntregaMonedas();
        RotarUI();
    }
}
