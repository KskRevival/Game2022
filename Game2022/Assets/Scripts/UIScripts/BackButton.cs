using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
   public void OnClick()
    {
        Debug.Log("BackButton pushed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Retry()
    {
        Debug.Log("Return to Game");
        SceneManager.LoadScene("Game");
    }

    public void ToMenu()
    {
        Debug.Log("Return to menu");
        SceneManager.LoadScene(0);
    }
}
