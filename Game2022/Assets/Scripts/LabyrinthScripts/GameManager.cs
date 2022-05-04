using PlayerScripts;
using UnityEngine;

namespace LabyrinthScripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        public Player player;
        
        public DungeonData data;
        public DungeonGenerator dungeonGenerator;
        public int level = 3;

        void Awake()
        {
            if (Instance == null) Instance = this;
            else if(Instance != this) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            InitGame();
        }

        public static GameObject CreatePlayer(bool forSave)
        {
            return forSave
                ? Instantiate(GameObjectResources("Player"))
                : Instance.dungeonGenerator.SpawnPlayer();
        }

        public static void DestroyPlayer()
        {
            Destroy(Instance.player);
        }

        void InitGame()
        {
            dungeonGenerator = GetComponent<DungeonGenerator>();
            dungeonGenerator.Generate(data);
            player = gameObject.AddComponent<Player>();
            Player.Instance = dungeonGenerator.SpawnPlayer();
        }

        public static GameObject GameObjectResources(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }
}
