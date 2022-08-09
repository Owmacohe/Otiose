using System;
using System.Collections;
using TextToSpeechApi;
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
    bool hasInteracted;
    int NPCNum;
    
    NPCManager manager;
    NPCManager.NPCTypes type;
    NPCManager.Dialogue NPCDialogue;

    TextToSpeech speech;
    AudioSource source;

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
        
        manager = FindObjectOfType<NPCManager>();
        type = NPCManager.NPCTypes.None;

        speech = new TextToSpeech();
        speech.Init();
        source = GetComponent<AudioSource>();
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
        StopAllCoroutines();
        
        if (prompt != null && dialogue != null)
        {
            source.Stop();
            
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
        if (type.Equals(NPCManager.NPCTypes.None))
        {
            NPCNum = manager.currentNPCDialogue;

            type = manager.current;
            NPCDialogue = manager.NPCDialogues[manager.currentNPCDialogue];
            manager.currentNPCDialogue++;

            manager.UI.text = "Perspectives explored: <font=Aromatron SDF>" + manager.currentNPCDialogue + "/3</font>";

            switch (manager.current)
            {
                case NPCManager.NPCTypes.Bhata:
                    manager.current = NPCManager.NPCTypes.Raja;
                    break;
                case NPCManager.NPCTypes.Raja:
                    manager.current = NPCManager.NPCTypes.Shakata;
                    break;
                case NPCManager.NPCTypes.Shakata:
                    break;
            }
        }
        
        speech.SetNewSpeechVoice(speech.GetSpeechVoices()[NPCNum].Id);
        speech.SetNewSpeechSpeed(manager.dialogueSpeeds[NPCNum]);
        
        prompt.SetActive(false);
        dialogue.SetActive(true);

        dialogueText.pageToDisplay = 1;

        if (!hasInteracted)
        {
            dialogueText.text = NPCDialogue.First;
        }
        else
        {
            // TODO: check for hat
            
            dialogueText.text = NPCDialogue.Repeated;
        }
        
        StartCoroutine(ChangePages());
        
        speech.SpeechText(dialogueText.text).OnSuccess((audioData) =>
        {
            AudioClip clip = AudioClip.Create("TTS Clip", audioData.value.Length, 1, speech.samplerate, false);
            clip.SetData(audioData.value, 0);
            source.clip = clip;
            source.Play();
        });

        isInteracting = true;
        hasInteracted = true;
        
        transform.LookAt(player);
    }

    IEnumerator ChangePages()
    {
        for (int i = 0; i < dialogueText.text.Length / 35; i++)
        {
            yield return new WaitForSeconds(5);
                
            dialogueText.pageToDisplay++;
        }
        
        dialogueText.pageToDisplay--;
        source.Stop();
    }
}