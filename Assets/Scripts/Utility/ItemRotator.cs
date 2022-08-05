using System;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    [SerializeField]
    float speed = 0.5f;

    void Start()
    {
        //transform.position = GetComponent<MeshFilter>().mesh.bounds.center;
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, speed);
    }
}