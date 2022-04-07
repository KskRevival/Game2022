using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Application.LoadLevel(0);
    }
}
