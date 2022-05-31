using System.Linq;
using LabyrinthScripts;
using PlayerScripts;
using RoomGeneration;
using SaveScripts;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;

    public Player player;
    
    public GameObject lootContainer;
    public GameObject monsterContainer;
    public GameObject roomContainer;
    public GameObject dropContainer;

    public DungeonData data;
    public DungeonGenerator dungeonGenerator;
    public int level;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitGame();
        }
        else if (Instance != this && Instance.level == level) Destroy(gameObject);
    }

    void InitGame()
    {
        RoomGenerator.roomsCreated = 0;
        InitContainers();
        dungeonGenerator = GetComponent<DungeonGenerator>();
        dungeonGenerator.Generate(data);
        SpawnPlayer();
        level = SceneManager.GetActiveScene().buildIndex - 1;
        if (level != 1) LoadPlayer();
    }

    public static void LoadPlayer()
    {
        var data = SaveAndLoad.LoadGame();
        
        var player = Instance.player;
        player.id.items =
            data.playerData.id
                //.Where(id => id.itemData != null)
                .Select(x =>
                {
                    var restored = Restorer.RestoreInventoryItem(x);
                    if (restored == null) return null;
                    return Instantiate(
                        restored,
                        new Vector3(-999, 999, -999),
                        Quaternion.identity,
                        GameManager.Instance.dropContainer.transform);
                })
                .ToArray();
        player.health = data.playerData.health;
        player.maxHealth = data.playerData.maxHealth;
        AmmoCounter.AmmoCount = data.ammo;
    }

    void InitContainers()
    {
        lootContainer = Instantiate(
            GameObjectResources("Containers/LootContainer"),
            new Vector3(0, 0, 0),
            Quaternion.identity,
            transform);
        
        monsterContainer = Instantiate(
            GameObjectResources("Containers/MonsterContainer"),
            new Vector3(0, 0, 0),
            Quaternion.identity,
            transform);
        
        roomContainer = Instantiate(
            GameObjectResources("Containers/RoomContainer"),
            new Vector3(0, 0, 0),
            Quaternion.identity,
            transform);
        
        dropContainer = Instantiate(
            GameObjectResources("Containers/DropContainer"),
            new Vector3(0, 0, 0),
            Quaternion.identity,
            transform);
    }

    public void SpawnPlayer()
    {
        player = dungeonGenerator.SpawnPlayer().GetComponent<Player>();
        player.md = new MovementData(player.gameObject);
        player.id = new InventoryData();
    }

    public void DestroyMonsters()
    {
        foreach (var monster in monsterContainer.transform)
            Destroy(((Transform)monster).gameObject);
    }

    public void DestroyLoot()
    {
        foreach (var loot in lootContainer.transform)
            Destroy(((Transform)loot).gameObject);
    }

    public static GameObject GameObjectResources(string path)
    {
        return Resources.Load<GameObject>(path);
    }

    public static AudioClip AudioClipResources(string path)
    {
        return Resources.Load<AudioClip>(path);
    }
}

public enum GameState
{
    Maze,
    Fight
}