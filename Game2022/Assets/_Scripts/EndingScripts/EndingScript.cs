using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public GameObject Smiler;

    public GameObject Ending;
    public GameObject Title;
    public GameObject Authors;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Smiler.SetActive(true);

            StartCoroutine(StartEnding());
        }
    }

    IEnumerator StartEnding()
    {
        yield return new WaitForSeconds(0.4f);

        Ending.SetActive(true);
        Title.SetActive(true);

        yield return new WaitForSeconds(3f);

        Title.SetActive(false);
        Authors.SetActive(true);

        yield return new WaitForSeconds(3f);

        UIScripts.TransitionScript.ToMenu();
    }
}
