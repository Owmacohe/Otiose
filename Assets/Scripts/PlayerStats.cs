using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject hatObject;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}