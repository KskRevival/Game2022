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
        slots = inventorySlots.GetComponentsInChildren<Slot>(); //Получение всех ячеек
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Time.timeScale = !canvas.enabled ? 0f : 1f;
            UpdateUI(); //Обновление интерфейса
            canvas.enabled = !canvas.enabled;
		}
	}

	void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++) //Проверка всех предметов
		{
			slots[i].UpdateSlot(items.items[i], !items.hasItems[i]);
		}
	}
}

