using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public Player playerRef;
    public Chest chestRef;
    public Item itemRef;

    public GameObject keyObject;
    public GameObject chestObject;
    public GameObject grimisDrinkObject;
    public GameObject heartObject;
    public GameObject shieldObject;
    public GameObject powerup69Object;
    public GameObject pressE;
    public GameObject pressQ;

    public AudioSource audioSource;//key

    public Text keyCountText;
    public int totalKeys = 0;

    public bool onKey = false;
    public bool onChest = false;
    public bool onDoor = false;
    public bool onGrimisDrink = false;
    public bool onHeart = false;
    public bool onShield = false;
    public bool onPowerup69 = false;

    // Start is called before the first frame update
    void Start()
    {
        chestRef = GameObject.Find("Chest").GetComponent<Chest>();
        playerRef = GetComponent<Player>();
        itemRef = GetComponent<Item>();
        UpdateKey(1);
    }
    

    void Update()
    {
        InteractQ();
        PickUpE();
    }
    public void UpdateKey(int addkey)
    {
        audioSource.Play();
        keyCountText.text = totalKeys.ToString();
        totalKeys += addkey;
    }
    private void InteractQ()
    {
        if (onChest || onDoor)
        {
            pressQ.SetActive(true);
        }
        else
        {
            pressQ.SetActive(false);
        }
        //chest stuff
        if (totalKeys > 0)
        {
            if (onChest && Input.GetKeyDown(KeyCode.Q))
            {
                //take key away from player 
                totalKeys -= 1;
                UpdateKey(0);
                //spawns a rand gameobject / drop then destroys object
                chestRef.OpenChest(this.transform);
            }
        }
        //door stuff 
        if (onDoor && Input.GetKeyDown(KeyCode.Q))
        {
            FirstStartManager.isFirstStart = false;
            PlayerPrefs.SetInt("PlayerHealth", HealthSystem.health); // Save current health
            PlayerPrefs.SetInt("PlayerScore", ScoreManager.score); // Save current score
            SceneManager.LoadScene("Game1");
        }
    }
    private void PickUpE()
    {
        //key stuff
        if (onKey && Input.GetKeyDown(KeyCode.E))
        {
            UpdateKey(1);
            Destroy(keyObject);
        }

        if (onKey || onGrimisDrink || onShield || onPowerup69 || onHeart)
        {
            pressE.SetActive(true);
        }
        else
        {
            pressE.SetActive(false);
        }
        if (onGrimisDrink && Input.GetKeyDown(KeyCode.E))
        {
            playerRef.AddToInventory(itemRef.grimisDrinkPrefab, grimisDrinkObject);
        }
        if (onHeart && Input.GetKeyDown(KeyCode.E))
        {
            playerRef.AddToInventory(itemRef.heartPrefab, heartObject);
        }
        if (onShield && Input.GetKeyDown(KeyCode.E))
        {
            playerRef.AddToInventory(itemRef.shieldPrefab, shieldObject);
        }
        if (onPowerup69 && Input.GetKeyDown(KeyCode.E))
        {
            playerRef.AddToInventory(itemRef.powerup69Prefab, powerup69Object);
        }
        

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;

        switch (tag)
        {
            case "Key":
                onKey = true;
                keyObject = collider.gameObject;
                break;
            case "Chest":
                onChest = true;
                chestObject = collider.gameObject;
                break;
            case "Door":
                onDoor = true;
                break;
            case "GrimisDrink":
                onGrimisDrink = true;
                grimisDrinkObject = collider.gameObject;
                break;
            case "Heart":
                onHeart = true;
                heartObject = collider.gameObject;
                break;
            case "Shield":
                onShield = true;
                shieldObject = collider.gameObject;
                break;
            case "Powerup69":
                onPowerup69 = true;
                powerup69Object = collider.gameObject;
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Key")
        {
            if (!onKey)
            {
                onKey = true;
                Debug.Log("hallo");
                keyObject = collider.gameObject;
            }

        }
        if (collider.gameObject.tag == "Chest")
        {
            if (!onChest)
            {
                Debug.Log("hallo");
                onChest = true;
            }
        }
        //string tag = collider.gameObject.tag;

        //switch (tag)
        //{
            //case "Key":
                //if (!onKey)
                //{
                    //onKey = true;
                    //keyObject = collider.gameObject;
                //}
                //break;
            //case "Chest":
                //if (!onChest)
                //{
                    //onChest = true;
                //}
                //break;
        //}

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;

        switch (tag)
        {
            case "Key":
                onKey = false;
                keyObject = collider.gameObject;
                break;
            case "Chest":
                onChest = false;
                break;
            case "Door":
                onDoor = false;
                break;
            case "GrimisDrink":
                onGrimisDrink = false;
                grimisDrinkObject = collider.gameObject;
                break;
            case "Heart":
                onHeart = false;
                heartObject = collider.gameObject;
                break;
            case "Shield":
                onShield = false;
                shieldObject = collider.gameObject;
                break;
            case "Powerup69":
                onPowerup69 = false;
                powerup69Object = collider.gameObject;
                break;
        }
    }
}
