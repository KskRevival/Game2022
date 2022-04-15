using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public GameObject item;

	void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.transform.tag == "Player")
		{

			var items = obj.GetComponent<Items>();
			if (!items.IsInventoryFull())
			{
				items.AddItem(item);
				Destroy(gameObject);
			}
		}
	}
}
