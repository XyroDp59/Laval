using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumulonimbus : MonoBehaviour
{
    [SerializeField] Cloud cloudPrefab;
    [SerializeField] float radius;
    [SerializeField] float number;
    [SerializeField, Range(0,1)] private float minCloudNeededRatio = 0.1f;
    GameEvent gameEvent;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("cloud");
        float distance = Random.Range(100, 300);
        float theta1 = Random.Range(0, 2*Mathf.PI);

        transform.position = new Vector3(500 + distance * Mathf.Cos(theta1), transform.position.y, 500 + distance * Mathf.Sin(theta1));

        gameEvent = gameObject.GetComponent<GameEvent>();

        gameEvent.SetRange(150);
        gameEvent.SetPosition();
        Debug.Log(gameEvent.range);
        Debug.Log(gameEvent.position);



        for (int i = 0; i < number * radius *radius; i++)
        {
            float r = Random.value*radius;
            float theta = Random.value*2*Mathf.PI;
            float variation = (Random.value - 0.5f) * 2f;
            Transform t = Instantiate(cloudPrefab, transform).transform;
            t.position += new Vector3(r * Mathf.Cos(theta), variation, r * Mathf.Sin(theta));
        }
        Debug.Log("cloudSSS");
    }

    private void Start()
    {
        gameEvent.AddEventToQuarry();
        Debug.Log("AAAAAA");
    }

    private void Update()
    {
        if(transform.childCount < number * radius * radius * 0.1)
        {
            gameEvent.RemoveEventFromQuarry();
            Destroy(gameObject);
        }
    }

}
