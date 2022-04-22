using System.Collections;
using System.Collections.Generic;
using SaveScripts;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;

    public HealthBar(int health)
    {
        SetMaxHealth(health);
        SetHealth(health);
    }

    public void SetHealth(int health)
    {
        if (slider == null) return;
        Debug.Log("Ilya loh");
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        if (slider == null) return;
        slider.maxValue = health;
        slider.value = health;
    }
}