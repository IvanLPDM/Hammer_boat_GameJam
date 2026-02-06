using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBarra : MonoBehaviour
{
    public static SliderBarra instance;
    public GameObject verde, rojo;
    public float timeToFill = 0; // Si es > 0 entonces se está llenando la barra
    public float timeFill = 0;
    private RectTransform rtVerde;
    private float anchor = 0;

    public void SetTimeToFill(float time) // Disparador para que empiece la barra
    {
        if (time > 0)
        {
            timeToFill = time;
            timeFill = 0;
            SetObjects(true);
            rtVerde.sizeDelta = new Vector2(0, rtVerde.sizeDelta.y);
        }
        else
        {
            SetObjects(false);
            timeToFill = 0;
        }
    }

    void Awake()
    {
        rtVerde = verde.GetComponent<RectTransform>();
        anchor = rtVerde.sizeDelta.x;
        SetObjects(false);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToFill > 0)
        {
            timeFill += Time.deltaTime;
            rtVerde.sizeDelta = new Vector2(timeFill * anchor / timeToFill, rtVerde.sizeDelta.y);
            if (timeFill >= timeToFill)
            {
                SetObjects(false);
                timeToFill = 0;
                timeFill = 0;
            }
        }
    }

    private void SetObjects(bool active)
    {
        verde.gameObject.SetActive(active);
        rojo.gameObject.SetActive(active);
    }
}
