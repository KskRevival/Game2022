using System;
using System.Collections.Generic;
using System.Linq;
using InventoryScripts;
using LabyrinthScripts;
using UIScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public MovementData md;
        public InventoryData id;

        public int health;
        public int maxHealth;

        void Update()
        {
            if (GameManager.Instance.state == GameState.Fight || PauseScript.isPaused) return;
            var moveHorizontal = Input.GetAxisRaw("Horizontal");
            var moveVertical = Input.GetAxisRaw("Vertical");

            md.movement = new Vector2(moveHorizontal, moveVertical);

            md.animator.SetFloat(MovementData.Horizontal, moveHorizontal);
            md.animator.SetFloat(MovementData.Vertical, moveVertical);
            md.animator.SetFloat(MovementData.Speed, md.movement.sqrMagnitude);
        }
        
        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Stamina.IsStaminaAvailable(md.movement))
            {
                md.animator.speed = 2f;
                md.speed = MovementData.RunSpeed;
                Stamina.DrainStamina();
            }
            else
            {
                md.animator.speed = 1f;
                md.speed = MovementData.NormalSpeed;
                Stamina.RechargeStamina();
            }
            var speedMultiplier = md.movement.x != 0 && md.movement.y != 0 ? 0.75f : 1f;
            md.rb.MovePosition(md.rb.position + md.movement * (md.speed * Time.fixedDeltaTime * speedMultiplier));
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            if (health > 0) return;
            health = 0;
            Debug.Log("You're dead");
            SceneManager.LoadScene("DeathScene");
        }
        
        //Inventory Scripts
        public bool IsInventoryFull() => GetFirstEmptySlot() == id.items.Length;

        public int GetFirstEmptySlot() => id.items.TakeWhile(item => item != null).Count();

        public void AddItem(GameObject item)
        {
            var index = GetFirstEmptySlot();
            id.items[index] = item;
        }

        public void DragAndDropItem(int slotIndex)
        {
            var equippedSlots = GetEquippedSlotsIndexes();
            var handler = gameObject.GetComponentInChildren<InventoryHandler>();
            (handler.DraggedItem, id.items[slotIndex]) = (id.items[slotIndex], handler.DraggedItem);

            if (handler.IsDraggingEquippedItem)
                ReequipItem(id.items[slotIndex], slotIndex);

            handler.IsDraggingEquippedItem = equippedSlots.Contains(slotIndex);
            handler.SourceSlotIndex = slotIndex;
        }

        public bool HasItemInIndex(int index) => id.items[index] != null;
        
        public int GetWeaponDamage() => id.Weapon?.Item.GetComponent<WeaponScript>().damage ?? 0;

        public int GetArmor() => id.Armor?.Item.GetComponent<ArmorScript>().armor ?? 0;

        private bool IsWeapon(GameObject item) => item.GetComponent<WeaponScript>() != null;

        public List<int> GetEquippedSlotsIndexes()
        {
            var indexesArray = new List<int>();

            if (id.Weapon != null) indexesArray.Add(id.Weapon.SlotIndex);
            if (id.Armor != null) indexesArray.Add(id.Armor.SlotIndex);

            return indexesArray;
        }

        public void ReequipItem(GameObject item, int slotIndex)
        {
            if (IsWeapon(item)) id.Weapon.SlotIndex = slotIndex;
            else id.Armor.SlotIndex = slotIndex;
        }
    }
}
