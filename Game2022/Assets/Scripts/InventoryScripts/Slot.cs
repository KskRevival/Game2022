using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	public Sprite sprite; //������ ����� ��� ����� �����

	public Image icon; //������, ���� ����� ������������� ������

	public Item item;

	public void UpdateSlot(Item newItem) //���������� �����
	{
		sprite = newItem.GetComponent<SpriteRenderer>().sprite;
		item = newItem;
		icon.sprite = sprite;
		item = newItem;
	}
}