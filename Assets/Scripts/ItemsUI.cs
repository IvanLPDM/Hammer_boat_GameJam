using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemsUI : MonoBehaviour
{
    public TextMeshProUGUI coinsT, fishesT;

    public void ActualizeUI(int numFishes, int numCoins)
    {
        fishesT.text = numFishes.ToString() + "/3";
        coinsT.text = numCoins.ToString();
    }
}
