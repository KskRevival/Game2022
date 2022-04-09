using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{ 
    public static void Back()
    {
        Debug.Log("BackButton pushed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static void ToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public static void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
