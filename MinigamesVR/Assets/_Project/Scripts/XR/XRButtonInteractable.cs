using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(AudioSource))]
public class XRButtonInteractable : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Vector3 buttonDisplacement;
    [SerializeField] bool isPressed = false;

    private Vector3 _originalPosition;
    private AudioSource _audioSource;
    private GameObject _currentPresser;

    public delegate void PressedButton();
    public event PressedButton OnPressedButton;

    public delegate void ReleasedButton();
    public event ReleasedButton OnReleasedButton;

    private void Start()
    {
        _originalPosition = button.transform.localPosition;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPressed)
            return;
        
        button.transform.localPosition = new Vector3(0, 0.05f, 0);
        _currentPresser = other.gameObject;
        isPressed = true;
        
        if (_audioSource)
            _audioSource.Play();
        
        OnPressedButton?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _currentPresser)
            return;

        button.transform.localPosition = _originalPosition;
        _currentPresser = null;
        isPressed = false;
        
        OnReleasedButton?.Invoke();
    }
}
