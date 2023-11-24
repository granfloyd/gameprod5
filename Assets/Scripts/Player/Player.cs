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
    public AudioSource audioSource7;//heartbeat sfx
    public Image Active;

    public Canvas myCanvas;
    public PlayerMovement movementRef;
    public Chest chestRef;
    public HealthSystem hsRef;
    public Text keyCountText;
    public GameObject pressE;
    public GameObject pressQ;
    private GameObject keyObject;
    private GameObject chestObject;    
    private GameObject grimisDrinkObject;
    private GameObject heartObject;
    private GameObject shieldObject;
    private GameObject powerup69Object;
    public GameObject projectilePrefab;
    public GameObject aimProjectile;
    public GameObject shieldPrefab;
    public GameObject shield2Prefab;
    public GameObject grimisDrinkPrefab;
    public GameObject heartPrefab;
    public GameObject powerup69Prefab;

    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;

    GameObject shield;
    public List<GameObject> inventory = new List<GameObject>();

    public float projectileSpeed;
    private float ticker = 0;  // shoot timer 
    private float ticker2 = 0; // shield timer
    private float ticker3 = 0; //drink timer
    private float ticker4 = 0; //powerup69 timer
    public int totalKeys = 0;
    private float bonusSpeed = 5.0f;
    
    private Vector3 mousePos;
    private Vector2 aimDirection;

    private bool onKey = false;
    private bool onChest = false; 
    private bool onDoor = false;   
    private bool shieldActive = false;
    private bool drinkActive = false;
    private bool powerup69Active = false;
    private bool onGrimisDrink = false;
    private bool onHeart = false;
    private bool onShield = false;
    private bool onPowerup69 = false;
    private bool full = false;

    public int whatsActive = 0;

    void Start()
    {
        Active = Instantiate(Active, Slot1.transform.position, Quaternion.identity);
        Active.transform.SetParent(myCanvas.transform, false);
        Active.transform.position = Slot1.transform.position;

        aimProjectile = Instantiate(aimProjectile, aimDirection, Quaternion.identity);

        hsRef = GameObject.Find("Player").GetComponent<HealthSystem>();
        movementRef = GameObject.Find("Player").GetComponent<PlayerMovement>();
        chestRef = GameObject.Find("Chest").GetComponent<Chest>();
        UpdateKey(1);
    }

    private void Shoot()
    {
        ticker += Time.deltaTime;
        if (!drinkActive)
        {
            if (Input.GetMouseButton(0))
            {
                if (ticker >= 1)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //calcs a vector from player -  mouse pos
                    Vector2 direction = (Vector2)((mousePos - transform.position));
                    direction.Normalize();

                    GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                    projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

                    //// Destroy the gameobject 5 seconds after creation
                    Destroy(projectile, 5.0f);
                    // Resets ticker
                    ticker = 0;

                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (ticker >= 0.3)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //calcs a vector from player -  mouse pos
                    Vector2 direction = (Vector2)((mousePos - transform.position));
                    direction.Normalize();

                    GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                    projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

                    //// Destroy the gameobject 5 seconds after creation
                    Destroy(projectile, 5.0f);
                    // Resets ticker
                    ticker = 0;
                    Debug.Log("drink active");
                }
            }
        }
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
            inventory.RemoveAt(whatsActive);
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
        if (whatsActive >= 0 && whatsActive < inventory.Count)
        {
            GameObject itemToDelete = GameObject.Find($"UIImage{whatsActive}");
            if (itemToDelete != null)
            {
                Destroy(itemToDelete);
                if (inventory[whatsActive] == grimisDrinkPrefab)
                {
                    UseDrink();
                }
                else if (inventory[whatsActive] == shieldPrefab)
                {
                    UseShield();
                }
                else if (inventory[whatsActive] == powerup69Prefab)
                {
                    UsePowerup69();
                }
                else if (inventory[whatsActive] == heartPrefab)
                {
                    Useheart();
                }
                inventory[whatsActive] = null;
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
        if (!drinkActive)
        {
            audioSource2.Play();
            drinkActive = true;
        }
        
    }

    private void UsePowerup69()
    {
        if (!powerup69Active)
        {
            powerup69Active = true;
        }
        if (powerup69Active)
        {
            hsRef.powerup69active.SetActive(true);
            audioSource7.Play();
            movementRef.speed += bonusSpeed;
        }
    }

    private void Useheart()
    {
        hsRef.HealDamage(1);
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
        Shoot();
        Debug.Log(ticker4);
        if(Input.GetKeyDown(KeyCode.R))
        {
           
        }
        //key stuff
        if (onKey && Input.GetKeyDown(KeyCode.E))
        {
            UpdateKey(1);
            Destroy(keyObject);
        }

        if(onKey ||  onGrimisDrink|| onShield || onPowerup69 || onHeart)
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
        if (onHeart && Input.GetKeyDown(KeyCode.E))
        {
            AddToInventory(heartPrefab, heartObject);
        }
        if (onShield && Input.GetKeyDown(KeyCode.E))
        {
            AddToInventory(shieldPrefab, shieldObject);
        }

        if (onPowerup69 && Input.GetKeyDown(KeyCode.E))
        {
            AddToInventory(powerup69Prefab, powerup69Object);
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

        //drink stuff
        if (drinkActive)
        {
            ticker3 += Time.deltaTime;

            if (ticker3 > 10)
            {
                ticker3 = 0;
                drinkActive = false;
            }
        }
        //powerup69 stuff
        if (powerup69Active)
        {
            ticker4 += Time.deltaTime;
            if (ticker4 > 5)
            {
                ticker4 = 0;
                hsRef.powerup69active.SetActive(false);
                powerup69Active = false;
                movementRef.speed = 1.0f;
                audioSource7.Stop();
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
        if (collider.gameObject.tag == "Powerup69")
        {
            onPowerup69 = true;
            powerup69Object = collider.gameObject;
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
        if (collider.gameObject.tag == "Powerup69")
        {
            onPowerup69 = false;
            powerup69Object = collider.gameObject;
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
