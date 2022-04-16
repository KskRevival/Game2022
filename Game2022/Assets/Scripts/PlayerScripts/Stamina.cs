using UnityEngine;

namespace PlayerScripts
{
    public class Stamina : MonoBehaviour
    {
        private const float StaminaDepleteTime = 10f;
        private const float StaminaRegenTime = 15f;
        public float stamina;
        public float maxStamina;
        public bool canRun = true;

        public Stamina(float maxStamina)
        {
            this.maxStamina = maxStamina;
        }

        public bool IsStaminaAvailable(Vector2 movement)
        {
            return movement != Vector2.zero && canRun;
        }

        public void DrainStamina()
        {
            stamina -= Time.deltaTime / StaminaDepleteTime;
            if (stamina > 0f) return;
            canRun = false;
            stamina = 0f;
        }

        public void RechargeStamina()
        {
            if (stamina < maxStamina)
                stamina += Time.deltaTime / StaminaRegenTime;
            else
            {
                canRun = true;
                stamina = maxStamina;
            }
        }
    }
}
