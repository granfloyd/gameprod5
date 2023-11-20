using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

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

    public GameObject projectilePrefab;

    public GameObject aimProjectile;

    public GameObject shieldPrefab;

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
    void Start()
    {
        aimProjectile = Instantiate(aimProjectile, aimDirection, Quaternion.identity);

        chestRef = GameObject.Find("Chest").GetComponent<Chest>();
        UpdateKey(1);
    }


    public void UpdateKey(int addkey)
    {
        keyCountText.text = totalKeys.ToString();
        totalKeys += addkey;
        Debug.Log("key has been added to player inventory");
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
        //key stuff
        if (onKey && Input.GetKey(KeyCode.E))
        {
            Debug.Log("player picked up key");
            UpdateKey(1);
            Destroy(keyObject);
            onKey = false;
        }

        //chest stuff
        if(totalKeys > 0)
        {
            if (onChest && Input.GetKey(KeyCode.E))
            {
                Debug.Log("player opened chest!");
                //take key away from player 
                totalKeys -= 1; 
                //spawns a rand gameobject / drop then destroys object
                chestRef.OpenChest();
                Destroy(chestObject);
                onChest = false;
            }
        }
        //door stuff 
        if (onDoor && Input.GetKey(KeyCode.E))
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
            Debug.Log("player is on key");
            keyObject = collider.gameObject;
            pressE.SetActive(true);
        }
        if (collider.gameObject.tag == "Chest")
        {
            onChest = true;
            Debug.Log("player is on Chest");
            chestObject = collider.gameObject;
            pressE.SetActive(true);
        }
        if (collider.gameObject.tag == "Door")
        {
            pressE.SetActive(true);
            onDoor = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Key")
        {
            onKey = false;
            Debug.Log("player walked over key");
            pressE.SetActive(false);
        }
        if (collider.gameObject.tag == "Chest")
        {
            onChest = false;
            Debug.Log("player walked over Chest");
            pressE.SetActive(false);
        }
        if (collider.gameObject.tag == "Door")
        {
            onDoor = false;
            pressE.SetActive(false);

        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Key")
        {
            
           
            pressE.SetActive(true);
        }
        if (collider.gameObject.tag == "Chest")
        {
            
            pressE.SetActive(true);
        }
        if (collider.gameObject.tag == "Door")
        {
            
            pressE.SetActive(true);

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
