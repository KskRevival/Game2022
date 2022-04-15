using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	public Sprite sprite; //Спрайт брони для этого слота

	public Image icon; //Иконка, куда будет прикрепляться спрайт

	public GameObject gameObject;

	public void UpdateSlot(GameObject gameObjectToSlot, bool isEmpty) //Обновление слота
	{
		if (!isEmpty)
		{
			gameObject = gameObjectToSlot;
			sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
			icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1f);
			icon.sprite = sprite;
		}
        else
        {
			gameObject = null;
			sprite = null;
			icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0f);
			icon.sprite = null;
		}
	}
}