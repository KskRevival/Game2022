using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int minimalDefence;
    public int defence;

    public int maxHealth;
    public int health;

    public bool TakeDamage(int damage)
    {
        health -= damage;
        return health <= 0;
    }
}
