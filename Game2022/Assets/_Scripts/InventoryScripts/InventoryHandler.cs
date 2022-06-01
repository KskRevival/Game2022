using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LabyrinthScripts;
using PlayerScripts;
using UIScripts;
using UnityEngine;

namespace InventoryScripts
{
    public class InventoryHandler : MonoBehaviour
    {
        public GameObject inventoryPanel;
        public static bool IsInventoryActive;

        public GameObject DraggedItem;
        public bool IsDraggingEquippedItem;
        public int SourceSlotIndex;

        public Player player;

        public Transform inventorySlots;

        private Slot[] slots;

        void Awake()
        {
            inventoryPanel.SetActive(IsInventoryActive);
            slots = inventorySlots.GetComponentsInChildren<Slot>(); //��������� ���� �����
            // player = GameManager.Instance.player;
        }

        void Update()
        {
            if (player == null) player = GameManager.Instance.player;
            if (PauseScript.IsPaused) return;
            if (Input.GetKeyDown(KeyCode.Tab) && GameManager.Instance.state != GameState.Fight) SwitchInventory();

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

        public void SwitchInventory()
        {
            IsInventoryActive = !IsInventoryActive;
            Time.timeScale = IsInventoryActive ? 0f : 1f;

            if (!IsInventoryActive && DraggedItem != null) ReturnDraggedItem();

            inventoryPanel.SetActive(IsInventoryActive);
        }

        void ReturnDraggedItem()
        {
            Debug.Log(SourceSlotIndex);
            if (player.HasItemInIndex(SourceSlotIndex))
                player.AddItem(DraggedItem);

            else player.DragAndDropItem(SourceSlotIndex);

            // DraggedItem.Item = null;
        }

        public void DropItem()
        {
            if (DraggedItem == null) return;

            var dropPos = GameObject
                .FindGameObjectsWithTag("Waypoint")
                .Select(waypoint => waypoint.transform.position - player.transform.position)
                .OrderBy(vectorToWaypoint => vectorToWaypoint.magnitude)
                .First().normalized * 1.7f + player.transform.position;

            var itemOnScene = Instantiate(DraggedItem.GetComponent<DropItem>().ItemOnScene,
                dropPos,
                Quaternion.identity,
                GameManager.Instance.lootContainer.transform);

            DestroyDraggedItem();
        }

        public void RemoveFromInventory(int index)
        {
            GameManager.Instance.player.id.items[index] = null;
        }

        public void DestroyDraggedItem()
        {
            RemoveFromInventory(SourceSlotIndex);
            Destroy(DraggedItem);
            IsDraggingEquippedItem = false;
            SourceSlotIndex = 0;
        }
    }
}
