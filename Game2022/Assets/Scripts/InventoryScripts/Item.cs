using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{ 	
	void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.transform.CompareTag("Player"))
		{
			var items = obj.GetComponent<Items>();
			if (!items.IsInventoryFull())
			{
				items.AddItem(gameObject);
				Destroy(gameObject);
			}
		}
	}
}
