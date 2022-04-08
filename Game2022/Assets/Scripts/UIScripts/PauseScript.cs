using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScript: MonoBehaviour
{
    public float timer;
    public bool isPaused;

    void Update()
    {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        timer = isPaused? 0 : 1f;
    }

    public void OnGUI()
    {
        if (!isPaused) return;
        Cursor.visible = true;
        if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 150f, 150f, 45f), "Продолжить"))
        {
            isPaused = false;
            timer = 0;
            Cursor.visible = false;
        }
        if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, 150f, 45f), "Сохранить")){}
        if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, 150f, 45f), "Загрузить")){}
        if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 150f, 45f), "В Меню"))
        {
            isPaused = false;
            timer = 0;
            SceneManager.LoadScene(0);
        }
    }
}