using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    private const int DamageByMonster = 25;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.TakeDamage(DamageByMonster);
        }
    }
}
