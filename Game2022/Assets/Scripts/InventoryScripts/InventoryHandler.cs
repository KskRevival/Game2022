using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryHandler : MonoBehaviour
{
    public GameObject InventoryPanel;
    private bool isInventoryActive;

    public GameObject player;

    private PlayerInventory playerInventory;

    public Transform inventorySlots;

    private Slot[] slots;

    public static GameObject draggedItem = null;

    void Start()
    {
        InventoryPanel.SetActive(isInventoryActive);
        playerInventory = player.GetComponent<PlayerInventory>();
        slots = inventorySlots.GetComponentsInChildren<Slot>(); //��������� ���� �����
    }

    void Update()
    {
        UpdateUI();
        if (!Input.GetKeyDown(KeyCode.Tab)) return;
        isInventoryActive = !isInventoryActive;
        Time.timeScale = isInventoryActive ? 0f : 1f;
        InventoryPanel.SetActive(isInventoryActive);
    }

    void UpdateUI()
    {
        for (var i = 0; i < slots.Length; i++) //�������� ���� ���������
            slots[i].UpdateSlot(playerInventory.items[i]);
    }
}