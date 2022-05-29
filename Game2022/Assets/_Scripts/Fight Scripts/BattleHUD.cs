using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;    
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI armorText;
    public Slider HPSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = $@"lvl{unit.unitLevel}";
        HPSlider.maxValue = unit.maxHealth;
        HPSlider.value = unit.health;
    }

    public void SetHP(int hp)
    {
        HPSlider.value = hp;
    }
}