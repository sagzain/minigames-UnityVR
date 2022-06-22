using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;

public class ClimbingProvider : LocomotionProvider
{
    private static ClimbingProvider _instance;
    public static ClimbingProvider Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ClimbingProvider>();
            }

            return _instance;
        }
    }

    private ActionBasedController _leftClimbingHand;
    private Vector3 _leftLastPos;

    private ActionBasedController _rightClimbingHand;
    private Vector3 _rightLastPos;

    [SerializeField] private InputActionReference leftControllerPosAction;
    [SerializeField] private InputActionReference rightControllerPosAction;
    
    private void Update()
    {
        if (!_rightClimbingHand && !_leftClimbingHand)
            return;
        
        ComputeClimbMovement();
    }

    public void SetClimbingHand(ActionBasedController controller)
    {
        if (controller.name == "LeftHand Controller")
        {
            _leftClimbingHand = controller;
            // _leftLastPos = _leftClimbingHand.positionAction.action.ReadValue<Vector3>();
            _leftLastPos = leftControllerPosAction.action.ReadValue<Vector3>();
        }
        if (controller.name == "RightHand Controller")
        {
            _rightClimbingHand = controller;
            // _rightLastPos = _rightClimbingHand.positionAction.action.ReadValue<Vector3>();
            _rightLastPos = rightControllerPosAction.action.ReadValue<Vector3>();
        }
        
    }

    public void RemoveClimbingHand(ActionBasedController controller)
    {
        if (_leftClimbingHand && _leftClimbingHand.name == controller.name)
        {
            _leftClimbingHand = null;
            _leftLastPos = Vector3.zero;
        }

        if (_rightClimbingHand && _rightClimbingHand.name == controller.name)
        {
            _rightClimbingHand = null;
            _rightLastPos = Vector3.zero;
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void ComputeClimbMovement()
    {
        Vector3 movement = Vector3.zero;

        if (_leftClimbingHand)
        {
            Vector3 controllerPos = leftControllerPosAction.action.ReadValue<Vector3>();//_leftClimbingHand.positionAction.action.ReadValue<Vector3>();
            Vector3 velocity = controllerPos - _leftLastPos;

            if (velocity != Vector3.zero)
                movement -= velocity;
        
            _leftLastPos = controllerPos;
        }

        if (_rightClimbingHand)
        {
            Vector3 controllerPos = rightControllerPosAction.action.ReadValue<Vector3>();//_rightClimbingHand.positionAction.action.ReadValue<Vector3>();
            Vector3 velocity = controllerPos - _rightLastPos;

            if (velocity != Vector3.zero)
                movement -= velocity;
        
            _rightLastPos = controllerPos;
        }
        
        // var playerRotation = Player.Instance.transform.rotation;
        Player.Instance.MovePlayerTo(movement);
    }
}