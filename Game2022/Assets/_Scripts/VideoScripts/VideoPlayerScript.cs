using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayerScript : MonoBehaviour
{
    private const float delay = 2f;

    public GameObject DeathMenu;
    public GameObject VideoPlayerUI;

    public void Start()
    {
        DeathMenu.SetActive(false);
        StartCoroutine(PlayAnimWithDelay(delay));
    }

    private IEnumerator PlayAnimWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        DeathMenu.SetActive(true);
        VideoPlayerUI.SetActive(false);
    }
}
