using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public Vector3 position;
    public float range;
    [SerializeField] public EventManager.EventID Eventid;
    public int id;
    Quarry q;


    // Start is called before the first frame update
    void Start()
    {
        q = EventManager.instance.quarry;
        id = (int) Eventid;
    }

    public void AddEventToQuarry()
    {
        q.PointOfInterests.Add( this );
        Debug.Log(q);
        Debug.Log(q.PointOfInterests[q.PointOfInterests.Count - 1]);
    }

    public void RemoveEventFromQuarry()
    {
        foreach (var x in q.PointOfInterests)
        {
            if(Vector3.Distance(x.position, this.position) < 1 && x.id == (int) this.id) { 
                q.PointOfInterests.Remove(x);
            }
        }
    }

    public void SetRange(float range)
    {
        this.range = range;
    }
    public void SetPosition()
    {
        position = transform.position;
    }
}
