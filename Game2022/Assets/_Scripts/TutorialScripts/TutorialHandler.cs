using InventoryScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    public GameObject MovementTutorial;
    public GameObject InventoryTutorial;
    public GameObject FlashlightTutorial;
    public static bool WasInventoryOpened;
    public static bool IsTutorialOpenned;

    void Start()
    {
        if (GameManager.Instance.level == 1) MovementTutorial.SetActive(true);
        if (GameManager.Instance.level == 2) FlashlightTutorial.SetActive(true);
        IsTutorialOpenned = true;
        WasInventoryOpened = GameManager.Instance.level != 1;
    }

    void Update()
    {
        InventoryTutorial.SetActive(!WasInventoryOpened && GameManager.Instance.level == 1 && InventoryHandler.IsInventoryActive);
    }
}
