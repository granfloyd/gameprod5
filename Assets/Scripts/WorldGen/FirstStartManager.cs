using System.Collections.Generic;
using UnityEngine;

public class FirstStartManager : MonoBehaviour
{
    public static bool isFirstStart = true;

    public GameObject[] FindGameObjectsWithSameName(string name)
    {
        GameObject[] allObjs = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> likeNames = new List<GameObject>();
        foreach (GameObject obj in allObjs)
        {
            if (obj.name == name)
            {
                likeNames.Add(obj);
            }
        }
        return likeNames.ToArray();
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
