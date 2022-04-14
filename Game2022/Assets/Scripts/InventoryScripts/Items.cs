using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Items : MonoBehaviour
{
	public bool[] hasItems = new bool[8];

	public Sprite[] sprites = new Sprite[8];

	public GameOvject[] items = new Item[8];

	public bool IsInventoryFull() => hasItems.TakeWhile(isSlotHasItem => isSlotHasItem).Count() == hasItems.Length;

	public int GetFirstEmptySlot() => hasItems.TakeWhile(isSlotHasItem => isSlotHasItem).Count();

	public void AddItem(GameObject gameObject)
	{
		var index = GetFirstEmptySlot();
		hasItems[index] = true;
		sprites[index] = gameObject.GetComponent<SpriteRenderer>().sprite;
	}
}
