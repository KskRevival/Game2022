using UnityEngine;

namespace LabyrinthScripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        public DungeonData data;
        public DungeonGenerator dungeonGenerator;
        public int level = 3;

        void Awake()
        {
            if (Instance == null) Instance = this;
            else if(Instance != this) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            dungeonGenerator = GetComponent<DungeonGenerator>();
            InitGame();
        }

        void InitGame()
        {
            dungeonGenerator.Generate(data);
        }

        public static GameObject GameObjectResources(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }
}
