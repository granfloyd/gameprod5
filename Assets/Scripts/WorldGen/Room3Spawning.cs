using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3Spawning : MonoBehaviour
{
    public GameObject Blackguyprefab;
    public GameObject Grimisprefab;
    public GameObject Roboguyprefab;
    public GameObject Enemy1prefab;
    List<GameObject> enemyList = new List<GameObject>();
    public GameObject randEnemy;
    List<Vector2> spawnPos = new List<Vector2>();
    List<Vector2> randSpawnPos = new List<Vector2>();

    public GameObject Room3Prefab;

    void Start()
    {
        enemyList.Add(Blackguyprefab);
        enemyList.Add(Grimisprefab);
        enemyList.Add(Roboguyprefab);
        enemyList.Add(Enemy1prefab);

        spawnPos.Add(new Vector2(0, -1));
        spawnPos.Add(new Vector2(0, 0));
        spawnPos.Add(new Vector2(0, 1));
        spawnPos.Add(new Vector2(-1, 0));
        spawnPos.Add(new Vector2(1, 0));
        //rand enemy
        for (int i = 0; i < 1; i++)
        {
            int randIndex = Random.Range(0, enemyList.Count);
            GameObject randEnemy = enemyList[randIndex];
        }

        //room2
        for (int i = 0; i < 1; i++)
        {
            int randIndex = Random.Range(0, spawnPos.Count);
            Vector2 randPos = spawnPos[randIndex];
            randSpawnPos.Add(randPos);
            Debug.Log(randSpawnPos);
        }
        for (int i = 0; i < 1; i++)
        {
            GameObject spawnthis = Instantiate(randEnemy, Room3Prefab.transform);
            spawnthis.transform.localPosition = randSpawnPos[i];
        }

    }

}
