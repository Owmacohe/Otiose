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
    [SerializeField]
    Material waterMaterial;
    [SerializeField]
    bool addHills = true;
    [SerializeField]
    bool randomSeed = true;
    [SerializeField, Range(4.1f, 4.5f)]
    float seed;

    List<GameObject> tiles, waterTiles;
    int offset;
    [HideInInspector] public float bounds;

    void Start()
    {
        bounds = count * 2;
        offset = (count / 2) * 10;
        
        tiles = new List<GameObject>();

        if (randomSeed)
        {
            seed = Random.Range(4.1f, 4.5f);   
        }

        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                Vector3 tempPosition = new Vector3((i * 10) - offset, 0, (j * 10) - offset);

                if (Vector3.Distance(tempPosition, transform.position) <= (count * 10) / 3f)
                {
                    GameObject temp = CreateGround(tempPosition);

                    if (!addHills)
                    {
                        temp.transform.localPosition += Vector3.up * 4.8f;
                    }
                    
                    Generate(temp, seed, true);
                    
                    tiles.Add(temp);
                }
            }
        }

        waterTiles = new List<GameObject>();

        for (int k = 0; k < count * 1.5f; k++)
        {
            for (int l = 0; l < count * 1.5f; l++)
            {
                Vector3 tempPosition = new Vector3((k * 10) - (offset * 1.5f), 0, (l * 10) - (offset * 1.5f));
                
                if (Vector3.Distance(tempPosition, transform.position) <= (count * 10) / 1.5f)
                {
                    GameObject temp = CreateGround(tempPosition);
                    temp.GetComponent<MeshRenderer>().material = waterMaterial;
                    Destroy(temp.GetComponent<MeshCollider>());
                    temp.transform.position += Vector3.up * 4.5f;
                
                    waterTiles.Add(temp);   
                }
            }
        }

        if (addHills)
        {
            AddHills();   
        }
    }

    void FixedUpdate()
    {
        foreach (GameObject i in waterTiles)
        {
            Generate(i, Time.time / 3f, false);
        }
    }

    GameObject CreateGround(Vector3 pos)
    {
        GameObject temp = Instantiate(groundObject, transform);
        temp.transform.localPosition = pos;

        return temp;
    }

    void Generate(GameObject obj, float seed, bool addCollider)
    {
        MeshFilter mf = obj.GetComponent<MeshFilter>();
        Vector3[] vertices = mf.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = Mathf.PerlinNoise(seed + vertices[i].x + obj.transform.position.x, seed + vertices[i].z + obj.transform.position.z);
        }

        mf.mesh.vertices = vertices;
        mf.mesh.RecalculateBounds();

        if (addCollider)
        {
            Destroy(obj.GetComponent<MeshCollider>());
            obj.AddComponent<MeshCollider>();   
        }
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