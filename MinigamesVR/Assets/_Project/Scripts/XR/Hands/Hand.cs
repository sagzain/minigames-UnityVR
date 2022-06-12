using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetGripValue(float value)
    {
        _animator.SetFloat("Grip", value);
    }

    public void SetTriggerValue(float value)
    {
        _animator.SetFloat("Trigger", value);
    }
}
