using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEvent))]
public class MoonVisuals : MonoBehaviour
{
    [SerializeField] private Transform moonParent;
    [SerializeField] private GameEvent gameEvent;

    private bool isNight = false;

    private void Awake()
    {
        gameEvent = GetComponent<GameEvent>();
    }

    private void Update()
    {
        var rotation = transform.rotation;
        var angles = rotation.eulerAngles;
        angles.x = 0;
        angles.y = 0;
        angles.z = moonParent.rotation.eulerAngles.z;
        rotation.eulerAngles = angles;
        transform.rotation = rotation;

        if(transform.position.y < 0 && isNight)
        {
            // le jour
            isNight = false;
            gameEvent.RemoveEventFromQuarry();
            //retirer event
        }
        else if ( !isNight )
        {
            isNight = true;
            gameEvent.AddEventToQuarry();
            //add event
        }
    }
}
