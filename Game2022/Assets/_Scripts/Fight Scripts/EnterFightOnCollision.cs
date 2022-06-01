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
            if (!collision.gameObject.CompareTag("Player")) return;

            // Debug.Log("Fight is active");
            // SaveAndLoad.SaveGame();

            FightPreparation.SetFightPrefab(fightPrefabIndex);
            GameManager.Instance.state = GameState.Fight;
            FightPreparation.Camera = GameObject.FindGameObjectWithTag("MainCamera");
            SceneManager.LoadScene("FightScene");

            StartCoroutine(DestroyMonsterAndOffCamera());
        }

        private IEnumerator DestroyMonsterAndOffCamera()
        {
            yield return new WaitForSeconds(0.01f);

            FightPreparation.Camera.SetActive(false);
            Destroy(gameObject);
        }
    }
}