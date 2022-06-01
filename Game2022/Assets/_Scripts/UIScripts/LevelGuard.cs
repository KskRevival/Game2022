using SaveScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class LevelGuard : MonoBehaviour
    {
        public Button[] buttons;

        void Start()
        {
            buttons[0].interactable = true;
            var data = SaveAndLoad.LoadGame();
            if (data == null) return;
            for (var i = 1; i < buttons.Length; i++)
            {
                buttons[i].interactable = i < data.level;
            }
        }
    }
}
