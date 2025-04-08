using System;
using UnityEngine;

public class Pharaon : MonoBehaviour
{
    [SerializeField] private GameObject goldParticles;
    [SerializeField] private float minSpeed;
    
    private Rigidbody rb;
    private bool beingHeld;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnGrab()
    {
        beingHeld = true;
    }

    public void OnRelease()
    {
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
