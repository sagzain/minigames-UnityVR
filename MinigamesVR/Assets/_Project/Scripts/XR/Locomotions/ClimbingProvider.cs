using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbingProvider : Singleton<LocomotionProvider>
{
    [SerializeField] private XRController climbingHand;
    
    private void FixedUpdate()
    {
        if (!climbingHand)
            return;
        
        ComputeClimbMovement();
    }

    private void ComputeClimbMovement()
    {

    }
}
