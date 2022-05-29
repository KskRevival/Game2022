using System.Collections;
using System.Collections.Generic;
using InventoryScripts.Items;
using UnityEngine;

public class HealerScript : EquippedItem
{
    public void UseHealer()
    {
        GameManager.Instance.player.health = GameManager.Instance.player.maxHealth;
        Destroy(gameObject);
    }
}
