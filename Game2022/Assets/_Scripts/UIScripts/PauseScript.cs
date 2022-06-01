using System;
using System.Linq;
using LabyrinthScripts;
using PlayerScripts;
using SaveScripts;
using UIScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveScripts.OnBoardObject;

namespace UIScripts
{
    public class PauseScript : MonoBehaviour
    {
        public static bool IsPaused;
        public GameObject pauseMenuUI;

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            Debug.Log("Escape");
            if (IsPaused) Resume();
            else Pause();
        }

        public void Resume()
        {
            IsPaused = false;
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(IsPaused);
        }

        private void Pause()
        {
            IsPaused = true;
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(IsPaused);
        }

        public void Save()
        {
            SaveAndLoad.SaveGame();
        }

        public void Load()
        {
            var data = SaveAndLoad.LoadGame();
            if (data == null || data.level != GameManager.Instance.level) return;
            
            AmmoCounter.AmmoCount = data.ammo;

            #region load player

            var player = GameManager.Instance.player;
            player.gameObject.transform.position = PositionToVector2(data.playerData.position);
            player.id.items =
                data.playerData.id
                    //.Where(id => id.itemData != null)
                    .Select(x =>
                    {
                        var restored = Restorer.RestoreInventoryItem(x);
                        if (restored == null) return null;
                        return Instantiate(
                            restored,
                            new Vector3(-999, 999, -999),
                            Quaternion.identity,
                            GameManager.Instance.dropContainer.transform);
                    })
                    .ToArray();
            player.health = data.playerData.health;
            player.maxHealth = data.playerData.maxHealth;

            #endregion
            
            if (data.level == 3) return;
            
            #region load monsters

            GameManager.Instance.DestroyMonsters();
            var monsters = data.monsterData;
            foreach (var monster in monsters)
            {
                var m = Restorer.RestoreMonster(monster);
                Instantiate(
                    m,
                    PositionToVector2(monster.position),
                    Quaternion.identity,
                    GameManager.Instance.monsterContainer.transform);
            }

            #endregion

            #region load loot

            GameManager.Instance.DestroyLoot();
            var loot = data.lootData;
            foreach (var item in loot)
            {
                Instantiate(
                    Restorer.RestoreItem(item.itemData),
                    PositionToVector2(item.position),
                    Quaternion.identity,
                    GameManager.Instance.lootContainer.transform);
            }

            #endregion
        }

        public void ToMenu()
        {
            TransitionScript.ToMenu();
        }

        public void Exit()
        {
            TransitionScript.Exit();
        }
    }
}