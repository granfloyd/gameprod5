using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject pressE;

    public Chest chestRef;

    public Text keyCountText;

    public GameObject keyObject;

    public GameObject chestObject;
    
    public GameObject grimisDrinkObject;

    public GameObject heartObject;

    public GameObject shieldObject;

    public GameObject projectilePrefab;

    public GameObject aimProjectile;

    public GameObject shieldPrefab;

    public GameObject grimisDrinkPrefab;

    public List<GameObject> inventory = new List<GameObject>();

    public float projectileSpeed;

    public float ticker = 0;

    public int totalKeys = 0;

    private Vector3 mousePos;

    private Vector2 aimDirection;

    //key stuff
    public bool isPickedUp = false;

    public bool onKey = false;

    //chest stuff
    public bool isOpened = false;

    public bool onChest = false;

    //door stuff 
    public bool onDoor = false;

    //shield stuff
    public float ticker2 = 0;

    public bool shieldActive = false;

    GameObject shield;

    public bool onGrimisDrink = false;

    public bool onHeart = false;

    public bool onShield = false;

    public bool full = false;

    public enum Items { grimisDrink, Heart, Shield }
    void Start()
    {
        aimProjectile = Instantiate(aimProjectile, aimDirection, Quaternion.identity);

        chestRef = GameObject.Find("Chest").GetComponent<Chest>();
        UpdateKey(4);
    }


    public void UpdateKey(int addkey)
    {
        keyCountText.text = totalKeys.ToString();
        totalKeys += addkey;
    }


    // Update is called once per frame
    void Update()
    {
        RotateAim();
        ticker += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if(ticker >=1)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
                //calcs a vector from player -  mouse pos
                Vector2 direction = (Vector2)((mousePos - transform.position ));
                direction.Normalize();

                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
               
                //// Destroy the gameobject 5 seconds after creation
                Destroy(projectile, 5.0f);
                // Resets ticker
                ticker = 0;
                
            } 
        }
        if(full)
        {
            if(inventory.Count <= 4)
            {
                full = false;
            }
            if(inventory.Any(item => item == null))
            {
                full = false;
            }
        }
        if(inventory.Count >= 4 && inventory.Any(item => item != null))
        {
            full = true;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            inventory.RemoveAt(3);
        }
        //key stuff
        if (onKey && Input.GetKeyDown(KeyCode.E))
        {
            UpdateKey(1);
            Destroy(keyObject);
        }

        if(onKey)
        {
            pressE.SetActive(true);
        }
        else if(onChest)
        {
            pressE.SetActive(true);
        }
        else if (onDoor)
        {
            pressE.SetActive(true);
        }
        else if(onHeart)
        {
            pressE.SetActive(true);
        }
        else if(onGrimisDrink)
        {
            pressE.SetActive(true);
        }
        else if(onShield)
        {
            pressE.SetActive(true);
        }
        else
        {
            pressE.SetActive(false);
        }
        //chest stuff
        if(totalKeys > 0)
        {
            if (onChest && Input.GetKeyDown(KeyCode.E))
            {
                //take key away from player 
                totalKeys -= 1; 
                //spawns a rand gameobject / drop then destroys object
                chestRef.OpenChest();
            }
        }
        if(!full)
        {
            if (onGrimisDrink && Input.GetKeyDown(KeyCode.F))
            {
                inventory.Add(grimisDrinkPrefab);
                Destroy(grimisDrinkObject);
            }

            if (onHeart && Input.GetKeyDown(KeyCode.F))
            {

            }

            if (onShield && Input.GetKeyDown(KeyCode.F))
            {

            }
        }
        

        //door stuff 
        if (onDoor && Input.GetKeyDown(KeyCode.E))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            SceneManager.LoadScene("Game1");
        }

        //shield stuff
        if(!shieldActive)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
                shieldActive = true;
            }
        }
        
        if (shieldActive)
        {
            ticker2 += Time.deltaTime;

            if (ticker2 > 10)
            {
                Destroy(shield);
                ticker2 = 0;
                shieldActive = false;
            }
        }
        if(shield != null)
        shield.transform.position = transform.position;


    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Key")
        {
            onKey = true;
            keyObject = collider.gameObject;
        }
        if (collider.gameObject.tag == "Chest")
        {
            onChest = true;
            chestObject = collider.gameObject;
        }
        if (collider.gameObject.tag == "Door")
        {
            onDoor = true;            
        }
        if (collider.gameObject.tag == "GrimisDrink")
        {
           onGrimisDrink = true;
            grimisDrinkObject = collider.gameObject;
        }
        if (collider.gameObject.tag == "Heart")
        {
            onHeart = true;
            heartObject = collider.gameObject;
        }
        if (collider.gameObject.tag == "Shield")
        {
            onShield = true;
            shieldObject = collider.gameObject;
        }

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Key")
        {
            if(!onKey)
            {
                onKey = true;
                keyObject = collider.gameObject;
            }

        }
        if (collider.gameObject.tag == "Chest")
        {
            if (!onChest)
            {
                onChest = true;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Key")
        {
            onKey = false;
            keyObject = collider.gameObject;
        }
        if (collider.gameObject.tag == "Chest")
        {
            onChest = false;
        }
        if (collider.gameObject.tag == "Door")
        {
            onDoor = false;
        }
        if (collider.gameObject.tag == "GrimisDrink")
        {
            onGrimisDrink = false;
            grimisDrinkObject = collider.gameObject;
        }
        if (collider.gameObject.tag == "Heart")
        {
            onHeart = false;
            heartObject = collider.gameObject;
        }
        if (collider.gameObject.tag == "Shield")
        {
            onShield = false;
            shieldObject = collider.gameObject;
        }
    }
    
    void RotateAim()
    {
         mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         Vector2 aimDirection = (Vector2)((mousePos - transform.position));
         aimDirection.Normalize();
         // Update the position of aimProjectile to follow the player
         aimProjectile.transform.position = (Vector2)transform.position + aimDirection * 0.3f;
         
         //Calculates the angle of aimDirection, adjusting for a sprite that faces up
         float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
         
         // Rotate aimProjectile to face towards the mouse cursor
         aimProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
}
