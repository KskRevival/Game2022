using System;
using System.Collections;
using System.Collections.Generic;
using LabyrinthScripts;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    private void Awake()
    {
        if (GameManager.Instance == null)
            Instantiate(gameManager);
    }
}
