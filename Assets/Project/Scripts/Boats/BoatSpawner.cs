using System.Collections;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boatPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float secondsBetweenSpawns;

    private void Start()
    {
        StartCoroutine(SpawnBoat());
    }

    private IEnumerator SpawnBoat()
    {
        yield return new WaitForSeconds(secondsBetweenSpawns);
        Instantiate(boatPrefab, spawnPoint.position, Quaternion.identity);
    }
}
