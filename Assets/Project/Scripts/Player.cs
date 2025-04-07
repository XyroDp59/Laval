using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Transform leftHand;
    public Transform rightHand;
    
    private HapticImpulsePlayer _hapticImpulseLeft;
    private HapticImpulsePlayer _hapticImpulseRight;
    private void Start()
    {
        Instance = this;
        _hapticImpulseLeft = leftHand.GetComponent<HapticImpulsePlayer>();
        _hapticImpulseRight = rightHand.GetComponent<HapticImpulsePlayer>();
    }

    public void LeftRumble(float amplitude, float duration)
    {
        _hapticImpulseLeft.SendHapticImpulse(amplitude, duration);
    }

    public void RightRumble(float amplitude, float duration)
    {
        _hapticImpulseRight.SendHapticImpulse(amplitude, duration);
    }
}
