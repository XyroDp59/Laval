using System.Collections.Generic;
using UnityEngine;

public class UnbreakableBridges : MonoBehaviour
{
    [SerializeField] private Transform target;
    public static List<Transform> Bridges;
    void Start()
    {
        Bridges ??= new List<Transform>();
        Bridges.Add(target);
    }
}
