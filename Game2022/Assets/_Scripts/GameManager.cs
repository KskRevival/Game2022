using LabyrinthScripts;
using PlayerScripts;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;

    public Player player;
    public GameObject container;

    public DungeonData data;
    public DungeonGenerator dungeonGenerator;
    public int level;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    public static void DestroyPlayer()
    {
        Destroy(GameObject.FindWithTag("Player"));
    }

    void InitGame()
    {
        dungeonGenerator = GetComponent<DungeonGenerator>();
        dungeonGenerator.Generate(data);
        InitContainers();
        SpawnPlayer();
    }

    void InitContainers()
    {
        container = Instantiate(
            GameObjectResources("Containers/LootContainer"),
            new Vector3(0, 0, 0),
            Quaternion.identity,
            transform);
    }

    public void SpawnPlayer()
    {
        player = dungeonGenerator.SpawnPlayer().GetComponent<Player>();
        player.md = new MovementData(player.player);
        player.id = new InventoryData();
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