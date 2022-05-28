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
            return GenerationData.Objects[(int) itemData.type][itemData.itemSpawnIndex];
        }

        public static GameObject RestoreMonster()
        {
            return GenerationData.Objects[(int) Spawnable.Monster][GameManager.Instance.level - 1];
        }
    }
}