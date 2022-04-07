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
}
