using System;
using LabyrinthScripts;
using PlayerScripts;
using SaveScripts;
using UIScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class PauseScript : MonoBehaviour
    {
        private static bool isPaused;
        public GameObject pauseMenuUI;

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            Debug.Log("Escape");
            if (isPaused) Resume();
            else Pause();
        }

        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(isPaused);
        }

        private void Pause()
        {
            isPaused = true;
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(isPaused);
        }

        public void Save()
        {
            SaveAndLoad.SaveGame();
        }

        public void Load()
        {
            var data = SaveAndLoad.LoadGame();
            var player = GameManager.Instance.player;
            player.player.transform.position = OnBoardObject.PositionToVector2(data.playerData.position);
            //player.md = data.playerData.md;
            //player.id = data.playerData.id;
            player.health = data.playerData.health;
            player.maxHealth = data.playerData.maxHealth;

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