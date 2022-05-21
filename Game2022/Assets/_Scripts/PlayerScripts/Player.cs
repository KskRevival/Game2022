using System;
using System.Collections.Generic;
using System.Linq;
using InventoryScripts;
using LabyrinthScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public GameObject player;
        public MovementData md;
        public InventoryData id;

        public float health;
        public float maxHealth;

        void Update()
        {
            if (GameManager.Instance.state == GameState.Fight) return;
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

        public void TakeDamage(float amount)
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

        public void AddItem(GameObject gameObject)
        {
            var index = GetFirstEmptySlot();
            id.items[index] = gameObject;
        }

        public void DragAndDropItem(int slotIndex)
        {
            var equippedSlots = GetEquippedSlotsIndexes();
            (DraggedItem.Item, id.items[slotIndex]) = (id.items[slotIndex], DraggedItem.Item);

            if (DraggedItem.IsDraggingEquippedItem)
                ReequipItem(id.items[slotIndex], slotIndex);

            DraggedItem.IsDraggingEquippedItem = equippedSlots.Contains(slotIndex);
            DraggedItem.SourceSlotIndex = slotIndex;
        }

        public bool HasItemInIndex(int index) => id.items[index] != null;
        
        public int GetWeaponDamage() => id.Weapon.Item.GetComponent<WeaponScript>().Damage;

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
