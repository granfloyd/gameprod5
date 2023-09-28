using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    //room
    public GameObject[] RoomPrefabs;
    //roomPos
    List<Vector3> roomPos = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        RoomPrefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms");
        List<GameObject> rooms = new List<GameObject>(RoomPrefabs);
        roomPos.Add(new Vector3(0, -3.52f,0));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
