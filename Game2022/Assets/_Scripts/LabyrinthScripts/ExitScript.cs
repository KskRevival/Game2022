using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        var level = GameManager.Instance.level;
        Destroy(GameManager.Instance);
        SceneManager.LoadScene(level + 2);
    }
}
