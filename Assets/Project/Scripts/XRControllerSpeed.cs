using UnityEngine;
using UnityEngine.XR;

public class XRControllerSpeed : MonoBehaviour
{
    public XRNode controllerNode = XRNode.RightHand; // or LeftHand
    private InputDevice controller;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    public Vector3 GetControllerSpeed()
    {
        if (controller.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity))
        {
            return velocity;
        }
        else return Vector3.zero;
    }
}