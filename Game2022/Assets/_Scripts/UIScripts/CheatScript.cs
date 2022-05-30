using System.Linq;
using RoomGeneration;
using SaveScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class CheatScript : MonoBehaviour
    {
        public GameObject cheatMenuUI;

        public static Spawnable type;
        public TMP_Dropdown typeDropdown;

        public static int index;

        public TMP_Dropdown[] indexDropdowns =
        {
        };

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Backspace)) return;
            Debug.Log("Cheats");
            if (PauseScript.IsPaused) Resume();
            else Pause();
        }

        public void Resume()
        {
            PauseScript.IsPaused = false;
            Time.timeScale = 1f;
            cheatMenuUI.SetActive(PauseScript.IsPaused);
        }

        private void Pause()
        {
            PauseScript.IsPaused = true;
            Time.timeScale = 0f;
            cheatMenuUI.SetActive(PauseScript.IsPaused);
        }

        public void DestroyMonsters()
        {
            GameManager.Instance.DestroyMonsters();
        }

        public void TeleportToTheEnd()
        {
            var data = GameManager.Instance.data;
            GameManager.Instance.player.gameObject.transform.position =
                new Vector3(
                    (data.columns - 1) * data.offset.x + 3,
                    -(data.rows - 1) * data.offset.y + 3,
                    0);
        }

        public void SpawnItem()
        {
            if (type == Spawnable.Empty) return;
            var itemData = new ItemData {itemSpawnIndex = index, type = type};
            var player = GameManager.Instance.player;
            var dropPos = GameObject
                .FindGameObjectsWithTag("Waypoint")
                .Select(waypoint => waypoint.transform.position - player.transform.position)
                .OrderBy(vectorToWaypoint => vectorToWaypoint.magnitude)
                .First().normalized * 1.7f + player.transform.position;

            if (type == Spawnable.Monster)
            {
                Instantiate(
                    Restorer.RestoreMonster(),
                    dropPos,
                    Quaternion.identity,
                    GameManager.Instance.monsterContainer.transform);
            }
            else
            {
                Instantiate(
                    Restorer.RestoreItem(itemData),
                    dropPos,
                    Quaternion.identity,
                    GameManager.Instance.lootContainer.transform);
            }
        }

        public void UpdateType()
        {
            indexDropdowns[(int) type].gameObject.SetActive(false);
            type = (Spawnable) typeDropdown.value;
            indexDropdowns[(int) type].gameObject.SetActive(true);
            index = 0;
        }

        public void UpdateIndex()
        {
            index = indexDropdowns[(int) type].value;
        }
    }
}