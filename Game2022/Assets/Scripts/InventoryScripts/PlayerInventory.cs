using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public bool[] hasItem = new bool[8];

	public GameObject[] items = new GameObject[8];

	public bool IsInventoryFull() => GetFirstEmptySlot() == hasItem.Length;

	public int GetFirstEmptySlot() => hasItem.TakeWhile(isSlotHasItem => isSlotHasItem).Count();

	public void AddItem(GameObject gameObject)
	{
		var index = GetFirstEmptySlot();
		items[index] = gameObject;
		hasItem[index] = true;
	}

	public void DragAndDropItem(int slotIndex)
    {
		(InventoryHandler.draggedItem, items[slotIndex]) = (items[slotIndex], InventoryHandler.draggedItem);
		hasItem[slotIndex] = items[slotIndex] != null;
	}
}


