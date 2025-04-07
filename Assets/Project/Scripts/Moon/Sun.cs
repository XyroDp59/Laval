using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private Transform moonParent;

    private void Update()
    {
        var angles = transform.rotation.eulerAngles;
        angles.z = moonParent.rotation.eulerAngles.z + 180;
        var rotation = transform.rotation;
        rotation.eulerAngles = angles;
        transform.rotation = rotation;
    }
}
