using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBarra : MonoBehaviour
{
    public RectTransform flecha, flechaAux;
    public GameObject idlePos, golpePos, flechaAuxObj;
    public float initY = 0, finalY = 0;
    private bool dashed = false;
    private Player pl;

    void Start()
    {
        initY = flecha.anchoredPosition.y;
        finalY = flechaAux.anchoredPosition.y;

        flechaAuxObj.SetActive(false);
        idlePos.SetActive(true);
        golpePos.SetActive(false);
        
        pl = FindObjectOfType<Player>();
    }

    void Update()
    {
        float dashTime = pl.GetDashTime();
        bool dashing = pl.GetIsDashing();
        if (!dashing)
        {
            if (dashed)
            {
                idlePos.SetActive(true);
                golpePos.SetActive(false);
                dashed = false;
            }
            float newY = initY - dashTime * (initY - finalY);
            flecha.anchoredPosition = new Vector2(flecha.anchoredPosition.x, newY);
        }
        else
        {
            if (!dashed)
            {
                golpePos.SetActive(true);
                idlePos.SetActive(false);
                dashed = true;
            }
        }
    }
}
