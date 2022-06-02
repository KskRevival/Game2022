using InventoryScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public bool IsInventoryCanvas;

    public void Close()
    {
        TutorialHandler.WasInventoryOpened = IsInventoryCanvas || TutorialHandler.WasInventoryOpened;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        TutorialHandler.IsTutorialOpenned = false;
        if (IsInventoryCanvas) 
            for (var i = 0; i < 2; i++) 
                GameManager.Instance.player.GetComponentInChildren<InventoryHandler>().SwitchInventory();
    }
}
