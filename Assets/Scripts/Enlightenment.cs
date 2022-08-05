using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enlightenment : MonoBehaviour
{
    [SerializeField]
    GameObject distortion;
    [SerializeField]
    int count;

    GameObject[] distortions;
    Light light;

    void Start()
    {
        distortions = new GameObject[count];
        
        for (int i = 0; i < count; i++)
        {
            GameObject temp = Instantiate(distortion, transform);
            Rotate(temp, GetRandomFloat() * 360f, true);
            Scale(temp, GetRandomFloat(), true);
            temp.GetComponentInChildren<MeshRenderer>().material.color = GetRandomColour(0f, 1f);

            distortions[i] = temp;
        }

        light = GetComponent<Light>();
    }

    void FixedUpdate()
    {
        foreach (GameObject i in distortions)
        {
            Rotate(i, GetRandomFloat() * 20f);
            Scale(i, GetRandomFloat() * 0.5f);
        }

        light.color += GetRandomColour(-1f, 1f);
    }

    void Rotate(GameObject obj, float amount, bool set = false)
    {
        if (set)
        {
            obj.transform.localEulerAngles = GetRandomVector3() * amount;
        }
        else
        {
            obj.transform.localEulerAngles += GetRandomVector3() * amount;
        }
    }

    void Scale(GameObject obj, float amount, bool set = false)
    {
        if (set)
        {
            obj.transform.localScale = GetRandomVector3() * amount;
        }
        else
        {
            obj.transform.localScale += GetRandomVector3() * amount;
        }
    }
    
    float GetRandomFloat(float min = -1f, float max = 1f)
    {
        return Random.Range(min, max);
    }

    Vector3 GetRandomVector3()
    {
        return new Vector3(GetRandomFloat(), GetRandomFloat(), GetRandomFloat());
    }

    Color GetRandomColour(float min, float max)
    {
        return new Color(GetRandomFloat(min, max), GetRandomFloat(min, max), GetRandomFloat(min, max));
    }
}