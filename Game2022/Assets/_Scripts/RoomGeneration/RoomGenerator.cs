using InventoryScripts;
using UnityEngine;

namespace RoomGeneration
{
    public class RoomGenerator : MonoBehaviour
    {
        private const int CellCount = 16;
        private const int Side = 4;
        public GameObject leftCorner;
        public Spawnable type;
        public int itemSpawnIndex;

        void Start()
        {
            var item = GetItem();
            if (item == null) return;
            var pos = Random.Range(0, CellCount);
            var position = leftCorner.transform.position;
            var container = type == Spawnable.Monster
                ? GameManager.Instance.monsterContainer.transform
                : GameManager.Instance.lootContainer.transform;
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
            if (type == Spawnable.Monster) return;
            var itemData = loot.GetComponent<ItemBehaviour>().itemData;
            itemData.type = type;
            itemData.itemSpawnIndex = itemSpawnIndex;
        }

        GameObject GetItem()
        {
            type = (Spawnable) GetIndex(Random.Range(0, 100), Spawnable.Empty);
            return type == Spawnable.Empty
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