using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Generation : MonoBehaviour
{
    GameObject[] RoomPrefabs;
    public AudioSource spawnSFX;
    List<Vector3> roomPos = new List<Vector3>();
    List<GameObject> randRoomList = new List<GameObject>();
    public List<RoomSpawning> roomSpawningScripts = new List<RoomSpawning>();
    public RoomSpawning roomSpawning;
    public GameObject portalPrefab;
    public GameObject floorClearedTextPrefab;
    public Transform target;//Player transform
    public bool isSpawned = false;
    void Awake()
    {
        RoomPrefabs = Resources.LoadAll<GameObject>("Rooms");
        GenerateRoomPositions();

        for (int i = 0; i < roomPos.Count; i++)
        {
            int randIndex = Random.Range(0, RoomPrefabs.Length);
            GameObject randRoom = RoomPrefabs[randIndex];
            randRoomList.Add(randRoom);
            GameObject room = Instantiate(randRoomList[i], roomPos[i], Quaternion.identity);
            RoomSpawning roomSpawning = room.GetComponent<RoomSpawning>();
            if (roomSpawning != null)
            {
                roomSpawningScripts.Add(roomSpawning);
            }
        }
    }
    void FloorClearedDisplay()
    {
        GameObject clearedFloorTextObject = Instantiate(floorClearedTextPrefab, target.transform.position, Quaternion.identity, transform);
        TextMeshPro TextObject = clearedFloorTextObject.GetComponent<TextMeshPro>();
        StartCoroutine(MoveAndDestroy(clearedFloorTextObject, TextObject));
    }
    IEnumerator MoveAndDestroy(GameObject clearedFloorTextObject, TextMeshPro TextObject)
    {
        float fadeTime = 1f;
        float speed = 0.5f;
        Color originalColor = TextObject.color;
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            clearedFloorTextObject.transform.position += Vector3.up * speed * Time.deltaTime;
            yield return null;
            float normalizedTime = t / fadeTime;
            // Set the alpha
            TextObject.color = Color.Lerp(originalColor, Color.clear, normalizedTime);
        }
        Destroy(clearedFloorTextObject);
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
    void Update()
    {
        if (roomSpawningScripts.All(roomSpawning => roomSpawning.roomEnemies.All(room => room.Value.All(enemy => enemy == null))))
        {
            if(!isSpawned)
            {
                FloorClearedDisplay();
                spawnSFX.Play();
                Instantiate(portalPrefab, Vector3.zero, Quaternion.identity);
                isSpawned = true;
            }
            
        }
    }
}
