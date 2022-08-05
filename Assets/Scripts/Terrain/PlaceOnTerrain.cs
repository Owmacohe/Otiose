
using System;
using UnityEngine;

public class PlaceOnTerrain : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(Place), 0.01f);
    }

    void Place()
    {
        if (Physics.Raycast(Vector3.up * 100, Vector3.down, out var hit, Mathf.Infinity))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 0.5f, transform.position.z);
        }
    }
}