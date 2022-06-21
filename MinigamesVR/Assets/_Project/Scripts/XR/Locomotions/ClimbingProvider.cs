using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbingProvider : Singleton<LocomotionProvider>
{
    [SerializeField] private List<XRController> climbingHands;
    
    private void FixedUpdate()
    {
        if (climbingHands.Count == 0)
            return;
        
        ComputeClimbMovement();
    }

    private void ComputeClimbMovement()
    {
        foreach (XRController hand in climbingHands)
        {
            // Acumular las velocidades de las manos para luego aplicarlas al transform del Player
        }
        
        Player.Instance.MovePlayerTo(Vector3.zero);
    }
}
