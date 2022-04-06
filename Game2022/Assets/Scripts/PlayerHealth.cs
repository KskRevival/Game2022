using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    public static float Health, MaxHealth = 100;

    // Start is called before the first frame update
    private void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Health = 0;
            Debug.Log("You're dead");
            OnPlayerDeath?.Invoke();
        }
    }
}
