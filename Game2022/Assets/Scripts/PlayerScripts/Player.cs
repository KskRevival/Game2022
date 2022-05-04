using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        public GameObject player;
        public float health = 20;
        public float maxHealth = 20;

        public Player() { }

        public Player(GameObject p)
        {
            if (Instance == null) Instance = this;
            else if(Instance != this) Destroy(gameObject);
            Instance.player = p;
        }

        public Player(int health, int MaxHealth)
        {
            
        }
    
        public void TakeDamage(float amount)
        {
            health -= amount;
            if (health > 0) return;
            health = 0;
            Debug.Log("You're dead");
            SceneManager.LoadScene("DeathScene");
        }
    }
}
