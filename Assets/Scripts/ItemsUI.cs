using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemsUI : MonoBehaviour
{
    public TextMeshProUGUI coinsT, fishesT;
    public GameObject cositas;

    public void ActualizeUI(int numFishes, int numCoins)
    {
        fishesT.text = numFishes.ToString() + "/3";
        coinsT.text = numCoins.ToString();
    }

    void Update()
    {
        if (Input.anyKey)
        {
            cositas.SetActive(false);
        }
    }

    void Awake()
    {
        cositas.SetActive(true);
    }
}
