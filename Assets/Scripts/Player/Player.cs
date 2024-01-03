using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //refs//
    public PlayerMovement movementRef;
    public HealthSystem hsRef;
    public PlayerCollision PC;
    public Item itemRef;
    public GeneralUI genUIRef;
    //audio stuff//
    public AudioSource audioSourceAddToTory;//addtoinventory
    public AudioSource audioSourceSelector;//select
    public AudioSource audioSourceCant;//cant pickup item SFX
    public AudioSource audioSourcePortalSpawn;//portal sfx   
    //gameobjects//
    public GameObject projectilePrefab;
    public GameObject aimProjectile;
    public GameObject pauseMenuUI;
    public GameObject portal2prefab;
    public Canvas healthUI;
    //player stats//
    public float shootCD = 1f;
    public int playerDamage = 1;
    public float projectileSpeed;
    private float ticker = 0;  // shoot timer 
    //player inventory//
    public List<GameObject> inventory = new List<GameObject>();
    public int whatsActive = 0;
    //other//
    private Vector3 mousePos;
    private Vector2 aimDirection;
    public static bool GameIsPaused = false;
    //for boss / part of cleanup 2//
    public bool isBossActive = false;
    public int thing = 0;
    public int dispair = 0;//for portal
    private bool spawned = false;//for portal    

    void Start()
    {
        aimProjectile = Instantiate(aimProjectile, aimDirection, Quaternion.identity);
        genUIRef = GameObject.Find("GeneralUI").GetComponent<GeneralUI>();
        hsRef = GetComponent<HealthSystem>();
        movementRef = GetComponent<PlayerMovement>();
        PC = GetComponent<PlayerCollision>();
        itemRef = GetComponent<Item>();
    }

    private void Shoot()
    {
        ticker += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if (ticker >= shootCD)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //calcs a vector from player -  mouse pos
                Vector2 direction = (Vector2)((mousePos - transform.position));
                direction.Normalize();
                //3 projectiles with directions deviated by -35, 0, 35 degrees
                for (int i = -GeneralUI.shootSpread; i <= GeneralUI.shootSpread; i++)
                {
                    GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    Vector2 rotatedDirection = Quaternion.Euler(0, 0, i * 10) * direction; // You can adjust the value '10' to control the angle between projectiles
                    projectile.GetComponent<Rigidbody2D>().velocity = rotatedDirection * projectileSpeed;
                    Destroy(projectile, 5.0f);
                }
                ticker = 0;
            }
        }
    }

    //add picked up item to ui slots
    private void GuyzGamezLovesSlots(GameObject type)
    {
        genUIRef.imageObject = new GameObject("UIImage" + whatsActive);
        genUIRef.imageObject.transform.SetParent(genUIRef.genUIcanvan.transform, false);
        genUIRef.image = genUIRef.imageObject.AddComponent<Image>();        
        if (inventory.Any())
        {
            type = inventory.ElementAt(whatsActive);
            if (type != null)
            {
                if (whatsActive == 0)
                    genUIRef.image.transform.position = genUIRef.Slot1.transform.position;                  
                if (whatsActive == 1)
                    genUIRef.image.transform.position = genUIRef.Slot2.transform.position;                   
                if (whatsActive == 2)
                    genUIRef.image.transform.position = genUIRef.Slot3.transform.position;                    
                if (whatsActive == 3)
                    genUIRef.image.transform.position = genUIRef.Slot4.transform.position;
                  
                // Update the sprite of the existing UI Image
                SpriteRenderer sr = type.GetComponent<SpriteRenderer>();
                genUIRef.image.sprite = sr.sprite;
            }
        }
    }

    public void AddToInventory(GameObject itemToAdd,GameObject itemToDelete)
    {
        int inventorySize = inventory.Count;
        for (int i = whatsActive; i < inventorySize; i++)
        {
            if (inventory[i] == null)
            {
                whatsActive = i;
                inventory.RemoveAt(i);
                audioSourceAddToTory.Play();
                inventory.Insert(i, itemToAdd);
                GuyzGamezLovesSlots(itemToAdd);
                Destroy(itemToDelete);
                return;
            }
        }
        for (int i = whatsActive; i < inventorySize; i--)
        {
            if (i < 0)
            {
                audioSourceCant.Play();//NO SPACE LEFT
                audioSourceCant.Play();//NO SPACE LEFT
                return;
            }
            else if (inventory[i] == null)
            {
                whatsActive = i;
                inventory.RemoveAt(i);
                audioSourceAddToTory.Play();
                inventory.Insert(i, itemToAdd);
                GuyzGamezLovesSlots(itemToAdd);
                Destroy(itemToDelete);
                return;
            }
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
                audioSourceSelector.Play();
                // Scrolling up
                whatsActive += 1;
                if (whatsActive > 3)
                {
                    audioSourceSelector.Play();
                    whatsActive = 0;
                }
            }
        }
        if (whatsActive > -1)
        {
            if (scroll < 0f)
            {
                audioSourceSelector.Play();
                // Scrolling down
                whatsActive -= 1;
                if (whatsActive < 0)
                {
                    audioSourceSelector.Play();
                    whatsActive = 3;
                }
            }
        }
    }
    void Update()
    {
        //Cursor.visible = false;
        RotateAim();
        Shoot();
        ScrollingControl();
        RemoveFromInventory();

        if (Input.GetKeyDown(KeyCode.Space))//use item
            DeleteItemFromInventory();

        if (Input.GetKeyDown(KeyCode.P))//pause
        {
            if (GameIsPaused)
            {
                genUIRef.UpdatePermishPowerUpsUI();
                genUIRef.page.SetActive(false);
                Resume();
            }
            else
            {
                genUIRef.page.SetActive(true);
                Pause();
            }
        }

        if (!GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                audioSourceSelector.Play();
                whatsActive = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                audioSourceSelector.Play();
                whatsActive = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                audioSourceSelector.Play();
                whatsActive = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                audioSourceSelector.Play();
                whatsActive = 3;
            }

            if (whatsActive == 0)
            {
                genUIRef.SelectorIMG.transform.position = genUIRef.Slot1.transform.position;
            }
            else if (whatsActive == 1)
            {
                genUIRef.SelectorIMG.transform.position = genUIRef.Slot2.transform.position;
            }
            else if (whatsActive == 2)
            {
                genUIRef.SelectorIMG.transform.position = genUIRef.Slot3.transform.position;
            }
            else if (whatsActive == 3)
            {
                genUIRef.SelectorIMG.transform.position = genUIRef.Slot4.transform.position;
            }
        }        
    }
    public void Resume()
    {
        genUIRef.SelectorIMG.gameObject.SetActive(true);
        genUIRef.generalUIGO.SetActive(true);
        healthUI.gameObject.SetActive(true);
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Resume all sounds
        AudioListener.volume = 1;

        // Enable mouse input
        Cursor.lockState = CursorLockMode.None;
    }

    public void Pause()
    {
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        genUIRef.SelectorIMG.gameObject.SetActive(false);
        healthUI.gameObject.SetActive(false);
        // Pause all sounds
        AudioListener.volume = 0.25f;

        // Disable mouse input
        //Cursor.lockState = CursorLockMode.Locked;
        genUIRef.generalUIGO.SetActive(false);
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
}
