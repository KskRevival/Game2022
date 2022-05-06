using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class TransitionScript : MonoBehaviour
    {
        public static void Next()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    
        public static void Back()
        {
            Debug.Log("BackButton pushed");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public static void ToLevelSelect()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
        }

        public static void Death()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("DeathScene");
        }

        public static void ToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    
        public static void Exit()
        {
            Debug.Log("Exit");
            Application.Quit();
        }
    }
}
