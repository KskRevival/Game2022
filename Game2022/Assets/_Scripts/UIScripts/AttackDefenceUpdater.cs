using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackDefenceUpdater : MonoBehaviour
{
    public TextMeshProUGUI attack;
    public TextMeshProUGUI defence;
    
    
    void Update()
    {
        attack.text = $"= {GameManager.Instance.player.GetWeaponDamage() + 2}";
        defence.text = $"= {GameManager.Instance.player.GetArmor()}";
    }
}
