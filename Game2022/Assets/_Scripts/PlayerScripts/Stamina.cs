using UnityEngine;

namespace PlayerScripts
{
    public static class Stamina
    {
        private const float StaminaDepleteTime = 10f;
        private const float StaminaRegenTime = 15f;
        public static float stamina = 1f;
        public static float maxStamina = 1f;
        public static bool canRun = true;

        public static bool IsStaminaAvailable(Vector2 movement)
        {
            return movement != Vector2.zero && canRun;
        }

        public static void DrainStamina()
        {
            stamina -= Time.deltaTime / StaminaDepleteTime;
            if (stamina > 0f) return;
            canRun = false;
            stamina = 0f;
        }

        public static void RechargeStamina()
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
