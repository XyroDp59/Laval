using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

[RequireComponent(typeof(Animator))]
public class HandAnimator : MonoBehaviour
{
    [SerializeField] private bool isRight;
    private bool _isHolding;
    private bool _isTriggering;
    
    public enum ObjectHeldType
    {
        None,
        ConstructionBlock,
        Egyptian
    }
    
    private ObjectHeldType objectHeld = ObjectHeldType.None;
    
    /// <summary>
    /// Parameters for animaiton on action performed | Method 1
    /// </summary>
    [SerializeField] private InputActionReference controllerActionGrip;

    [SerializeField] private InputActionReference controllerActionTrigger;
    [SerializeField] private InputActionReference controllerActionPrimary;

    #region Method 2 Parameters

    ///// <summary>
    ///// Parameters for realtime animation | Method 2
    ///// </summary>
    //[SerializeField] private XRInputValueReader<Vector2> m_StickInput = new XRInputValueReader<Vector2>("Thumbstick");

    //[SerializeField] private XRInputValueReader<float> m_TriggerInput = new XRInputValueReader<float>("Trigger");
    //[SerializeField] private XRInputValueReader<float> m_GripInput = new XRInputValueReader<float>("Grip");

    #endregion Method 2 Parameters

    private Animator handAnimator;
    
    public bool ClosedFist() => _isTriggering && _isHolding;
    
    public void SetConstructionBlockGrabbed(bool grabbed)
    {
        objectHeld = grabbed ? ObjectHeldType.ConstructionBlock : ObjectHeldType.None;
    }

    public void SetEgyptianGrabbed(bool grabbed)
    {
        objectHeld = grabbed ? ObjectHeldType.Egyptian : ObjectHeldType.None;
    }

    private float ClosureRatio()
    {
        switch (objectHeld)
        {
            case ObjectHeldType.None:
                return 1.0f;
            case ObjectHeldType.ConstructionBlock:
                return 0.25f;
            case ObjectHeldType.Egyptian:
                return 0.75f;
            default:
                return 1.0f;
        }
    }

    public void Rumble(float amplitude, float duration)
    {
        if(isRight) Player.Instance.RightRumble(amplitude, duration);
        else Player.Instance.LeftRumble(amplitude, duration);
    }

    /// <summary>
    /// List of fingers animated when grabbing / using grab action
    /// </summary>
    private readonly List<Finger> grippingFingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky)
    };

    /// <summary>
    /// List of fingers animated when pointing / using trigger action
    /// </summary>
    private readonly List<Finger> pointingFingers = new List<Finger>()
    {
        new Finger(FingerType.Index)
    };

    /// <summary>
    /// List of fingers animated when locomtion / using thumbstick
    /// </summary>
    private readonly List<Finger> primaryFingers = new List<Finger>()
    {
        new Finger(FingerType.Thumb)
    };

    /// <summary>
    /// Add your own hand animation here. For example a fist.
    /// </summary>
    //private readonly List<Finger> fistFingers = new List<Finger>()
    //{
    //    new Finger(FingerType.Thumb),
    //    new Finger(FingerType.Index)
    //    new Finger(FingerType.Middle),
    //    new Finger(FingerType.Ring),
    //    new Finger(FingerType.Pinky)
    //};

    private void Start()
    {
        this.handAnimator = GetComponent<Animator>();
    }

    #region Method 1

    private void OnEnable()
    {
        controllerActionGrip.action.performed += GripAction_performed;
        controllerActionTrigger.action.performed += TriggerAction_performed;
        controllerActionPrimary.action.performed += PrimaryAction_performed;

        controllerActionGrip.action.canceled += GripAction_canceled;
        controllerActionTrigger.action.canceled += TriggerAction_canceled;
        controllerActionPrimary.action.canceled += PrimaryAction_canceled;
    }

    private void OnDisable()
    {
        controllerActionGrip.action.performed -= GripAction_performed;
        controllerActionTrigger.action.performed -= TriggerAction_performed;
        controllerActionPrimary.action.performed -= PrimaryAction_performed;

        controllerActionGrip.action.canceled -= GripAction_canceled;
        controllerActionTrigger.action.canceled -= TriggerAction_canceled;
        controllerActionPrimary.action.performed -= PrimaryAction_canceled;
    }

    private void GripAction_performed(InputAction.CallbackContext obj)
    {
        _isHolding = true;
        StartCoroutine(SetFingerAnimationValues(grippingFingers, 0, true));
        StartCoroutine(AnimateActionInput(grippingFingers));
    }

    private void TriggerAction_performed(InputAction.CallbackContext obj)
    {
        _isTriggering = true;
        StartCoroutine(SetFingerAnimationValues(pointingFingers, 0, true));
        StartCoroutine(AnimateActionInput(pointingFingers));
    }

    private void PrimaryAction_performed(InputAction.CallbackContext obj)
    {
        StartCoroutine(SetFingerAnimationValues(primaryFingers, 0, true));
        StartCoroutine(AnimateActionInput(primaryFingers));
    }

    private void GripAction_canceled(InputAction.CallbackContext obj)
    {
        _isHolding = false;
        StartCoroutine(SetFingerAnimationValues(grippingFingers, 0.0f));
        StartCoroutine(AnimateActionInput(grippingFingers));
    }

    private void TriggerAction_canceled(InputAction.CallbackContext obj)
    {
        _isTriggering = false;
        StartCoroutine(SetFingerAnimationValues(pointingFingers, 0.0f));
        StartCoroutine(AnimateActionInput(pointingFingers));
    }

    private void PrimaryAction_canceled(InputAction.CallbackContext obj)
    {
        StartCoroutine(SetFingerAnimationValues(primaryFingers, 0.0f));
        StartCoroutine(AnimateActionInput(primaryFingers));
    }

    #endregion Method 1

    #region Method 2

    private void Update()
    {
        //if (m_StickInput != null)
        //{
        //    var stickVal = m_StickInput.ReadValue();
        //    SetFingerAnimationValues(primaryFingers, stickVal.y);
        //    AnimateActionInput(primaryFingers);
        //}

        //if (m_TriggerInput != null)
        //{
        //    var triggerVal = m_TriggerInput.ReadValue();
        //    SetFingerAnimationValues(pointingFingers, triggerVal);
        //    AnimateActionInput(pointingFingers);
        //}

        //if (m_GripInput != null)
        //{
        //    var gripVal = m_GripInput.ReadValue();
        //    SetFingerAnimationValues(grippingFingers, gripVal);
        //    AnimateActionInput(grippingFingers);
        //}
    }

    #endregion Method 2

    public IEnumerator SetFingerAnimationValues(List<Finger> fingersToAnimate, float targetValue, bool recalculate = false)
    {
        yield return null;
        if (recalculate)
        {
            targetValue = ClosureRatio();
        }
        foreach (Finger finger in fingersToAnimate)
        {
            finger.target = targetValue;
        }
    }

    public IEnumerator AnimateActionInput(List<Finger> fingersToAnimate)
    {
        yield return null;
        foreach (Finger finger in fingersToAnimate)
        {
            var fingerName = finger.type.ToString();
            var animationBlendValue = finger.target;
            handAnimator.SetFloat(fingerName, animationBlendValue);
        }
    }
}
