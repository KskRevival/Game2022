using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    public static float Health, MaxHealth = 100;

    private void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Health = 0;
            Debug.Log("You're dead");
            //OnPlayerDeath?.Invoke();
            SceneManager.LoadScene("DeathScene");
        }
    }
}
