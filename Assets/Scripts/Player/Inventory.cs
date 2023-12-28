using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject imageObject;//display item to ui 
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;
    //public GameObject pauseMenuUI;

    //UI stuff//
    public Canvas GeneralUI;
    public Image SelectorIMG;//selctor square
    public Image image;//for ui slots
    public Image SelectorIMGPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //playerRef = playerGO.GetComponent<Player>();
        //SelectorIMG = Instantiate(SelectorIMGPrefab, Slot1.transform.position, Quaternion.identity);
        SelectorIMGPrefab = SelectorIMG.GetComponent<Image>(); 
        SelectorIMG.transform.SetParent(GeneralUI.transform, false);
        SelectorIMG.transform.position = Slot1.transform.position;
    }

    //add picked up item to ui slots
    
    //playerRef.
    

    
}
