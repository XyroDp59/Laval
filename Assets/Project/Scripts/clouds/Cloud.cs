using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float speedGoal = 10f;

    private float maxSpeed;
     
    private void OnTriggerStay(Collider collision)
    {
        XRControllerSpeed controller;
        if (collision.gameObject.TryGetComponent(out controller))
        {
            if(controller.GetControllerSpeed().magnitude > maxSpeed) maxSpeed = controller.GetControllerSpeed().magnitude;
            Debug.Log(maxSpeed);
            if (maxSpeed > speedGoal)
            {
                Debug.Log("killedCloud");
                Destroy(gameObject);
            }
        }
    }
}
