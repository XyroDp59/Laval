using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float speedGoal = 10f;
    [SerializeField] private AnimationCurve fadeOut;
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private Renderer meshRenderer;

    private float maxSpeed;

    private void Start()
    {
        StartCoroutine(DestroyCloud());
    }

    private void OnTriggerStay(Collider collision)
    {
        XRControllerSpeed controller;
        if (collision.gameObject.TryGetComponent(out controller))
        {
            if(controller.GetControllerSpeed().magnitude > maxSpeed) maxSpeed = controller.GetControllerSpeed().magnitude;
            //Debug.Log(maxSpeed);
            if (maxSpeed > speedGoal)
            {
                StartCoroutine(DestroyCloud());
            }
        }
    }

    IEnumerator DestroyCloud()
    {
        yield return null;
        Destroy(gameObject);
        /*
        float t = 0;
        while(t < fadeOutDuration)
        {
            yield return null;
            t += Time.deltaTime;

            meshRenderer.material.SetColor("_BaseColor", new Color(255,255,255, fadeOut.Evaluate(t)));
            Debug.Log(126 * fadeOut.Evaluate(t/fadeOutDuration));
        }
        Destroy(gameObject);
        */
    }
}
