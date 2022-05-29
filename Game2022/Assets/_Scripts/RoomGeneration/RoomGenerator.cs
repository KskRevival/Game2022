using InventoryScripts;
using InventoryScripts.Items;
using UnityEngine;
using static RoomGeneration.Spawnable;

namespace RoomGeneration
{
    public class RoomGenerator : MonoBehaviour
    {
        private const int CellCount = 16;
        private const int Side = 4;
        public GameObject leftCorner;
        public Spawnable type;
        public int itemSpawnIndex;
        public static int roomsCreated;

        void Start()
        {
            roomsCreated++;
            var item = GetItem();
            if (item == null) return;
            var pos = Random.Range(0, CellCount);
            var position = leftCorner.transform.position;
            var container = type == Monster
                ? GameManager.Instance.monsterContainer.transform
                : GameManager.Instance.lootContainer.transform;
            
            if (type == Monster && roomsCreated < 4) return;
            
            var loot = Instantiate(
                item,
                new Vector2(
                    position.x + pos / Side + 1,
                    position.y - pos % Side - 1),
                Quaternion.identity,
                container);
            
            AddItemData(loot);
        }
        
        

        void AddItemData(GameObject loot)
        {
            if (type == Monster) return;
            var itemData = loot.GetComponent<ItemBehaviour>().itemData;
            itemData.type = type;
            itemData.itemSpawnIndex = itemSpawnIndex;
        }

        GameObject GetItem()
        {
            type = (Spawnable) GetIndex(Random.Range(0, 100), Empty);
            return type == Empty
                ? null
                : GenerationData.Objects[(int) type][itemSpawnIndex = GetIndex(Random.Range(0, 100), type)];
        }

        int GetIndex(int gen, Spawnable spawnable)
        {
            var index = 0;
            while (index < GenerationData.Chances[(int) spawnable].Length
                   && GenerationData.Chances[(int) spawnable][index] < gen)
            {
                gen -= GenerationData.Chances[(int) spawnable][index++];
            }

            //Debug.Log(index);
            return index;
        }
    }
}