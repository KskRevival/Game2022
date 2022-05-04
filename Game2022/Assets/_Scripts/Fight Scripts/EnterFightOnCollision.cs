using System.Collections;
using System.Collections.Generic;
using SaveScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterFightOnCollision: MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Fight is active");
            SaveAndLoad.SaveGame();
            SceneManager.LoadScene("FightScene");
        }
    }
}