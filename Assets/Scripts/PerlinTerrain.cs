using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerlinTerrain : MonoBehaviour
{
    [SerializeField]
    GameObject groundObject;
    [SerializeField]
    int count;

    List<GameObject> tiles;
    int offset;

    void Start()
    {
        tiles = new List<GameObject>();
        
        float seed = Random.Range(4.1f, 4.5f);
        offset = (count / 2) * 10;

        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                GameObject temp = Instantiate(groundObject, transform);
                temp.transform.localPosition = new Vector3((i * 10) - offset, 0, (j * 10) - offset);

                if (Vector3.Distance(temp.transform.position, transform.position) > (count * 10) / 2f)
                {
                    Destroy(temp);
                }
                else
                {
                    Generate(temp, seed);
                    tiles.Add(temp);
                }
            }
        }
        
        AddHills();
    }

    void Generate(GameObject obj, float seed)
    {
        MeshFilter mf = obj.GetComponent<MeshFilter>();
        Vector3[] vertices = mf.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = Mathf.PerlinNoise(seed + vertices[i].x + obj.transform.position.x, seed + vertices[i].z + obj.transform.position.z);
        }

        mf.mesh.vertices = vertices;
        mf.mesh.RecalculateBounds();
        
        Destroy(obj.GetComponent<MeshCollider>());
        obj.AddComponent<MeshCollider>();
    }

    void AddHills()
    {
        for (int i = 0; i < count * 5; i++)
        {
            Vector3 pos = GetRandomVector3();

            foreach (GameObject j in tiles)
            {
                MeshFilter mf = j.GetComponent<MeshFilter>();
                Vector3[] vertices = mf.mesh.vertices;

                for (int k = 0; k < vertices.Length; k++)
                {
                    vertices[k].y += 3f / Vector3.Distance(j.transform.TransformPoint(vertices[k]), pos);
                }

                mf.mesh.vertices = vertices;
                mf.mesh.RecalculateBounds();
                
                Destroy(j.GetComponent<MeshCollider>());
                j.AddComponent<MeshCollider>();
            }
        }
    }

    Vector3 GetRandomVector3()
    {
        float size = count * 10;
        float tempOffset = offset;
        
        return new Vector3(
            Random.Range(0f, size) - tempOffset,
            0,
            Random.Range(0, size) - tempOffset
        );
    }
}