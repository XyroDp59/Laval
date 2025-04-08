using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] bool _isBroken;
    [SerializeField] Transform entry;
    [SerializeField] Transform exit;
    List<Egyptian> _egyptiansOn = new List<Egyptian>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsValid(){
        return !_isBroken;
    }

    public Vector3 GetEntryPos(){
        return entry.position;
    }

    public Vector3 GetExitPos(){
        return exit.position;
    }

    public void Pass(Egyptian egyptian){
        _egyptiansOn.Add(egyptian);
    }

    public void Passed(Egyptian egyptian){
        _egyptiansOn.Remove(egyptian);
    }

    public void Break(){
        foreach (Egyptian temp in _egyptiansOn){
            temp.Die();
        }
        _egyptiansOn.Clear();
    }
}
