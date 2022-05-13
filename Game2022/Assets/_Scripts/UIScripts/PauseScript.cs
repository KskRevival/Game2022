using System;
using LabyrinthScripts;
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