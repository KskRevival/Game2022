using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Items : MonoBehaviour
{
	public bool[] hasItems = new bool[8];

	public GameObject[] items = new GameObject[8];

	public bool IsInventoryFull() => hasItems.TakeWhile(isSlotHasItem => isSlotHasItem).Count() == hasItems.Length;

	public int GetFirstEmptySlot() => hasItems.TakeWhile(isSlotHasItem => isSlotHasItem).Count();

	public void AddItem(GameObject gameObject)
	{
		var index = GetFirstEmptySlot();
		items[index] = gameObject;
		hasItems[index] = true;
	}
}


