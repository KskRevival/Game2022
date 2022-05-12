using PlayerScripts;
using UnityEngine;

namespace LabyrinthScripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public GameState State;
        
        public Player player;
        
        public DungeonData data;
        public DungeonGenerator dungeonGenerator;
        public int level;

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
            Destroy(GameObject.FindWithTag("Player"));
        }

        void InitGame()
        {
            dungeonGenerator = GetComponent<DungeonGenerator>();
            dungeonGenerator.Generate(data);
            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            player = dungeonGenerator.SpawnPlayer().GetComponent<Player>();
        }
        
        public static GameObject GameObjectResources(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }

    public enum GameState
    {
        Maze,
        Fight
    }
}
