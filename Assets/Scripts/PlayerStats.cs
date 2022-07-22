using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector]
    public GameObject hatObject;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}