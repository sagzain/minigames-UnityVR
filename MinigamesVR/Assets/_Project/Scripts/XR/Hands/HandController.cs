using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    [SerializeField] private Hand _hand;
   
    private ActionBasedController _controller;

    void Start()
    {
        _controller = GetComponent<ActionBasedController>();
    }

    void Update()
    {
        _hand.SetGripValue(_controller.selectActionValue.action.ReadValue<float>());
        _hand.SetTriggerValue(_controller.activateActionValue.action.ReadValue<float>());
    }
}
