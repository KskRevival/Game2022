using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrinkScript : MonoBehaviour
{
    public void UseEnergyDrink()
    {
        Stamina.stamina = 1f;
        Destroy(gameObject);
    }
}
