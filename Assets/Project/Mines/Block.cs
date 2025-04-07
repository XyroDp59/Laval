using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Block : MonoBehaviour
{
    private bool rightHanded;
    private HandAnimator rightHandAnimator;
    private HandAnimator leftHandAnimator;
    private HandAnimator currentHandAnimator;

    private void Start()
    {
        rightHandAnimator = GameObject.FindGameObjectWithTag("RightHand").GetComponent<HandAnimator>();
        leftHandAnimator = GameObject.FindGameObjectWithTag("LeftHand").GetComponent<HandAnimator>();
    }

    public void OnBlockGrabbed(SelectEnterEventArgs args)
    {
        rightHanded = args.interactorObject.transform.parent.name.Contains("Right");
        currentHandAnimator = rightHanded ? rightHandAnimator : leftHandAnimator;
        currentHandAnimator.SetConstructionBlockGrabbed(true);
    }

    public void OnBlockReleased(SelectExitEventArgs args)
    {
        currentHandAnimator.SetConstructionBlockGrabbed(false);
    }
}
