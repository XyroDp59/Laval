using System.Collections.Generic;
using UnityEngine;

public class UnbreakableBridges : MonoBehaviour
{
    public static List<UnbreakableBridges> Bridges;
    void Start()
    {
        Bridges ??= new List<UnbreakableBridges>();
        Bridges.Add(this);
    }
}
