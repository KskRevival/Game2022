using UnityEngine;

namespace LabyrinthScripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        
        public BoardManager boardManager;
        public int level = 3;

        void Awake()
        {
            if (instance == null) instance = this;
            else if(instance != this) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            boardManager = GetComponent<BoardManager>();
            InitGame();
        }

        void InitGame()
        {
            boardManager.SetupScene(level);
        }
    }
}
