using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    float walkChance = 5;
    [SerializeField]
    float speed = 0.01f;
    
    Rigidbody rb;
    Animator anim;
    PerlinTerrain terrain;

    Vector3 direction;
    bool isWalking;

    GameObject dialogue, prompt;
    TMP_Text dialogueText;
    [HideInInspector] public bool isInteracting;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        terrain = FindObjectOfType<PerlinTerrain>();

        Canvas[] canvases = GetComponentsInChildren<Canvas>();
        
        dialogue = canvases[0].gameObject;
        dialogueText = dialogue.GetComponentInChildren<TMP_Text>();
        dialogue.SetActive(false);

        prompt = canvases[1].gameObject;
        prompt.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!isInteracting)
        {
            if (!isWalking && Random.Range(0, 100) <= walkChance)
            {
                isWalking = true;
                anim.SetBool("isWalking", true);
                direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * speed;
            
                Invoke(nameof(StopWalking), Random.Range(1f, 5f));
            }
        
            if (isWalking)
            {
                if (Vector3.Distance(Vector3.zero, transform.position) > terrain.bounds)
                {
                    direction = (Vector3.zero - transform.position).normalized / 100f;
                }
            
                rb.transform.position += direction;
                transform.LookAt(transform.position + direction);
            }   
        }
        else
        {
            StopWalking();
        }
    }

    void StopWalking()
    {
        isWalking = false;
        anim.SetBool("isWalking", false);
    }

    public void Hide()
    {
        if (prompt != null && dialogue != null)
        {
            prompt.SetActive(false);
            dialogue.SetActive(false);
        
            isInteracting = false;   
        }
    }

    public void Prompt()
    {
        if (prompt != null)
        {
            prompt.SetActive(true);   
        }
    }

    public void Interact(Transform player)
    {
        prompt.SetActive(false);
        dialogue.SetActive(true);

        isInteracting = true;
        
        transform.LookAt(player);

        dialogueText.text = "test";
    }
}