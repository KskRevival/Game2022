using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public GameObject item;

	void OnTriggerEnter2D(Collider2D collidedObject)
	{
		if (collidedObject.transform.tag == "Player")
		{
			var items = collidedObject.GetComponent<Items>();
			if (!items.IsInventoryFull())
			{
				items.AddItem(item);
				Destroy(gameObject);
			}
		}
	}
}
