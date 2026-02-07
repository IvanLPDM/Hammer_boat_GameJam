using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntregaPeces : MonoBehaviour
{
    public bool entregando = false;
    public float entregaTime = 1.5f;

    private Player pl = null;
    private float remainTime = 0f;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pl = other.gameObject.GetComponent<Player>();
            if (pl != null)
            {
                int numFishes = pl.GetNumOfFishes();
                if (numFishes > 0)
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (entregando)
        {
            if (pl == null) return;

            remainTime -= Time.deltaTime;
            if (remainTime <= 0)
            {
                pl.EntregaPez();
                int numFishes = pl.GetNumOfFishes();
                if (numFishes > 0)
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
