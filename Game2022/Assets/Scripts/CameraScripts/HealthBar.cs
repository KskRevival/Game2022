using System;
using System.Collections;
using System.Collections.Generic;
using SaveScripts;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static Slider slider;
    
    public static void SetHealth(int health)
    {
        Debug.Log("SH");
        if (slider == null) return;
        Debug.Log("HP Changed");
        slider.value = health;
    }

    public static void SetMaxHealth(int health)
    {
        if (slider == null) return;
        slider.maxValue = health;
        slider.value = health;
    }
}