using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   public void OnQuitButtonPress()
    {
        print("game quit");
        Application.Quit();
    }

    public void OnHelpButtonPress()
    {
        SceneManager.LoadScene("Help");
    }

    public void OnStartButtonPress()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnMenuButtonPress()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
