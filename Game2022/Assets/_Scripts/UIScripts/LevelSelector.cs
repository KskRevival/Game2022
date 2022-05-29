using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class LevelSelector : MonoBehaviour
    {
        public void Load()
        {
            
        }

        public void Level(int index)
        {
            SceneManager.LoadScene(index + 1);
        }
    }
}
