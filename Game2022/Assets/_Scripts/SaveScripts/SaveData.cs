using System;
using System.Collections.Generic;
using System.Linq;
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

        public SaveData()
        {
            playerData = new PlayerData(GameManager.Instance.player);
            monsterData = GetMonsterData();
            lootData = GetLootData();
        }

        private static LootData[] GetLootData()
        {
            return (from object loot in GameManager.Instance.lootContainer.transform
                    select new LootData(((Transform) loot).gameObject))
                .ToArray(); 
        }
        
        private static MonsterData[] GetMonsterData()
        {
            return (from object monster in GameManager.Instance.monsterContainer.transform
                    select new MonsterData((Transform) monster))
                .ToArray();
        }
    }

    [Serializable]
    public class PlayerData : OnBoardObject
    {
        //public MovementData md;
        public InventoryData id;

        public float health;
        public float maxHealth;

        public PlayerData(Player player)
        {
            //md = new MovementData(player.md);
            id = new InventoryData(player.id);
            health = player.health;
            maxHealth = player.maxHealth;
            position = GetObjectPosition(player.player);
        }
    }

    [Serializable]
    public class MonsterData : OnBoardObject
    {
        public int level;

        public MonsterData(GameObject obj)
        {
            level = GameManager.Instance.level;
            position = GetObjectPosition(obj);
        }

        public MonsterData(Transform transform)
        {
            level = GameManager.Instance.level;
            position = GetTransformPosition(transform);
        }
    }

    [Serializable]
    public class LootData : OnBoardObject
    {
        
        public LootData(GameObject loot)
        {
            position = GetObjectPosition(loot);
            
        }
    }
    
    [Serializable]
    public class OnBoardObject
    {
        public float[] position;

        public static float[] GetTransformPosition(Transform transform)
        {
            var pos = new float[2];
            var vector = transform.position;
            pos[0] = vector.x;
            pos[1] = vector.y;

            return pos;
        }

        public static float[] GetObjectPosition(GameObject obj)
        {
            return GetTransformPosition(obj.transform);
        }

        public static Vector2 PositionToVector2(float[] pos)
        {
            return new Vector2(pos[0], pos[1]);
        }
    }
}