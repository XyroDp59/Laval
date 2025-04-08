using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyramid : MonoBehaviour
{
    [SerializeField] List<GameObject> pyramideLayers = new List<GameObject>();
    [SerializeField] int blocksPerLayers = 100;
    int currentBlocksStoredInLayer = 0;
    int currentLayer = -1;




    
    [SerializeField] Transform pos;
    private int _nbEgyptian;
    public int index;

    private void Start()
    {
        foreach (var layer in pyramideLayers)
        {
            layer.gameObject.SetActive(false);
        }
    }

    public void AddBlockToPyramide()
    {
        if (currentLayer == pyramideLayers.Count -1) return;

        currentBlocksStoredInLayer++;
        if(currentBlocksStoredInLayer >= blocksPerLayers)
        {
            currentLayer++;
            currentBlocksStoredInLayer = 0;

            pyramideLayers[currentLayer].gameObject.SetActive(true);
        }
    }

    
    public Vector3 GetPos(){
        return pos.position;
    }

    public void Build(){

    }

    public int GetNbEgyptian(){
        return _nbEgyptian;
    }

    public void IncrementEgyptian(){
        _nbEgyptian++;
    }

    public void DecrementEgyptian(){
        _nbEgyptian--;
    }
}
