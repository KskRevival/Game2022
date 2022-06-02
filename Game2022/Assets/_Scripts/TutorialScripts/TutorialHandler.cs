using InventoryScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    public static GameObject MovementTutorial;
    public static GameObject InventoryTutorial;
    public static GameObject FlashlightTutorial;
    public GameObject MT;
    public GameObject IT;
    public GameObject FT;
    public static bool WasInventoryOpened;
    public static bool IsTutorialOpened;

    void Start()
    {
        MovementTutorial = MT;
        InventoryTutorial = IT;
        if (GameManager.Instance.level == 1) MovementTutorial.SetActive(true);
        //if (GameManager.Instance.level == 2) FlashlightTutorial.SetActive(true);
        IsTutorialOpened = true;
        WasInventoryOpened = GameManager.Instance.level != 1;
    }
}
