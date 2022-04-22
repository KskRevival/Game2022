using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public GameObject player;

        public static int MaxHealth = 100;
        public static int Health;

        public static HealthBar healthBar;

        void Awake()
        {
            healthBar = player.AddComponent<HealthBar>();
            player = GameObject.Find("Player");
            Health = MaxHealth;
            healthBar.SetMaxHealth(MaxHealth);
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TakeDamage(20);
            }
        }

        public static void TakeDamage(int amount)
        {
            Health -= amount;
            healthBar.SetHealth(Health);
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