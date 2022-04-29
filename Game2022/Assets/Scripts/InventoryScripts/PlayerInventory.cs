using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public GameObject[] items = new GameObject[8];

	public bool IsInventoryFull() => GetFirstEmptySlot() == items.Length;

	private int GetFirstEmptySlot() => items.TakeWhile(item => item != null).Count();

	public void AddItem(GameObject gameObject)
	{
		var index = GetFirstEmptySlot();
		items[index] = gameObject;
	}

	public void DragAndDropItem(int slotIndex)
    {
		(InventoryHandler.draggedItem, items[slotIndex]) = (items[slotIndex], InventoryHandler.draggedItem);
    }
}


