using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egyptian : MonoBehaviour
{
    [SerializeField] private GameObject goQuarry;
    private Quarry _scQuarry = null;
    private Vector3 _posQuarry;

    private UnityEngine.AI.NavMeshAgent agent;


    private int _state;
    private Vector3 _posToGo;
    private int _idThingToDo;
    private Bridge _bridgeToPass;
    private Pyramid _pyramidToBuild;
    private int _idBridgeBack;
    private bool isInPyramid = false;
    private bool isPassingBridge = false;

    // Start is called before the first frame update
    void Start()
    {   
        _state = -1;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (goQuarry.TryGetComponent<Quarry>(out _scQuarry)){
            _posQuarry = goQuarry.transform.position; 
        } else {
            Debug.Log("no quarry found");
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (_posToGo != null && !isPassingBridge && agent.remainingDistance < 0.5 ) // Arrive à destination ?
        {
            switch(_state){
                case -1:
                    LeavePyramid();
                    _scQuarry.GiveDirection(this);
                    _state = 0;
                    break;
                case 0:
                    if (_bridgeToPass.IsValid()){
                        _state = 1;
                        _bridgeToPass.Pass(this);
                        //isPassingBridge = true;
                        //agent.isStopped = true;
                        //agent.enabled = false;
                        _posToGo = _bridgeToPass.GetExitPos();
                    } else {
                        _state = -1;
                        _posToGo = _posQuarry;
                    }
                    break;
                case 1:
                    _state = 2;
                    _posToGo = _pyramidToBuild.GetPos();
                    break;
                case 2:
                    LeavePyramid();
                    _idBridgeBack = _pyramidToBuild.index;
                    _state = 3;
                    _idBridgeBack = _scQuarry.GiveDirectionBack(this, _idBridgeBack);
                    break;
                case 3:
                    if (_bridgeToPass.IsValid()){
                        _state = 4;
                        _bridgeToPass.Pass(this);
                        //isPassingBridge = true;
                        //agent.isStopped = true;
                        //agent.enabled = false;
                        _posToGo = _bridgeToPass.GetEntryPos();
                    } else {
                        _idBridgeBack = _scQuarry.GiveDirectionBack(this, _idBridgeBack);
                    }
                    break;
                case 4:
                    _state = -1;
                    _posToGo = _posQuarry;
                    break;

            }
        }
        if (isPassingBridge){
            transform.position = Vector3.MoveTowards(transform.position, _posToGo, 1.5f * Time.deltaTime);
            Debug.Log("Position A: " + transform.position + " | Position B: " + _posToGo);
            Debug.Log("ispassing");
            if ((_posToGo - transform.position).sqrMagnitude < 0.5){
                isPassingBridge = false;
                agent.isStopped = false;
                //agent.enabled = true;
            }

        } else {
            Behaviour();
        }
        
    }

    void Behaviour(){
        if (HasThingToDo()){
            switch (_idThingToDo){
                case 0 : 
                    agent.isStopped = true;
                    break;
            }
        } else {
            agent.isStopped = false;
            agent.SetDestination(_posToGo); 
        }
    }


    bool HasThingToDo(){
        foreach (var poi in _scQuarry.GetPOI()){
            if ((poi.position - transform.position).sqrMagnitude < poi.range * poi.range){
                _idThingToDo = poi.id;
                return true;
            }
        }
        return false;
    }

    public void Die(){
        LeavePyramid();
    }

    public void SetPosToGo(Vector3 pos){
        _posToGo = pos;
    }

    public void SetBridgeToPass(Bridge bridge){
        _bridgeToPass = bridge;
    }

    public void SetPyramidToBuild(Pyramid pyramid){
        _pyramidToBuild = pyramid;
    }

    public void EnterPyramid(){
        if (!isInPyramid){
            isInPyramid = true;
            _pyramidToBuild.IncrementEgyptian();
        }
    }
    public void LeavePyramid(){
        if (isInPyramid){
            isInPyramid = false;
            _pyramidToBuild.DecrementEgyptian();
        }
    }
}
