using System.Collections.Generic;
using UnityEngine;

public class RoomSpawning : MonoBehaviour
{
    public GameObject Blackguyprefab;
    public GameObject Grimisprefab;
    public GameObject Roboguyprefab;
    public GameObject Enemy1prefab;
    List<GameObject> enemyList = new List<GameObject>();
    public GameObject RoomPrefab;
    public FirstStartManager fs;

    void Start()
    {
        //scale to make game harder
        fs = GameObject.Find("firststartmanager").GetComponent<FirstStartManager>();
        GameObject[] sameNameObjs = fs.FindGameObjectsWithSameName("firststartmanager");
        int scale = sameNameObjs.Length;

        enemyList.Add(Blackguyprefab);
        enemyList.Add(Grimisprefab);
        enemyList.Add(Roboguyprefab);
        enemyList.Add(Enemy1prefab);

        for (int i = 0; i < scale; i++)
        {
            int randIndex = Random.Range(0, enemyList.Count);
            GameObject randEnemy = enemyList[randIndex];

            float randX = Random.Range(0f, 1.2f);
            float randY = Random.Range(0f, 1.2f);
            Vector2 randPos = new Vector2(randX, randY);

            GameObject spawnthis = Instantiate(randEnemy, RoomPrefab.transform);
            spawnthis.transform.localPosition = randPos;
        }
    }
}
