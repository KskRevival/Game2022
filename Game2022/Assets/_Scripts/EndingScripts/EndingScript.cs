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
        if (collision.gameObject.tag == "Player")
        {
            Smiler.SetActive(true);

            StartCoroutine(StartEnding());
        }
    }

    IEnumerator StartEnding()
    {
        yield return new WaitForSeconds(0.5f);

        Ending.SetActive(true);
        Title.SetActive(true);

        yield return new WaitForSeconds(4f);

        Title.SetActive(false);
        Authors.SetActive(true);

        yield return new WaitForSeconds(4f);

        UIScripts.TransitionScript.ToMenu();
    }
}
