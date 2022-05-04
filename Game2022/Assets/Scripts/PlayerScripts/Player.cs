using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public static GameObject player;
        public static float Health = 20, MaxHealth = 20;

        public Player(GameObject p)
        {
            player = p;
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
