using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRMagazineSocket : XRSocketInteractor
{
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        args.interactableObject.transform.GetComponent<MeshCollider>().isTrigger = true;
    }
    
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        args.interactableObject.transform.GetComponent<MeshCollider>().isTrigger = false;

    }
}
