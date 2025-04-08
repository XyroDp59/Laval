using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pharaon : MonoBehaviour
{
    [SerializeField] private GameObject goldParticles;
    [SerializeField] private float minSpeed;
    
    private bool rightHanded;
    private HandAnimator rightHandAnimator;
    private HandAnimator leftHandAnimator;
    private HandAnimator currentHandAnimator;
    private Rigidbody rb;
    private bool beingHeld;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnGrab(SelectEnterEventArgs args)
    {
        rightHanded = args.interactorObject.transform.parent.name.Contains("Right");
        currentHandAnimator = rightHanded ? rightHandAnimator : leftHandAnimator;
        currentHandAnimator.SetEgyptianGrabbed(true);
        beingHeld = true;
    }

    public void OnRelease(SelectExitEventArgs args)
    {
        currentHandAnimator.SetEgyptianGrabbed(false);
        beingHeld = false;
    }

    private void Update()
    {
        Debug.Log(rb.velocity.magnitude);
        /*if (!beingHeld)
        {
            goldParticles.SetActive(false);
            return;
        }*/

        goldParticles.SetActive(rb.velocity.magnitude > minSpeed);
    }
}
