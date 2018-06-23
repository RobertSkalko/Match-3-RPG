using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setMaxFPS : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        Application.targetFrameRate = 30;
    }
}