using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public int MaxHealth = 100;
        public int Health;


        void Awake()
        {
            Health = MaxHealth;
            HealthBar.SetMaxHealth(MaxHealth);
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TakeDamage(20);
            }
        }

        public void TakeDamage(int amount)
        {
            Debug.Log("TD");
            Health -= amount;
            HealthBar.SetHealth(Health);
            if (Health > 0) return;
            Health = 0;
            Debug.Log("You're dead");
            SceneManager.LoadScene("DeathScene");
        }



        /*public static float Health, MaxHealth;

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
        }*/
    }
}