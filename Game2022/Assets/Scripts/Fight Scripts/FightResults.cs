using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightResults : MonoBehaviour
{
    public void FightWin()
    {
        ExitScript.ToGame();
        Destroy(GameObject.Find("Monster"));
        SaveAndLoad.LoadGame();
    }

    public void FightDefeat()
    {
        ExitScript.Death();
    }
}
