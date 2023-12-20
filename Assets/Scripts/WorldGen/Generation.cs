using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    GameObject[] RoomPrefabs;
    List<Vector3> roomPos = new List<Vector3>();
    List<GameObject> randRoomList = new List<GameObject>();

    void Start()
    {
        RoomPrefabs = Resources.LoadAll<GameObject>("Rooms");
        GenerateRoomPositions();

        for (int i = 0; i < roomPos.Count; i++)
        {
            int randIndex = Random.Range(0, RoomPrefabs.Length);
            GameObject randRoom = RoomPrefabs[randIndex];
            randRoomList.Add(randRoom);
            Instantiate(randRoomList[i], roomPos[i], Quaternion.identity);
        }
    }

    void GenerateRoomPositions()
    {
        float[] positions = { 0, 3.84f, 7.68f, -3.84f, -7.68f };
        foreach (float x in positions)
        {
            foreach (float y in positions)
            {
                Vector3 pos = new Vector3(x, y, 0);
                if (pos != Vector3.zero) // This will prevent adding the position (0,0,0)
                {
                    roomPos.Add(pos);
                }
            }
        }
    }
}
