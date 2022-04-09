using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScript: MonoBehaviour
{
    public float timer;
    public static bool isPaused;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape");
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        isPaused = false;
        timer = 1f;
        pauseMenuUI.SetActive(isPaused);
    }
    public void Pause()
    {
        isPaused = true;
        timer = 0f;
        pauseMenuUI.SetActive(isPaused);
    }

    public void Save()
    {
        //make it later
    }

    public void Load()
    {
        //make it later
    }

    public void ToMenu()
    {
        ExitScript.ToMenu();
    }

    public void Exit()
    {
        ExitScript.Exit();
    }
}