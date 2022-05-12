using System.Collections;
using System.Collections.Generic;
using LabyrinthScripts;
using SaveScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterFightOnCollision: MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Debug.Log("Fight is active");
        // SaveAndLoad.SaveGame();
        GameManager.Instance.State = GameState.Fight;
        SceneManager.LoadScene("FightScene");
    }
}