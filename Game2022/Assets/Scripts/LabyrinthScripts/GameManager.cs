using UnityEngine;

namespace LabyrinthScripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        
        public DungeonGenerator dungeonGenerator;
        public int level = 3;

        void Awake()
        {
            if (instance == null) instance = this;
            else if(instance != this) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            dungeonGenerator = GetComponent<DungeonGenerator>();
            InitGame();
        }

        void InitGame()
        {
        }

        public static GameObject GameObjectResources(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }
}
