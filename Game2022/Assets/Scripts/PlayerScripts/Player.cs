using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public static readonly GameObject player = GameObject.Find("Player");
        public static float Health, MaxHealth;

        public Player(float maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
        }
    
        public static void TakeDamage(float amount)
        {
            Health -= amount;
            if (Health > 0) return;
            Health = 0;
            Debug.Log("You're dead");
            SceneManager.LoadScene("DeathScene");
        }
    }
}
