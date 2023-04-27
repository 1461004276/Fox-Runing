using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public bool isTwoJump;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
