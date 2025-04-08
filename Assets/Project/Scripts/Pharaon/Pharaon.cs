using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pharaon : MonoBehaviour
{
    [SerializeField] private ParticleSystem goldParticles;
    [SerializeField] private CapsuleCollider particlesCollider;
    [SerializeField] private float minSpeed;
    
    private bool rightHanded;
    private HandAnimator rightHandAnimator;
    private HandAnimator leftHandAnimator;
    private HandAnimator currentHandAnimator;
    private XRControllerSpeed controller;
    private bool beingHeld;
    
    private void Start()
    {
        rightHandAnimator = GameObject.FindGameObjectWithTag("RightHand").GetComponent<HandAnimator>();
        leftHandAnimator = GameObject.FindGameObjectWithTag("LeftHand").GetComponent<HandAnimator>();
    }

    public void OnGrab(SelectEnterEventArgs args)
    {
        rightHanded = args.interactorObject.transform.parent.name.Contains("Right");
        currentHandAnimator = rightHanded ? rightHandAnimator : leftHandAnimator;
        currentHandAnimator.SetEgyptianGrabbed(true);
        controller = currentHandAnimator.gameObject.transform.parent.GetComponent<XRControllerSpeed>();
        beingHeld = true;
    }

    public void OnRelease(SelectExitEventArgs args)
    {
        currentHandAnimator.SetEgyptianGrabbed(false);
        beingHeld = false;
    }

    private void Update()
    {
        if (!beingHeld)
        {
            particlesCollider.enabled = false;
            var module = goldParticles.emission;
            module.enabled = false;
            return;
        }

        if (controller.GetControllerSpeed().magnitude > minSpeed) StartCoroutine(ParticlesActivation());
    }

    private IEnumerator ParticlesActivation()
    {
        var module = goldParticles.emission;
        module.enabled = true;
        particlesCollider.enabled = true;
        yield return new WaitForSeconds(2f);
        module.enabled = false;
        particlesCollider.enabled = false;
    }
}
