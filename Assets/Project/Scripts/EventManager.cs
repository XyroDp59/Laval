using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] List<GameEvent> events = new List<GameEvent>();
    [SerializeField] public Quarry quarry;
    public static EventManager instance;

    [SerializeField] List<BoatSpawner> boatSpawns;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public enum EventID
    {
        RAIN,
        BOAT,
        NIGHT,
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiateEvents());
    }

    //boucle recursive, qui termine pas
    IEnumerator InstantiateEvents()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("spawned new event");
        GameEvent g = events[Random.Range(0, events.Count)];

        if(g.Eventid == EventID.BOAT) {
            Transform t = boatSpawns[Random.Range(0, boatSpawns.Count)].transform;
            if(t.childCount == 0) {
                GameEvent g1 = Instantiate(g, t);
                g1.AddEventToQuarry();
            }
        }
        else Instantiate(g, transform);
        
        StartCoroutine(InstantiateEvents());
    }
}
