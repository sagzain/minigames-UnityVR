using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class XRButtonInteractable : MonoBehaviour
{

    [SerializeField] private GameObject button;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] bool isPressed = false;
    
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            if (audioSource)
                audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
