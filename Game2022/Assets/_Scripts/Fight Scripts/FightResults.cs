using System.Collections;
using System.Collections.Generic;
using System.Threading;
using SaveScripts;
using UIScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightResults : MonoBehaviour
{
    public void FightWin()
    {
        TransitionScript.ToGame();
        //while (!SceneManager.sceneLoaded)
        //{
        //    Thread.Sleep(100);
        //}
        Destroy(GameObject.Find("Monster"));
        SaveAndLoad.LoadGame();
    }

    public void FightDefeat()
    {
        TransitionScript.Death();
    }
}
