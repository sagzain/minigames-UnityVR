using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRClimbInteractable : XRBaseInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

     
        if (!(args.interactorObject is XRDirectInteractor)) return;
        
        var controller = args.interactorObject.transform.GetComponent<ActionBasedController>();
        controller.SendHapticImpulse(1, 1f);
        ClimbingProvider.Instance.SetClimbingHand(controller);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        if (!(args.interactorObject is XRDirectInteractor)) return;

        var controller = args.interactorObject.transform.GetComponent<ActionBasedController>();
        ClimbingProvider.Instance.RemoveClimbingHand(controller);
    }
}