using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	public Sprite sprite; //Спрайт брони для этого слота

	public Image icon; //Иконка, куда будет прикрепляться спрайт

	public void UpdateSlot(Sprite sprite) //Обновление слота
	{
		icon.sprite = sprite;
	}
}