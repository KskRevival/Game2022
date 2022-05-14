using System;
using System.Xml.Serialization;
using LabyrinthScripts;
using PlayerScripts;
using UnityEngine;

namespace SaveScripts
{
    [Serializable]
    public class SaveData
    {
        public PlayerData playerData;
        public MonsterData[] monsterData;
        public LootData[] lootData;
        
        private GameObject player;
        
        public SaveData()
        {
            playerData = new PlayerData(GameManager.Instance.player);
            player = GameManager.Instance.player.player;
        }
    }
    
    public class PlayerData : OnBoardObject
    {
        public MovementData md;
        public InventoryData id;

        public float health;
        public float maxHealth;

        public PlayerData(Player player)
        {
            md = new MovementData(player.md);
            id = new InventoryData(player.id);
            health = player.health;
            maxHealth = player.maxHealth;
            position = GetObjectPosition(player.player);
        }
    }

    public class MonsterData : OnBoardObject
    {
        public int level;

        public MonsterData(GameObject obj)
        {
            level = GameManager.Instance.level;
            position = GetObjectPosition(obj);
        }
    }

    public class LootData : OnBoardObject
    {
        
    }

    public class OnBoardObject
    {
        public float[] position;

        public static float[] GetObjectPosition(GameObject obj)
        {
            var pos = new float[2];
            var transform = obj.transform.position;
            pos[0] = transform.x;
            pos[1] = transform.y;
            
            return pos;
        }

        public static Vector2 PositionToVector2(float[] pos)
        {
            return new Vector2(pos[0], pos[1]);
        }
    }
}