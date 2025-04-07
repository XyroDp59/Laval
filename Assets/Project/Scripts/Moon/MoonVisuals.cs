using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonVisuals : MonoBehaviour
{
    [SerializeField] private Transform moonParent;

    private void Update()
    {
        var rotation = transform.rotation;
        var angles = rotation.eulerAngles;
        angles.x = 0;
        angles.y = 0;
        angles.z = moonParent.rotation.eulerAngles.z;
        rotation.eulerAngles = angles;
        transform.rotation = rotation;
    }
}
