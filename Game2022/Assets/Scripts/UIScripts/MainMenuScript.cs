using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
   public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
