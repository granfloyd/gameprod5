using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    GameObject imageObject;
    Image image;
    public AudioSource audioSource;//key
    public AudioSource audioSource2;//drink
    public AudioSource audioSource3;//shield
    public AudioSource audioSource4;//addtoinventory
    public AudioSource audioSource5;//select
    public AudioSource audioSource6;//cant pickup item SFX
    public Image Active;
    Image Activeimg;

    public Canvas myCanvas;
    public Chest chestRef;
    public Text keyCountText;
    public GameObject pressE;
    public GameObject pressQ;
    private GameObject keyObject;
    private GameObject chestObject;    
    private GameObject grimisDrinkObject;
    private GameObject heartObject;
    private GameObject shieldObject;
    public GameObject projectilePrefab;
    public GameObject aimProjectile;
    public GameObject shieldPrefab;
    public GameObject shield2Prefab;
    public GameObject grimisDrinkPrefab;
    public GameObject heartPrefab;

    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;

    GameObject shield;
    public List<GameObject> inventory;

    public float projectileSpeed;
    private float ticker = 0;
    private float ticker2 = 0;
    public int totalKeys = 0;

    private Vector3 mousePos;
    private Vector2 aimDirection;

    private bool isPickedUp = false;
    private bool onKey = false;
    private bool isOpened = false;
    private bool onChest = false; 
    private bool onDoor = false;   
    private bool shieldActive = false;
    private bool onGrimisDrink = false;
    private bool onHeart = false;
    private bool onShield = false;
    private bool full = false;

    public int whatsActive = 0;

    void Start()
    {
        Active = Instantiate(Active, Slot1.transform.position, Quaternion.identity);
        Active.transform.SetParent(myCanvas.transform, false);
        Active.transform.position = Slot1.transform.position;
        //inventory = new List<GameObject>(new GameObject[4]);
        aimProjectile = Instantiate(aimProjectile, aimDirection, Quaternion.identity);
        
        chestRef = GameObject.Find("Chest").GetComponent<Chest>();
        UpdateKey(1);
    }

    //add picked up item to ui slots
    private void GuyzGamezLovesSlots(GameObject type)
    {
        imageObject = new GameObject("UIImage" + whatsActive);
        imageObject.transform.SetParent(myCanvas.transform, false);
        image = imageObject.AddComponent<Image>();

        
        if (inventory.Any())
        {
            type = inventory.ElementAt(whatsActive);
            if (type != null)
            {
                if (whatsActive == 0)
                    image.transform.position = Slot1.transform.position;                  
                if (whatsActive == 1)
                    image.transform.position = Slot2.transform.position;                   
                if (whatsActive == 2)
                    image.transform.position = Slot3.transform.position;                    
                if (whatsActive == 3)
                    image.transform.position = Slot4.transform.position;
                  
                // Update the sprite of the existing UI Image
                SpriteRenderer sr = type.GetComponent<SpriteRenderer>();
                image.sprite = sr.sprite;
            }
        }
    }

    private void AddToInventory(GameObject itemToAdd,GameObject itemToDelete)
    {
        if (inventory[whatsActive] == null)
        {
            audioSource4.Play();
            inventory.Insert(whatsActive, itemToAdd);
            GuyzGamezLovesSlots(itemToAdd);
            Destroy(itemToDelete);
        }
        else
        {
            audioSource6.Play();
            audioSource6.Play();
            Debug.Log("Greedy Jew, PUT THAT BACK!");
        }
    }

    private void DeleteItemFromInventory()
    {
        
        if (whatsActive == 0)
        {
            if (inventory[0] == grimisDrinkPrefab)
            {
                UseDrink();
                GameObject itemToDelete = GameObject.Find("UIImage0");
                if (itemToDelete != null)
                {
                    Destroy(itemToDelete);
                    inventory.RemoveAt(0);
                }


            }
            else if (inventory[0] == shieldPrefab)
            {
                UseShield();
                GameObject itemToDelete = GameObject.Find("UIImage0");
                if (itemToDelete != null)
                {
                    Destroy(itemToDelete);
                    inventory.RemoveAt(0);
                }
            }   
        }

        if (whatsActive == 1)
        {
            if (inventory[1] == grimisDrinkPrefab)
            {
                UseDrink();
                GameObject itemToDelete = GameObject.Find("UIImage1");
                if (itemToDelete != null)
                {
                    Destroy(itemToDelete);
                    inventory.RemoveAt(1);
                }
            }
            else if (inventory[1] == shieldPrefab)
            {
                UseShield();
                GameObject itemToDelete = GameObject.Find("UIImage1");
                if (itemToDelete != null)
                {
                    Destroy(itemToDelete);
                    inventory.RemoveAt(1);
                }
            }
        }

        if (whatsActive == 2)
        {
            if (inventory[2] == grimisDrinkPrefab)
            {
                UseDrink();
                GameObject itemToDelete = GameObject.Find("UIImage2");
                if (itemToDelete != null)
                {
                    Destroy(itemToDelete);
                    inventory.RemoveAt(2);
                }
            }
            else if (inventory[2] == shieldPrefab)
            {
                UseShield();
                GameObject itemToDelete = GameObject.Find("UIImage2");
                if (itemToDelete != null)
                {
                    Destroy(itemToDelete);
                    inventory.RemoveAt(2);
                }
            } 
        }

        if (whatsActive == 3)
        {
            if (inventory[3] == grimisDrinkPrefab)
            {
                UseDrink();
                GameObject itemToDelete = GameObject.Find("UIImage3");
                if (itemToDelete != null)
                {
                    Destroy(itemToDelete);
                    inventory.RemoveAt(3);
                }
            }
            else if (inventory[3] == shieldPrefab)
            {
                UseShield();
                GameObject itemToDelete = GameObject.Find("UIImage3");
                if (itemToDelete != null)
                {
                    Destroy(itemToDelete);
                    inventory.RemoveAt(3);
                }
            }
        }
    }
    private void UseShield()
    {
        //shield stuff
        if (!shieldActive)
        {
            audioSource3.Play();
            shield = Instantiate(shield2Prefab, transform.position, Quaternion.identity);
            shieldActive = true;
        }        
    }

    private void UseDrink()
    {
        audioSource2.Play();
        Debug.Log("used Drink");
    }

    public void UpdateKey(int addkey)
    {
        audioSource.Play();
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

        if(Input.GetKeyDown(KeyCode.R))
        {
           
        }
        //key stuff
        if (onKey && Input.GetKeyDown(KeyCode.E))
        {
            UpdateKey(1);
            Destroy(keyObject);
        }

        if(onKey ||  onGrimisDrink|| onShield)
        {
            pressE.SetActive(true);
        }
        else
        {
            pressE.SetActive(false);
        }

        if(onChest || onDoor)
        {
            pressQ.SetActive(true);
        }
        else
        {         
            pressQ.SetActive(false);
        }
        
        //chest stuff
        if(totalKeys > 0)
        {
            if (onChest && Input.GetKeyDown(KeyCode.Q))
            {
                //take key away from player 
                totalKeys -= 1;
                UpdateKey(0);
                //spawns a rand gameobject / drop then destroys object
                chestRef.OpenChest();
            }
        }

        if (onGrimisDrink && Input.GetKeyDown(KeyCode.E))
        {
            AddToInventory(grimisDrinkPrefab, grimisDrinkObject);            
        }

        if (onShield && Input.GetKeyDown(KeyCode.E))
        {
            AddToInventory(shieldPrefab, shieldObject);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            DeleteItemFromInventory();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioSource5.Play();
            whatsActive = 0;
            Active.transform.position = Slot1.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioSource5.Play();
            whatsActive = 1;
            Active.transform.position = Slot2.transform.position;

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioSource5.Play();
            whatsActive = 2;
            Active.transform.position = Slot3.transform.position;

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            audioSource5.Play();
            whatsActive = 3;
            Active.transform.position = Slot4.transform.position;

        }
        //shield stuff
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
        if (shield != null)
            shield.transform.position = transform.position;
        //door stuff 
        if (onDoor && Input.GetKeyDown(KeyCode.Q))
        {
            FirstStartManager.isFirstStart = false;
            PlayerPrefs.SetInt("PlayerHealth", HealthSystem.health); // Save current health
            PlayerPrefs.SetInt("PlayerScore", ScoreManager.score); // Save current score
            SceneManager.LoadScene("Game1");
        }

        


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
            Debug.Log("On Shield");
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
