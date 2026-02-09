using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ItemsUI : MonoBehaviour
{
    public TextMeshProUGUI coinsT, fishesT;
    public GameObject cositas, menu;
    private bool toggleMenu = false;

    public void ActualizeUI(int numFishes, int numCoins)
    {
        fishesT.text = numFishes.ToString() + "/3";
        coinsT.text = numCoins.ToString();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        if (Input.anyKey)
        {
            cositas.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            toggleMenu = !toggleMenu;
            menu.SetActive(toggleMenu);
        }
    }

    void Awake()
    {
        cositas.SetActive(true);
        toggleMenu = false;
        menu.SetActive(false);
    }
}
