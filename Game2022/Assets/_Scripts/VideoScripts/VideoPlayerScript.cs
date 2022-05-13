using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayerScript : MonoBehaviour
{
    public GameObject DeathMenu;
    public GameObject VideoPlayerUI;

    public void Start()
    {
        DeathMenu.SetActive(false);
        StartCoroutine(WaitForTwoSeconds());
    }

    private IEnumerator WaitForTwoSeconds()
    {
        yield return new WaitForSeconds(2f);

        DeathMenu.SetActive(true);
        VideoPlayerUI.SetActive(false);
    }
}
