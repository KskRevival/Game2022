using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    public static int AmmoCount;
    public TextMeshProUGUI field;

    void Update()
    {
        field.text = $"= {AmmoCount}";
    }
}
