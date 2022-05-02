using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject Armor;

    public int GetDamage() => Weapon.GetComponent<WeaponScript>().Damage;
}
