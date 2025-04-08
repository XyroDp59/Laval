using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float condition = 0;

    private bool hasTriggered = false;

    void Update()
    {
        if (!hasTriggered && condition == 4) 
        {
            hasTriggered = true; // pour éviter de le faire plusieurs fois
            TriggerSuccess();
        }
    }

    void TriggerSuccess()
    {
        
    }
}
