using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        transform.LookAt(cam.transform);
    }
}