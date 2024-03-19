using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;



public class Haptics : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float intensity;
    public float duration;
    bool isHaptic = false;
    XRBaseController xRController;

    private void Start()
    {
        //XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        //interactable.activated.AddListener(TriggerHaptic);
    }
    private void Update()
    {
        if (isHaptic)
        {
            xRController.SendHapticImpulse(intensity, duration);
        }
    }
    public void SetHaptic(XRBaseControllerInteractor Xinteractable)
    {
        
        xRController = Xinteractable.xrController;
        TriggerHaptic(xRController);
    }

    //public void TriggerHaptic(BaseInteractionEventArgs e)
    //{
    //    if(e.interactableObject is XRBaseControllerInteractor controllerInteractor)
    //    {
    //        TriggerHaptic(controllerInteractor.xrController);
    //    }
    //}
    public void TriggerHaptic(XRBaseController controller)
    {
        xRController = controller;
        isHaptic=true;
    }
    public void Stop()
    {
        isHaptic = false;
    }
}
