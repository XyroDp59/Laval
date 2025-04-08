using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public static List<Bridge> Bridges;
    [SerializeField] bool _isBroken;
    [SerializeField] Transform entry;
    [SerializeField] Transform exit;
    List<Egyptian> _egyptiansOn = new List<Egyptian>();
    UnityEngine.AI.NavMeshObstacle _obstacle;
    // Start is called before the first frame update
    void Start()
    {
        Bridges ??= new List<Bridge>();
        Bridges.Add(this);
        _obstacle = GetComponent<UnityEngine.AI.NavMeshObstacle>();
        if (gameObject.name == "Pont (1)"){
                StartCoroutine(niqueTaMere());
            }
    }
IEnumerator niqueTaMere(){
    yield return new WaitForSeconds(20);
    Break();
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
        _obstacle.center = new Vector3(0f,0f,0f);
        _isBroken = true;
        foreach (Egyptian temp in _egyptiansOn){
            temp.Die();
        }
        _egyptiansOn.Clear();
    }

    public void Repair(){
        _isBroken = false;
        _obstacle.center = new Vector3(0f,0f,1f);
    }
}
