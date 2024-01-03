using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSpawning : MonoBehaviour
{
    public GameObject Blackguyprefab;
    public GameObject Grimisprefab;
    public GameObject Roboguyprefab;
    public GameObject Enemy1prefab;
    public GameObject Plant1Prefab;
    public GameObject Plant2Prefab;
    public GameObject Plant3Prefab;
    
    public GameObject[] PlantPrefabs;
    List<GameObject> enemyList = new List<GameObject>();
    List<GameObject> plantList = new List<GameObject>();
    public Dictionary<GameObject, List<GameObject>> roomEnemies = new Dictionary<GameObject, List<GameObject>>();
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

        plantList.Add(Plant1Prefab);
        plantList.Add(Plant2Prefab);
        plantList.Add(Plant3Prefab);

        int randIndexPlants = Random.Range(0, plantList.Count);
        GameObject randPlant = plantList[randIndexPlants];
        float randX2 = Random.Range(-1.7f, 1.7f);//rand pos for plant diffrent from enemy 
        float randY2 = Random.Range(-1.7f, 1.7f);
        Vector2 randPos2 = new Vector2(randX2, randY2);
        GameObject spawnthisPlant = Instantiate(randPlant, RoomPrefab.transform);
        spawnthisPlant.transform.localPosition = randPos2;
        for (int i = 0; i < scale; i++)
        {
            int randIndex = Random.Range(0, enemyList.Count);
            GameObject randEnemy = enemyList[randIndex];
            float randX = Random.Range(-1.7f, 1.7f);
            float randY = Random.Range(-1.7f, 1.7f);
            Vector2 randPos = new Vector2(randX, randY);
            GameObject spawnthis = Instantiate(randEnemy, RoomPrefab.transform);
            spawnthis.transform.localPosition = randPos;
            if (!roomEnemies.ContainsKey(RoomPrefab))
            {
                roomEnemies[RoomPrefab] = new List<GameObject>();
            }
            roomEnemies[RoomPrefab].Add(spawnthis);
            
        }
    }
}
