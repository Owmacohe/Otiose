using System;
using UnityEngine;

public class Preserver : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}