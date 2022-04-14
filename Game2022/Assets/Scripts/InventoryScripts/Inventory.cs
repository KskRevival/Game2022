using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

	private Canvas canvas;

	public GameObject player;

	private Items items;

	public Transform inventorySlots;

	private Slot[] slots;

	void Start()
	{
		canvas = GetComponent<Canvas>();
		canvas.enabled = false;
		items = player.GetComponent<Items>();
		slots = inventorySlots.GetComponentsInChildren<Slot>(); //��������� ���� �����
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			UpdateUI(); //���������� ����������
			canvas.enabled = !canvas.enabled;
		}
	}

	void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++) //�������� ���� ���������
		{
			bool active = items.hasItems[i];

			slots[i].UpdateSlot(items.items[i]);
		}
	}
}

