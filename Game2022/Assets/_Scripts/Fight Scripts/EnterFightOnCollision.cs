using System.Collections;
using System.Collections.Generic;
using LabyrinthScripts;
using SaveScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterFightOnCollision: MonoBehaviour
{
    public int fightPrefabIndex;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        // Debug.Log("Fight is active");
        // SaveAndLoad.SaveGame();

        FightPreparation.SetFightPrefab(fightPrefabIndex);
        GameManager.Instance.state = GameState.Fight;
        SceneManager.LoadScene("FightScene");

        StartCoroutine(DestroyMonster());
    }

    private IEnumerator DestroyMonster()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}