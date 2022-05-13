using LabyrinthScripts;
using PlayerScripts;
using UIScripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
        
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

        //GameObject.FindWithTag("MainCamera").GetComponent<PauseScript>().pauseMenuUI =
            //Instantiate(GameObjectResources("PauseMenu"));
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