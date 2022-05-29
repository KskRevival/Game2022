using UnityEngine;

namespace InventoryScripts.Items
{
    public class GrenadeScript : InventoryItem
    {
        public void ThrowGrenade()
        {
            var bs = GameObject
                .Find("BattleSystem")
                .GetComponent<BattleSystem>();
            bs.enemyUnit.health -= 10;
            bs.enemyHUD.HPSlider.value = bs.enemyUnit.health;
            if (bs.enemyUnit.health <= 0)
            {
                bs.state = BattleState.Won;
                bs.EndBattle();
            }
            Destroy(gameObject);
        }
    }
}
