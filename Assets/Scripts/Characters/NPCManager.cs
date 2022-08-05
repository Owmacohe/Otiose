using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    [SerializeField]
    GameObject NPCObject;
    [SerializeField]
    TextAsset[] dialogues;
    [SerializeField]
    Material[] materials;
    [SerializeField]
    GameObject enlightenmentObject;
    [SerializeField]
    int enlightenments;
    
    public enum NPCTypes { None, Bhata, Raja, Shakata }
    [HideInInspector] public NPCTypes current;

    [HideInInspector] public int currentNPCDialogue;
    [HideInInspector] public Dialogue[] NPCDialogues;
    [HideInInspector] public int[] dialogueSpeeds;

    public struct Dialogue
    {
        public string First, Repeated, Hat;
    }
    
    void Start()
    {
        current = NPCTypes.Bhata;
        currentNPCDialogue = 0;
        dialogueSpeeds = new [] { -5, -2, -3 };
        
        NPCDialogues = new[]
        {
            ParseDialogue(dialogues[0]),
            ParseDialogue(dialogues[1]),
            ParseDialogue(dialogues[2])
        };
        
        Invoke(nameof(GenerateNPCs), 0.05f);
        Invoke(nameof(GenerateEnlightenment), 0.1f);
    }

    void GenerateNPCs()
    {
        float max = FindObjectOfType<PerlinTerrain>().bounds;
        
        for (int i = 0; i < 3; i++)
        {
            GameObject NPC = Instantiate(NPCObject, transform);
            NPC.transform.position = new Vector3(Random.Range(-max, max), 0, Random.Range(-max, max));
            NPC.GetComponentInChildren<Renderer>().material = materials[i];
        }
    }

    void GenerateEnlightenment()
    {
        float max = FindObjectOfType<PerlinTerrain>().bounds;
        
        for (int i = 0; i < enlightenments; i++)
        {
            Instantiate(enlightenmentObject, null).transform.position = new Vector3(Random.Range(-max, max), 0, Random.Range(-max, max));
        }
    }

    Dialogue ParseDialogue(TextAsset file)
    {
        int progress = -1;
        Dialogue temp = default;
        
        foreach (string i in file.text.Split('\n'))
        {
            if (!i.Trim().Equals(""))
            {
                if (i.Trim() is "[FIRST]" or "[REPEATED]" or "[HAT]")
                {
                    progress++;
                }
                else
                {
                    string content = " " + i.Trim();
                    
                    switch (progress)
                    {
                        case 0:
                            temp.First += content;
                            break;
                        case 1:
                            temp.Repeated += content;
                            break;
                        case 2:
                            temp.Hat += content;
                            break;
                    }   
                }
            }
        }

        return temp;
    }
}