using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RoomGeneration;
using SaveScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveScripts.OnBoardObject;

public class ExitScript : MonoBehaviour
{
    private GameObject manager;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        var level = GameManager.Instance.level;
        SaveAndLoad.SaveGame();
        Destroy(GameManager.Instance.gameObject);
        RoomGenerator.roomsCreated = 0;
        SceneManager.LoadScene(level + 2);
    }
}
