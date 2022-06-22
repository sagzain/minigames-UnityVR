using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(AudioSource))]
public class XRButtonInteractable : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Vector3 buttonDisplacement;
    [SerializeField] private AudioClip pressedSound;

    private Vector3 _originalPosition;
    private AudioSource _audioSource;
    private GameObject _currentPresser;
    private bool _isPressed = false;

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
        if (_isPressed)
            return;

        button.transform.localPosition = buttonDisplacement;
        _currentPresser = other.gameObject;
        _isPressed = true;

        OnPressedButton?.Invoke();

        if (_audioSource)
            _audioSource.PlayOneShot(pressedSound);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _currentPresser)
            return;

        button.transform.localPosition = _originalPosition;
        _currentPresser = null;
        _isPressed = false;

        OnReleasedButton?.Invoke();
    }
}