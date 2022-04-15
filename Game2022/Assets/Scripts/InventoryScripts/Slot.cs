using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	public Sprite sprite; //Спрайт брони для этого слота

	public Image icon; //Иконка, куда будет прикрепляться спрайт

	public void UpdateSlot(Sprite sprite, bool isEmpty) //Обновление слота
	{
		if (!isEmpty)
		{
			icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1f);
			icon.sprite = sprite;
		}
        else
        {
			icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0f);
			icon.sprite = null;
		}
	}
}