using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    [SerializeField]
    GameObject NPCObject;
    
    void Start()
    {
        Invoke(nameof(Generate), 0.01f);
    }

    void Generate()
    {
        float max = FindObjectOfType<PerlinTerrain>().bounds;
        
        for (int i = 0; i < 4; i++)
        {
            GameObject NPC = Instantiate(NPCObject, transform);
            NPC.transform.position = new Vector3(Random.Range(-max, max), 0, Random.Range(-max, max));
        }
    }
}