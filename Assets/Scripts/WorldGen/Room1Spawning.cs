using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Spawning : MonoBehaviour
{
    public GameObject Blackguyprefab;
    public GameObject Grimisprefab;
    public GameObject Roboguyprefab;
    public GameObject Enemy1prefab;
    List<GameObject> enemyList = new List<GameObject>();
    List<GameObject> randEnemyList = new List<GameObject>();
    List<Vector2> spawnPos = new List<Vector2>();
    List<Vector2> randSpawnPos = new List<Vector2>();

    public GameObject Room1Prefab;

    void Start()
    {
        enemyList.Add(Blackguyprefab);
        enemyList.Add(Grimisprefab);
        enemyList.Add(Roboguyprefab);
        enemyList.Add(Enemy1prefab);
        
        spawnPos.Add(new Vector2(0, -1.5f));
        spawnPos.Add(new Vector2(0, 1.5f));
        spawnPos.Add(new Vector2(-1.5f, 0));
        spawnPos.Add(new Vector2(1.5f, 0));
        //rand enemy
        for (int i = 0; i < 2; i++)
        {
            int randIndex = Random.Range(0, enemyList.Count);
            GameObject randEnemy = enemyList[randIndex];
            randEnemyList.Add(randEnemy);
        }
        
        //room1
        for (int i = 0; i < 2; i++)
        {
            int randIndex = Random.Range(0, spawnPos.Count);
            Vector2 randPos = spawnPos[randIndex];
            randSpawnPos.Add(randPos);
            Debug.Log(randSpawnPos);
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject spawnthis = Instantiate(randEnemyList[i], Room1Prefab.transform);
            spawnthis.transform.localPosition = randSpawnPos[i];
        }

    }

    void Update()
    {
        Debug.Log(randEnemyList.Count);
        if (randEnemyList.Count == 0)
            Debug.Log("ZERO enemies left in this room...Spawning Chest");
    }
}

    

