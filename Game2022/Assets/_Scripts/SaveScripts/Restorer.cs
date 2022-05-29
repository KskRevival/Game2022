using InventoryScripts;
using RoomGeneration;
using UnityEngine;

namespace SaveScripts
{
    public static class Restorer
    {
        public static GameObject RestoreInventoryItem(InventorySaveData isd)
        {
            return isd?.itemData == null
                ? null
                : RestoreItem(isd.itemData).GetComponent<ItemBehaviour>().itemInInventory;
        }

        public static GameObject RestoreItem(ItemData itemData)
        {
            var item =  GenerationData.Objects[(int) itemData.type][itemData.itemSpawnIndex];
            var data = item.GetComponent<ItemBehaviour>().itemData;
            data.type = itemData.type;
            data.itemSpawnIndex = itemData.itemSpawnIndex;
            
            return item;
        }

        public static GameObject RestoreMonster()
        {
            return GenerationData.Objects[(int) Spawnable.Monster][GameManager.Instance.level - 1];
        }
    }
}