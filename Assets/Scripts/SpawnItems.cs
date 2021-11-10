using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    private List<Transform> spawnLocations = new List<Transform>();
    [SerializeField]
    private GameObject[] items = new GameObject[4];

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnLocations.Add(transform.GetChild(i));
        }
        foreach (GameObject item in items)
        {
            Instantiate(item);
            int spawnPosIndex = Random.Range(0, spawnLocations.Count);
            item.transform.position = spawnLocations[spawnPosIndex].position;
            spawnLocations.RemoveAt(spawnPosIndex);
        }
    }
}
