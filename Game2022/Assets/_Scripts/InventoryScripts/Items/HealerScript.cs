using System.Collections;
using System.Collections.Generic;
using InventoryScripts.Items;
using UnityEngine;

public class HealerScript : EquippedItem
{
    public void UseHealer()
    {
        GameManager.Instance.player.health = GameManager.Instance.player.maxHealth;
        if (GameManager.Instance.state == GameState.Fight)
        {
            var bs = GameObject
                .Find("BattleSystem")
                .GetComponent<BattleSystem>();
            bs.playerUnit.health = GameManager.Instance.player.maxHealth;
            bs.playerHUD.SetHP(GameManager.Instance.player.maxHealth);
        }

        Destroy(gameObject);
    }
}