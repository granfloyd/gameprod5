using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerCollision : MonoBehaviour
{
    public Player playerRef;
    public Item itemRef;
    public GeneralUI genUIRef;
    private GameObject keyObject;
    private GameObject grimisDrinkObject;
    private GameObject heartObject;
    private GameObject shieldObject;
    private GameObject powerup69Object;
    private GameObject chestObject;
    public GameObject pressE;
    public GameObject pressQ;
    private bool onKey = false;
    private bool onChest = false;
    private bool onDoor = false;
    private bool onGrimisDrink = false;
    private bool onHeart = false;
    private bool onShield = false;
    private bool onPowerup69 = false;
    //shop stuff
    public GameObject shopUI;
    public Button closeButton;
    public Button buyHomingButton;
    public Button buySpreadButton;
    public Text shopkeyCountText;
    public Text costText;
    public GameObject Cursorgo;
    private CanvasGroup canvasGroup;
    public int cost = -25;
    public int inflation = -25;
    // Start is called before the first frame update
    void Start()
    {
        shopUI.SetActive(false);//make sure shops always close when starting up
        closeButton.onClick.AddListener(CloseShop);
        buySpreadButton.onClick.AddListener(MoreSpread);
        buyHomingButton.onClick.AddListener(MoreHoming);
        genUIRef = GameObject.Find("GeneralUI").GetComponent<GeneralUI>();
        playerRef = GetComponent<Player>();
        itemRef = GetComponent<Item>();
        DisplayCost();
        canvasGroup = Cursorgo.GetComponent<CanvasGroup>();
        // If the CanvasGroup component doesn't exist, add one
        if (canvasGroup == null)
        {
            canvasGroup = Cursorgo.AddComponent<CanvasGroup>();
        }

        canvasGroup.blocksRaycasts = false;
    }
    public void DisplayCost()
    {
        costText.text = cost.ToString();
    }
    void Update()
    {
        InteractQ();
        PickUpE();
        Cursor.visible = false;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Cursorgo.transform.position = Input.mousePosition;
    }
    void CloseShop()
    {
        shopUI.SetActive(false);
        playerRef.Resume();
    }
    void MoreSpread()
    {
        if (GeneralUI.totalKeys >= -cost) // Check if player has enough keys
        {
            Debug.Log("Purchase successful");
            genUIRef.UpdateKey(cost);
            GeneralUI.shootSpread++;
            cost = cost += inflation;
            DisplayCost();
        }
    }
    void MoreHoming()
    {
        if (GeneralUI.totalKeys >= -cost) // Check if player has enough keys
        {
            Debug.Log("Purchase successful");
            genUIRef.UpdateKey(cost);
            GeneralUI.shootSpread++;
            cost = cost += inflation;
            DisplayCost();
        }
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

        //door stuff 
        if (onDoor && Input.GetKeyDown(KeyCode.Q))
        {
            FirstStartManager.isFirstStart = false;
            PlayerPrefs.SetInt("PlayerHealth", HealthSystem.health); // Save current health
            PlayerPrefs.SetInt("PlayerScore", GeneralUI.score); // Save current score
            PlayerPrefs.SetInt("PlayerKeys", GeneralUI.totalKeys);
            PlayerPrefs.SetInt("PlayerShootSpread", GeneralUI.shootSpread);
            PlayerPrefs.SetInt("PlayerHomingCharges", GeneralUI.homingCharges);
            SceneManager.LoadScene("OverWorld");
        }
        if(onChest && Input.GetKeyDown(KeyCode.Q))
        {
            shopUI.SetActive(true);
            playerRef.Pause();
        }
    }
    private void PickUpE()
    {
        //key stuff
        if (onKey && Input.GetKeyDown(KeyCode.E))
        {
            genUIRef.UpdateKey(1);
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
            case "Door":
                onDoor = true;
                break;
            case "Chest":
                onChest = true;
                chestObject = collider.gameObject;
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
        string tag = collider.gameObject.tag;

        switch (tag)
        {
            case "Key":
                onKey = false;
                keyObject = collider.gameObject;
                break;
            case "Chest":
                onChest = false;
                chestObject = collider.gameObject;
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
