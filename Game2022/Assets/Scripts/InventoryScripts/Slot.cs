using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	public Sprite sprite; //������ ����� ��� ����� �����

	public Image icon; //������, ���� ����� ������������� ������

	public void UpdateSlot(Sprite sprite, bool isEmpty) //���������� �����
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