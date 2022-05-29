using InventoryScripts.Items;
using LabyrinthScripts;
using PlayerScripts;
using UnityEngine.SceneManagement;

namespace InventoryScripts
{
    public static class UseItem
    {
        public static Player player;

        public static void UseFromSlot(int slotIndex)
        {
            if (player == null) player = GameManager.Instance.player;
            if (!player.HasItemInIndex(slotIndex)) return;

            var weaponComponent = player.id.items[slotIndex].GetComponent<WeaponScript>();
            var armorComponent = player.id.items[slotIndex].GetComponent<ArmorScript>();
            var healerComponent = player.id.items[slotIndex].GetComponent<HealerScript>();
            var energyDrinkComponent = player.id.items[slotIndex].GetComponent<EnergyDrinkScript>();
            var grenadeComponent = player.id.items[slotIndex].GetComponent<GrenadeScript>();

            if (weaponComponent != null) 
                player.id.Weapon 
                    = player.id.Weapon == null 
                      || player.id.Weapon.SlotIndex != slotIndex
                        ? new EquipmentItem(player.id.items[slotIndex], slotIndex)
                        : null;

            if (armorComponent != null)
                player.id.Armor
                    = player.id.Armor == null
                      || player.id.Armor.SlotIndex != slotIndex
                        ? new EquipmentItem(player.id.items[slotIndex], slotIndex)
                        : null;

            if (energyDrinkComponent != null)
            {
                GameManager.Instance.player.GetComponentInChildren<InventoryHandler>().RemoveFromInventory(slotIndex);
                energyDrinkComponent.UseEnergyDrink();
            }
            
            if (healerComponent != null)
            {
                GameManager.Instance.player.GetComponentInChildren<InventoryHandler>().RemoveFromInventory(slotIndex);
                healerComponent.UseHealer();
            }

            if (grenadeComponent != null)
            {
                GameManager.Instance.player.GetComponentInChildren<InventoryHandler>().RemoveFromInventory(slotIndex);
                if (GameManager.Instance.state != GameState.Fight) SceneManager.LoadScene("DeathScene");
                else grenadeComponent.ThrowGrenade();
            }
        }
    }
}
