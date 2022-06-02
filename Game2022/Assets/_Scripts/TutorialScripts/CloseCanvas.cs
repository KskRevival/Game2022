using InventoryScripts;
using System.Collections;
using System.Collections.Generic;
using UIScripts;
using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        TutorialHandler.IsTutorialOpened = false;
        GameObject.FindWithTag("Pause").GetComponent<PauseScript>().Resume();
    }

    public void OpenNext()
    {
        gameObject.SetActive(false);
        TutorialHandler.InventoryTutorial.SetActive(true);
    }
}
