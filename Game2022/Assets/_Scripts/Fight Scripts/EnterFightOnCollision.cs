using PlayerScripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fight_Scripts
{
    public class EnterFightOnCollision: MonoBehaviour
    {
        public int fightPrefabIndex;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player") || GameManager.Instance.player.health <= 0) return;

            // Debug.Log("Fight is active");
            // SaveAndLoad.SaveGame();

            GameManager.Instance.player.md.animator.SetFloat(MovementData.Horizontal, 0);
            GameManager.Instance.player.md.animator.SetFloat(MovementData.Vertical, 0);
            GameManager.Instance.player.md.animator.SetFloat(MovementData.Speed, 0);
            GameManager.Instance.player.md.movement = Vector2.zero;

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
}