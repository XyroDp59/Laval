using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public static List<Bridge> Bridges;
    [SerializeField] Mesh validMesh;
    [SerializeField] Mesh brokenMesh;

    [SerializeField] bool _isBroken;
    [SerializeField] Transform entry;
    [SerializeField] Transform exit;
    List<Egyptian> _egyptiansOn = new List<Egyptian>();
    MeshFilter _meshFilter;
    UnityEngine.AI.NavMeshObstacle _obstacle;
    private BoxCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _meshFilter = GetComponent<MeshFilter>();
        Bridges ??= new List<Bridge>();
        Bridges.Add(this);
        _obstacle = GetComponent<UnityEngine.AI.NavMeshObstacle>();
        if (gameObject.name == "Pont (1)"){
                Break();
                //StartCoroutine(niqueTaMere());
            }
    }
IEnumerator niqueTaMere(){
    yield return new WaitForSeconds(20);
    Break();
}
    // Update is called once per frame
    void Update()
    {
        _collider.enabled = _isBroken;
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
        _meshFilter.mesh = brokenMesh;
        _obstacle.center = new Vector3(0f,0f,0f);
        _isBroken = true;
        foreach (Egyptian temp in _egyptiansOn){
            temp.Die();
        }
        _egyptiansOn.Clear();
    }

    public void Repair(){
        _meshFilter.mesh = validMesh;
        _isBroken = false;
        _obstacle.center = new Vector3(0f,0f,1f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out XRControllerSpeed controller))
        {
            HandAnimator handAnimator = other.GetComponentInChildren<HandAnimator>();
            if (!handAnimator) return;
            handAnimator.Rumble(1, 0.2f);
            Repair();
        }
    }
}
