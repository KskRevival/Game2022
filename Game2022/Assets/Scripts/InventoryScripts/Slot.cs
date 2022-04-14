using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	public Sprite sprite; //Спрайт брони для этого слота

	public Image icon; //Иконка, куда будет прикрепляться спрайт

	public Item item;

	public void UpdateSlot(Item newItem) //Обновление слота
	{
		sprite = newItem.GetComponent<SpriteRenderer>().sprite;
		item = newItem;
		icon.sprite = sprite;
		item = newItem;
	}
}