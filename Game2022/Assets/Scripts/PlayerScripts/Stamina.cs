using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    private const float staminaDepleteTime = 10f;
    private const float staminaRegenTime = 15f;
    public float stamina;
    public float maxStamina;
    private bool canRun;

    public Stamina(float maxStamina)
    {
        this.maxStamina = maxStamina;
    }

    public bool IsStaminaAvailable(Vector2 movement)
    {
        return movement != Vector2.zero && stamina > 0f;
    }

    public void DrainStamina()
    {
        stamina -= Time.deltaTime / staminaDepleteTime;
        if (!(stamina < 0f)) return;
        canRun = false;
        stamina = 0f;
    }

    public void RechargeStamina()
    {
        if (stamina < 1f)
            stamina += Time.deltaTime / staminaRegenTime;
        else
        {
            canRun = true;
            stamina = 1f;
        }
    }
}
