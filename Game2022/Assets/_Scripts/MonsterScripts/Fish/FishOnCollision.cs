using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.player.md.animator.SetFloat(MovementData.Horizontal, 0);
            GameManager.Instance.player.md.animator.SetFloat(MovementData.Vertical, 0);
            GameManager.Instance.player.md.animator.SetFloat(MovementData.Speed, 0);
            GameManager.Instance.player.md.movement = Vector2.zero;

            GameManager.Instance.player.health = 0;

            SceneManager.LoadScene("DeathScene");
        }
    }
}
