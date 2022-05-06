using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class MainMenuScript : MonoBehaviour
    {
        public void PlayButton()
        {
            TransitionScript.Next();
        }

        public void ExitButton()
        {
            TransitionScript.Exit();
        }
    }
}
