using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] dropList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenChest()
    {
        int randIndex = Random.Range(0, dropList.Length);
        GameObject randDrop = dropList[randIndex];

        GameObject spawnthis = Instantiate(randDrop, transform.position, Quaternion.identity);
        Debug.Log("WOW DROPS ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
