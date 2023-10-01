using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Blackguyprefab;
    public GameObject Room1Prefab;
    Vector3 R1spawnPosition = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {

        GameObject spawnthis = Instantiate(Blackguyprefab, Room1Prefab.transform);
        spawnthis.transform.localPosition = R1spawnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
