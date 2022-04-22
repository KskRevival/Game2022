using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.05f;
    public string fullText;
    private string currentText = "";
    public TextMeshProUGUI dialogueText;
    public static bool isCurrentlyTyping = false;

    public void StartTyping(string text)
    {
        isCurrentlyTyping = true;
        fullText = text;
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (var i = 0; i <= fullText.Length; i++)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                dialogueText.text = fullText;
                break;
            }

            currentText = fullText.Substring(0, i);
            Debug.Log(currentText);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        isCurrentlyTyping = false;
    }
}
