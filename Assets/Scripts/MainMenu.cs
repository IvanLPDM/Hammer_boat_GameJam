using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, creditsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void Start()
    {
        GoToMainMenu();
    }
}
