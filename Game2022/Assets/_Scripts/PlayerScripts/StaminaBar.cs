using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Image staminaBarFilling;
    [SerializeField] private Image staminaBar;

    private Color blueColor = new Color(0.04939478f, 0.1323116f, 0.7075472f);
    private Color redColor = new Color(0.7058824f, 0.0509804f, 0.1377635f);

    private bool isFaded = true;

    private void Start()
    {
        staminaBarFilling.color = new Color(blueColor.r,
                                            blueColor.g,
                                            blueColor.b, 
                                            0f);

        staminaBar.color = new Color(staminaBar.color.r,
                                     staminaBar.color.g,
                                     staminaBar.color.b,
                                     0f);
    }

    private void Update()
    {
        Debug.Log(isFaded);

        staminaBarFilling.fillAmount = Stamina.stamina;

        if (!isFaded) staminaBarFilling.color = Stamina.canRun ? blueColor : redColor;

        if (!isFaded && Stamina.stamina == 1f) StartCoroutine(FadeTo(0f));
        else if (isFaded && Stamina.stamina != 1f) StartCoroutine(FadeTo(1f));

        isFaded = Stamina.stamina == 1f;
    }

    IEnumerator FadeTo(float alpha)
    {
        for (float i = 0; i <= 1; i += Time.deltaTime * 4)
        {
            var alphaValue = alpha == 1f ? i : 1 - i;

            staminaBarFilling.color = new Color(staminaBarFilling.color.r,
                                                staminaBarFilling.color.g,
                                                staminaBarFilling.color.b,
                                                alphaValue);

            staminaBar.color = new Color(staminaBar.color.r,
                                     staminaBar.color.g,
                                     staminaBar.color.b,
                                     alphaValue);

            yield return null;
        }
    }
}
