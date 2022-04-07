using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
   public void PlayButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
