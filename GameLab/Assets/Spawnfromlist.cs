using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnfromlist : MonoBehaviour
{
    [SerializeField] List<GameObject> SpawnPrefabs;
    [SerializeField] float SpawnIntervals;

    GameObject SpawnedObj;

    void Start()
    {
        InvokeRepeating("SpawnObject", 0, SpawnIntervals);
    }

    void SpawnObject()
    {
        if (SpawnedObj == null)
        {
            SpawnedObj = Instantiate(SpawnPrefabs[Random.Range(0, SpawnPrefabs.Count)]);
            SpawnedObj.transform.position = transform.position;
        }
    }
}
