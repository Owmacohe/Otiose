using System;
using UnityEngine;

public class SetAnim : MonoBehaviour
{
    [SerializeField]
    string[] parameters;
    
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        foreach (string i in parameters)
        {
            anim.SetBool(i, true);
        }
    }
}