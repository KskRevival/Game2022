using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class DraggedItem
{
	public static GameObject Item = null;
	public static bool IsDraggingeEuippedItem;
	public static int SourceSlotIndex;
}

public class InventoryHandler : MonoBehaviour
{
	public GameObject InventoryPanel;
	private bool isInventoryActive;

    public GameObject player;

    private PlayerInventory playerInventory;

    public Transform InventorySlots;

    private Slot[] Slots;

	void Start()
	{
		isInventoryActive = false;
		InventoryPanel.SetActive(isInventoryActive);
        playerInventory = player.GetComponent<PlayerInventory>();
        Slots = InventorySlots.GetComponentsInChildren<Slot>(); //Получение всех ячеек
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			isInventoryActive = !isInventoryActive;
			Time.timeScale = isInventoryActive ? 0f : 1f;

			if (!isInventoryActive && DraggedItem.Item != null) ReturnDraggedItem();

			InventoryPanel.SetActive(isInventoryActive);
		}
		UpdateUI();
	}

	void UpdateUI()
	{
		for (int i = 0; i < Slots.Length; i++) //Проверка всех предметов
			Slots[i].UpdateSlot(playerInventory.items[i]);
	}

	void ReturnDraggedItem()
    {
		Debug.Log(DraggedItem.SourceSlotIndex);
		if (playerInventory.HasItemInIndex(DraggedItem.SourceSlotIndex)) 
			playerInventory.AddItem(DraggedItem.Item);

		else playerInventory.DragAndDropItem(DraggedItem.SourceSlotIndex);

		DraggedItem.Item = null;
	}
}

