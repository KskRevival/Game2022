using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public GameObject[] items = new GameObject[8];

	public bool IsInventoryFull() => GetFirstEmptySlot() == items.Length;

	public int GetFirstEmptySlot() => items.TakeWhile(item => item != null).Count();

	public void AddItem(GameObject gameObject)
	{
		var index = GetFirstEmptySlot();
		items[index] = gameObject;
	}

	public void DragAndDropItem(int slotIndex)
    {
		var equippedSlots = GetComponent<PlayerEquipment>().GetEquippedSlotsIndexes();
		var itemToSlot = DraggedItem.Item;

		DraggedItem.Item = items[slotIndex];
		DraggedItem.IsDraggingeEuippedItem = equippedSlots.Contains(slotIndex);
		DraggedItem.SourceSlotIndex = slotIndex;

		items[slotIndex] = itemToSlot;
	}

	public bool HasItemInIndex(int index) => !(items[index] == null);
}


