using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetInteractable : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        IXRSelectInteractor interactor = args.interactorObject;
        IXRSelectInteractable interactable = interactor.firstInteractableSelected;
        
        bool isDirectInteractor = interactor is XRDirectInteractor;

        attachTransform.position = isDirectInteractor ? interactor.GetAttachTransform(interactable).position : transform.position;
        attachTransform.rotation = isDirectInteractor ? interactor.GetAttachTransform(interactable).rotation : transform.rotation;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
    }
}
