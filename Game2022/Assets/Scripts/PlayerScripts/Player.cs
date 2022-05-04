using System;
using LabyrinthScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        //Movement
        public MovementData md;

        //HP
        public float health = 20;
        public float maxHealth = 20;

        void Update()
        {
            var moveHorizontal = Input.GetAxisRaw("Horizontal");
            var moveVertical = Input.GetAxisRaw("Vertical");

            md.movement = new Vector2(moveHorizontal, moveVertical);

            md.animator.SetFloat(MovementData.Horizontal, moveHorizontal);
            md.animator.SetFloat(MovementData.Vertical, moveVertical);
            md.animator.SetFloat(MovementData.Speed, md.movement.sqrMagnitude);
        }
        
        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Stamina.IsStaminaAvailable(md.movement))
            {
                md.animator.speed = 2f;
                md.speed = MovementData.RunSpeed;
                Stamina.DrainStamina();
            }
            else
            {
                md.animator.speed = 1f;
                md.speed = MovementData.NormalSpeed;
                Stamina.RechargeStamina();
            }
            var speedMultiplier = md.movement.x != 0 && md.movement.y != 0 ? 0.75f : 1f;
            md.rb.MovePosition(md.rb.position + md.movement * (md.speed * Time.fixedDeltaTime * speedMultiplier));
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
