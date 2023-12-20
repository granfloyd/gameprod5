using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerMovement movementRef;
    public HealthSystem hsRef;
    public PlayerCollision PC;
    public Item itemRef;
    GameObject imageObject;
    Image image;
    public AudioSource audioSource4;//addtoinventory
    public AudioSource audioSource5;//select
    public AudioSource audioSource6;//cant pickup item SFX
    public AudioSource audioSource8;//portal sfx
    public Image Active;
    public Canvas myCanvas;
    public GameObject projectilePrefab;
    public GameObject aimProjectile;
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;

    public float shootCD = 1f;
    //GameObject shield;
    public List<GameObject> inventory = new List<GameObject>();
    public static int playerDamage = 1;
    public float projectileSpeed;
    private float ticker = 0;  // shoot timer 
    private Vector3 mousePos;
    private Vector2 aimDirection;
    public bool isBossActive = false;
    public int thing = 0;
    public int whatsActive = 0;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public int dispair = 0;
    private bool spawned = false;
    public GameObject portal2prefab;
    private object position;
    void Start()
    {
        Active = Instantiate(Active, Slot1.transform.position, Quaternion.identity);
        Active.transform.SetParent(myCanvas.transform, false);
        Active.transform.position = Slot1.transform.position;
        aimProjectile = Instantiate(aimProjectile, aimDirection, Quaternion.identity);

        hsRef = GameObject.Find("Player").GetComponent<HealthSystem>();
        movementRef = GameObject.Find("Player").GetComponent<PlayerMovement>();
        PC = GetComponent<PlayerCollision>();
        itemRef = GetComponent<Item>();
    }

    private void Shoot()
    {
        ticker += Time.deltaTime;
        {
            if (Input.GetMouseButton(0))
            {
                if (ticker >= shootCD)
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

    public void AddToInventory(GameObject itemToAdd,GameObject itemToDelete)
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
                if (inventory[whatsActive] == itemRef.grimisDrinkPrefab)
                {
                    itemRef.Ability(() => itemRef.StartCoroutine(itemRef.UseDrink(10)));
                }
                else if (inventory[whatsActive] == itemRef.shieldPrefab)
                {
                    itemRef.Ability(() => itemRef.StartCoroutine(itemRef.UseShield(10)));
                }
                else if (inventory[whatsActive] == itemRef.powerup69Prefab)
                {
                    itemRef.Ability(() => itemRef.StartCoroutine(itemRef.UsePowerup69(10)));
                }
                else if (inventory[whatsActive] == itemRef.heartPrefab)
                {
                    itemRef.Ability(itemRef.Useheart);
                }
                inventory[whatsActive] = null;
            }
        }
    }
    private void RemoveFromInventory()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (whatsActive >= 0 && whatsActive < inventory.Count)
            {
                GameObject itemToDelete = GameObject.Find($"UIImage{whatsActive}");
                if (itemToDelete != null)
                {
                    Destroy(itemToDelete);
                    inventory[whatsActive] = null;
                }
            }
        }
    }
    private void ScrollingControl()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (whatsActive < 4)
        {
            if (scroll > 0f)
            {
                audioSource5.Play();
                // Scrolling up
                whatsActive += 1;
                if (whatsActive > 3)
                {
                    audioSource5.Play();
                    whatsActive = 0;
                }
            }
        }
        if (whatsActive > -1)
        {
            if (scroll < 0f)
            {
                audioSource5.Play();
                // Scrolling down
                whatsActive -= 1;
                if (whatsActive < 0)
                {
                    audioSource5.Play();
                    whatsActive = 3;
                }
            }
        }
    }
    void Update()
    {
        //Cursor.visible = false;
        SpawnPortal();
        RotateAim();
        Shoot();
        ScrollingControl();
        RemoveFromInventory();

        if (Input.GetKeyDown(KeyCode.Space))
            DeleteItemFromInventory();

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (!GameIsPaused)
        {
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
            if (whatsActive == 0)
            {
                Active.transform.position = Slot1.transform.position;
            }
            else if (whatsActive == 1)
            {
                Active.transform.position = Slot2.transform.position;
            }
            else if (whatsActive == 2)
            {
                Active.transform.position = Slot3.transform.position;
            }
            else if (whatsActive == 3)
            {
                Active.transform.position = Slot4.transform.position;
            }
        }        
    }
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Resume all sounds
        AudioListener.volume = 1;

        // Enable mouse input
        Cursor.lockState = CursorLockMode.None;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Pause all sounds
        AudioListener.volume = 0;

        // Disable mouse input
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    
    void RotateAim()
    {
        if(!GameIsPaused)
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
    void SpawnPortal()
    {
        if (dispair >= 15)
        {
            
            if (!spawned)                
            {
                Instantiate(portal2prefab, transform.position, Quaternion.identity);
                audioSource8.Play();
            }
            
            spawned = true;
        }
             
    }
}
