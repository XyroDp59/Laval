using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumulonimbus : MonoBehaviour
{
    [SerializeField] Cloud cloudPrefab;
    [SerializeField] float radius;
    [SerializeField] float density;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i< Mathf.PI * radius*radius*density; i++)
        {
            float r = Random.value*radius;
            float theta = Random.value*2*Mathf.PI;
            float variation = (Random.value - 0.5f) * 2f;
            Instantiate(cloudPrefab, new Vector3(r * Mathf.Cos(theta), transform.position.y, r * Mathf.Cos(theta)), Quaternion.identity);
        }
        Destroy(gameObject, 1f);
    }
}
