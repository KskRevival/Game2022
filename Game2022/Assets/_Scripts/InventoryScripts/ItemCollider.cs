using LabyrinthScripts;
using UnityEngine;

namespace InventoryScripts
{
	public class ItemCollider : MonoBehaviour
	{
		public GameObject itemInInventory;

		void OnTriggerEnter2D(Collider2D collidedObject)
		{
			if (!collidedObject.transform.CompareTag("Player")) return;
			var player = GameManager.Instance.player;
			if (player.IsInventoryFull()) return;
			player.AddItem(itemInInventory);
			Destroy(gameObject);
		}
	}
}
