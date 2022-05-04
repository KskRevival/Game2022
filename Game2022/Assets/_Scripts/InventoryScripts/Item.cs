using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public GameObject itemInInventory;

	void OnTriggerEnter2D(Collider2D collidedObject)
	{
		if (collidedObject.transform.tag == "Player")
		{
			var items = collidedObject.GetComponent<PlayerInventory>();
			if (!items.IsInventoryFull())
			{
				items.AddItem(itemInInventory);
				Destroy(gameObject);
			}
		}
	}
}
