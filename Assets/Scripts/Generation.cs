using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    //room
    GameObject[] RoomPrefabs;

    //roomPos //3.84 gap/room x & y
    List<Vector3> roomPos = new List<Vector3>();

    //hold for rand rooms
    List<GameObject> randRoomList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
        RoomPrefabs = Resources.LoadAll<GameObject>("Rooms");
        //Debug.Log(RoomPrefabs.Length);//prints how many objects in the rooms file in resources makes sure everything loaded
        List<GameObject> rooms = new List<GameObject>(RoomPrefabs); //makes a list of Type Gameobject

        roomPos.Add(new Vector3(0     , 3.84f , 0));//pos0  
        roomPos.Add(new Vector3(3.84f , 3.84f , 0));//pos1  
        roomPos.Add(new Vector3(3.84f , 0     , 0));//pos2 
        roomPos.Add(new Vector3(3.84f , -3.84f, 0));//pos3 
        roomPos.Add(new Vector3(0     , -3.84f, 0));//pos4 
        roomPos.Add(new Vector3(-3.84f, -3.84f, 0));//pos5 
        roomPos.Add(new Vector3(-3.84f, 0     , 0));//pos6
        roomPos.Add(new Vector3(-3.84f, 3.84f , 0));//pos7
        //for(int i =0 ; i < 9; i++)
        //{
        //    Debug.Log(RoomPrefabs[i]); //prints prefab room name
        //}
        //Debug.Log(roomPos[0]); //prints room position
       //i got 9 rooms //7 room position options 

        //get a random room
        //x8
        for(int i = 0; i < 8; i++)
        {
            //random room range = 0 - room prefab array count
            int randIndex = Random.Range(0, RoomPrefabs.Length);
            GameObject randRoom = RoomPrefabs[randIndex];
            randRoomList.Add(randRoom);
            Debug.Log(randRoomList[i]);
        }
        for(int i =0; i < 8; i++)
        {
            GameObject roomGen = Instantiate(randRoomList[i], roomPos[i], Quaternion.identity);
        }
        
        //Debug.Log(randIndex);//test to see if rand picker thing works
        //Debug.Log(randRoom);//
        //set  8 rand room numbers to all room positions

        

        //GameObject Room1 = Instantiate(RoomPrefabs[7], roomPos[0], Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        
        
        
        
    }
}
