using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
