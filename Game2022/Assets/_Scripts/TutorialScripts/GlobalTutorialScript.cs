using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalTutorialScript
{
    public static bool IsMovementTutorialPlayed;
    public static bool IsInventoryTutorialPlayed;
    public static bool IsBattleTutorialPlayed;
    public static bool IsFlashlightTutorialPlayed;

    public static GameObject MovementTutorial;
    public static GameObject InventoryTutorial;
    public static GameObject BattleTutorial;
    public static GameObject FlashlightTutorial;

    public static void OpenTutorial(GameObject canvas)
    {
        Time.timeScale = 0f;
    }
}
