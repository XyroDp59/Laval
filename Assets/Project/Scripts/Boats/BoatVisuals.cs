using System;
using UnityEngine;

public class BoatVisuals : MonoBehaviour
{
    private float defaultYPos;

    private void Start()
    {
        defaultYPos = transform.position.y;
    }

    private void Update()
    {
        var vector3 = transform.position;
        vector3.y = defaultYPos;
        transform.position = vector3;
    }
}
