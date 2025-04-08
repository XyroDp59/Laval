using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarry : MonoBehaviour
{
    [SerializeField] private GameObject _egyptianModel; 
    List<(Vector3 position, float range, int id)> PointOfInterests = new List<(Vector3, float, int)>();


    [SerializeField] List<GameObject> pyramids = new List<GameObject>();
    [SerializeField] List<GameObject> bridges = new List<GameObject>();
    List<Pyramid> _pyramids = new List<Pyramid>();
    List<Bridge> _bridges = new List<Bridge>();
    int nbPyramid;
    // Start is called before the first frame update
    void Start()
    {
        if (pyramids.Count != bridges.Count){
            Debug.Log("not same amount of bridges and pyramids");
        } else {
            for (int i = 0; i < pyramids.Count; i++){
                if (pyramids[i].TryGetComponent<Pyramid>(out var pyramidTemp) && bridges[i].TryGetComponent<Bridge>(out var bridgeTemp)){
                    pyramidTemp.index = i;
                    _pyramids.Add(pyramidTemp);
                    _bridges.Add(bridgeTemp);
                }
            }
            nbPyramid = _pyramids.Count;
        }

        StartCoroutine(CreateEgyptians());
    }

    IEnumerator CreateEgyptians(){
        for (int i = 0; i<1000; i++){
            CreateEgyptian();
            yield return new WaitForSeconds(0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space)){
            PointOfInterests.Add((new Vector3(0f,0f,0f), 1000000f, 0));
            Debug.Log("yo sale pute");
        }
        if (Input.GetKeyDown(KeyCode.P)){
            PointOfInterests.Clear();
        }*/
    }

    public List<(Vector3 position, float range, int id)> GetPOI(){

        return PointOfInterests;
    }

    public void GiveDirection(Egyptian egyptian){
        int idPyramid = GetPyramidToBuild();
        int idBridge = GetBridgeToPass(idPyramid);

        egyptian.SetPosToGo(_bridges[idBridge].GetEntryPos());
        egyptian.SetBridgeToPass(_bridges[idBridge]);
        egyptian.SetPyramidToBuild(_pyramids[idPyramid]);
        egyptian.EnterPyramid();
    }

    public int GiveDirectionBack(Egyptian egyptian, int idPyramid){
        egyptian.SetPosToGo(_bridges[idPyramid].GetExitPos());
        egyptian.SetBridgeToPass(_bridges[idPyramid]);
        return (idPyramid + 1) % nbPyramid;
    }

    private void CreateEgyptian(Vector3 pos){
        GameObject newEgyptian = Instantiate(_egyptianModel, pos, Quaternion.identity);
        newEgyptian.SetActive(true);
    }
    private void CreateEgyptian(){
        CreateEgyptian(transform.position);
    }


    private int GetPyramidToBuild(){
        int minPyramid = 0;
        int minValue = _pyramids[minPyramid].GetNbEgyptian();

        for (int i = 1; i < _pyramids.Count; i++)
        {
            int currentValue = _pyramids[i].GetNbEgyptian();
            if (currentValue < minValue)
            {
                minValue = currentValue;
                minPyramid = i;
            }
        }
        return minPyramid;
    }

    private int GetBridgeToPass(int offset){
        int k;
        for (int i = 0; i<1+nbPyramid/2; i++){
            k = (offset + i) % nbPyramid;
            if (_bridges[k].IsValid()){
                return k;
            }
            k = (offset - i + nbPyramid) % nbPyramid;
            if (_bridges[k].IsValid()){
                return k;
            }
        }
        return offset;
    }
}
