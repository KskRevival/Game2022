using UnityEngine;
using System.Collections;
using PlayerScripts;
using SaveScripts;
using UnityEngine.SceneManagement;

public class PauseScript: MonoBehaviour
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
        GameObject.Find("Player").transform.position =
            new Vector3(data.position[0], data.position[1], data.position[2]);

        var player = GameObject.Find("Player").GetComponent<Player>();
        
        player.MaxHealth = (int) data.maxHealth;
        player.Health = data.health;
        Debug.Log(player.Health);
        HealthBar.SetHealth(data.health);

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