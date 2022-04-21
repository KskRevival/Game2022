using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
	private Canvas canvas;

    public GameObject player;

    private PlayerInventory items;

    public Transform inventorySlots;

    private Slot[] slots;

	public static GameObject draggedItem = null;

	void Start()
	{
		canvas = GetComponent<Canvas>();
		canvas.enabled = false;
        items = player.GetComponent<PlayerInventory>();
        slots = inventorySlots.GetComponentsInChildren<Slot>(); //Получение всех ячеек
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Time.timeScale = !canvas.enabled ? 0f : 1f;
            canvas.enabled = !canvas.enabled;
		}
		UpdateUI();
	}

	void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++) //Проверка всех предметов
		{
			slots[i].UpdateSlot(items.items[i], !items.hasItem[i]);
		}
	}
}

