using System;
using TMPro;
using UnityEngine;

public class EnlightenmentController : MonoBehaviour
{
    [HideInInspector] public bool isInteracting;
    
    GameObject prompt;
    NPCManager manager;
    AudioSource source;

    void Start()
    {
        prompt = GetComponentInChildren<Canvas>().gameObject;
        prompt.SetActive(false);
        
        manager = FindObjectOfType<NPCManager>();

        source = GetComponent<AudioSource>();
    }
    
    public void Hide()
    {
        if (prompt != null)
        {
            prompt.SetActive(false);  
        }
    }

    public void Prompt()
    {
        if (prompt != null)
        {
            prompt.SetActive(true);   
        }
    }

    public void Interact()
    {
        Hide();
        
        isInteracting = true;
        
        TMP_Text temp = GameObject.FindWithTag("Enlightenment").GetComponent<TMP_Text>();
        temp.enabled = true;
        temp.text = manager.GetEnlightenment();
        
        source.Play();
        
        Invoke(nameof(HideEnlightenment), 6);
    }

    void HideEnlightenment()
    {
        manager.HideEnlightenment();
        Destroy(gameObject);
    }
}