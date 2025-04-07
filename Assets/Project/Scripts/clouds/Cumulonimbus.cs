using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumulonimbus : MonoBehaviour
{
    [SerializeField] Cloud cloudPrefab;
    [SerializeField] float radius;
    [SerializeField] float number;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i< number * radius *radius; i++)
        {
            float r = Random.value*radius;
            float theta = Random.value*2*Mathf.PI;
            float variation = (Random.value - 0.5f) * 2f;
            Instantiate(cloudPrefab, new Vector3(r * Mathf.Cos(theta), transform.position.y, r * Mathf.Sin(theta)), Quaternion.identity);
        }
        Destroy(gameObject, 1f);
    }
}
