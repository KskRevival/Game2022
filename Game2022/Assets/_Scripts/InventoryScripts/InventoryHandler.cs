using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LabyrinthScripts;
using PlayerScripts;
using UnityEngine;

namespace InventoryScripts
{
    public static class DraggedItem
    {
        public static GameObject Item;
        public static bool IsDraggingEquippedItem;
        public static int SourceSlotIndex;
    }

    public class InventoryHandler : MonoBehaviour
    {
        public GameObject inventoryPanel;
        private bool isInventoryActive;

        public Player player;

        public Transform inventorySlots;

        private Slot[] slots;

        void Awake()
        {
            inventoryPanel.SetActive(isInventoryActive);
            slots = inventorySlots.GetComponentsInChildren<Slot>(); //Получение всех яичек
            // player = GameManager.Instance.player;
        }

        void Update()
        {
            if (player == null) player = GameManager.Instance.player;
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isInventoryActive = !isInventoryActive;
                Time.timeScale = isInventoryActive ? 0f : 1f;

                if (!isInventoryActive && DraggedItem.Item != null) ReturnDraggedItem();

                inventoryPanel.SetActive(isInventoryActive);
            }

            UpdateUI();
        }

        void UpdateUI()
        {
            var equippedSlotsIndexes = player.GetEquippedSlotsIndexes();
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].UpdateSlot(player.id.items[i]);

                if (equippedSlotsIndexes.Contains(i)
                    && player.HasItemInIndex(i))
                    slots[i].SetSlotAsEquipped();
                else slots[i].SetSlotAsUnequipped();
            }
        }

        void ReturnDraggedItem()
        {
            Debug.Log(DraggedItem.SourceSlotIndex);
            if (player.HasItemInIndex(DraggedItem.SourceSlotIndex))
                player.AddItem(DraggedItem.Item);

            else player.DragAndDropItem(DraggedItem.SourceSlotIndex);

            // DraggedItem.Item = null;
        }
    }
}