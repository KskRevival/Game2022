using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using InventoryScripts;
using LabyrinthScripts;
using PlayerScripts;
using RoomGeneration;
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
        public InventorySaveData[] id;

        public float health;
        public float maxHealth;

        public PlayerData(Player player)
        {
            //md = new MovementData(player.md);
            id = GameManager.Instance.player.id.GetSaveData();
            health = player.health;
            maxHealth = player.maxHealth;
            position = GetObjectPosition(player.player);
        }
    }

    [Serializable]
    public class InventorySaveData
    {
        public ItemData itemData;

        public InventorySaveData(GameObject item)
        {
            itemData = item?.GetComponent<InventoryItem>().itemData;
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
        public ItemData itemData;
        public LootData(GameObject loot)
        {
            position = GetObjectPosition(loot);
            itemData = loot.GetComponent<ItemBehaviour>().itemData;
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