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

        public static void DropDraggedItem()
        {
            
        }
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

        public void DropItem()
        {
            if (DraggedItem.Item == null) return;

            var dropPos = GameObject
                .FindGameObjectsWithTag("Waypoint")
                .Select(waypoint => waypoint.transform.position - player.transform.position)
                .OrderBy(vectorToWaypoint => vectorToWaypoint.magnitude)
                .First().normalized * 1.7f + player.transform.position;

            Debug.Log(dropPos);

            var itemOnScene = Instantiate(DraggedItem.Item.GetComponent<DropItem>().ItemOnScene, 
                dropPos, 
                Quaternion.identity, 
                GameManager.Instance.lootContainer.transform);
            DontDestroyOnLoad(itemOnScene);

            Destroy(DraggedItem.Item);
            DraggedItem.IsDraggingEquippedItem = false;
            DraggedItem.SourceSlotIndex = 0;
        }
    }
}