using InventoryScripts.Items;
using LabyrinthScripts;
using PlayerScripts;
using RoomGeneration;
using UnityEngine;
using Random = System.Random;

namespace InventoryScripts
{
    public class ItemBehaviour : MonoBehaviour
    {
        private Random random = new Random();
        
        public GameObject itemInInventory;
        public ItemData itemData;

        void OnTriggerEnter2D(Collider2D collidedObject)
        {
            if (!collidedObject.transform.CompareTag("Player")) return;

            if (itemData.type == Spawnable.Ammo)
            {
                var ammo = random.Next(1, 5);
                AmmoCounter.AmmoCount += ammo;
                Destroy(gameObject);
                return;
            }
            
            var player = GameManager.Instance.player;

            if (player.IsInventoryFull()) return;
            CopyItemDataToInventory();

            var instantiatedItem = Instantiate(
                itemInInventory,
                new Vector3(-999, 999, -999),
                Quaternion.identity,
                GameManager.Instance.dropContainer.transform);
            player.AddItem(instantiatedItem);
            // player.AddItem(itemInInventory);

            Destroy(gameObject);
        }

        void CopyItemDataToInventory()
        {
            var newItemData = itemInInventory.GetComponent<InventoryItem>().itemData;
            newItemData.type = itemData.type;
            newItemData.itemSpawnIndex = itemData.itemSpawnIndex;
        }
    }
}