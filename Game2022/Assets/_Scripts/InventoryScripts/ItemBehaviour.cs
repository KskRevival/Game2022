using InventoryScripts.Items;
using LabyrinthScripts;
using PlayerScripts;
using RoomGeneration;
using UnityEngine;

namespace InventoryScripts
{
	public class ItemBehaviour : MonoBehaviour
	{
		public GameObject itemInInventory;
		public ItemData itemData;

		void OnTriggerEnter2D(Collider2D collidedObject)
		{
			if (!collidedObject.transform.CompareTag("Player")) return;
			var player = GameManager.Instance.player;
			if (player.IsInventoryFull()) return;
			CopyItemDataToInventory();
			player.AddItem(itemInInventory);
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