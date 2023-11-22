
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    //position stuff
    public static int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject pannel;
    public bool benis = false;
    private float ticker = 0;

    void Start()
    {
        if (FirstStartManager.isFirstStart)
        {
            health = 10;
            PlayerPrefs.SetInt("PlayerHealth", health); // Save health to PlayerPrefs
        }
        else
        {
            // Load health from PlayerPrefs
            health = PlayerPrefs.GetInt("PlayerHealth", 10);
        }
    }


    public void TakeDamage(int dmg)
    {
        if (health > 0)
        {
            health -= dmg;
            PlayerPrefs.SetInt("PlayerHealth", health); // Save health to PlayerPrefs
        }
    }

    public void HealDamage(int amount)
    {
        if (health < numOfHearts)
        {
            health += amount;
            PlayerPrefs.SetInt("PlayerHealth", health); // Save health to PlayerPrefs
        }
    }

    void Update()
    {
        if (health > numOfHearts)
            health = numOfHearts;

        UpdateHearts();

            if (benis)
            {
                pannel.SetActive(true);
                ticker += Time.deltaTime;
                if (ticker > 0.3f)
                {
                    benis = false;
                }
            }
            if (!benis)
            {
                ticker = 0;
                pannel.SetActive(false);
            }
        Debug.Log("HEALTH: "+ health);
        if(health == 0)
        {
            SceneManager.LoadScene("TitleScreen");
            FirstStartManager.isFirstStart = true;
        }

    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            

        }
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        ////Check for a match with the specified name on any GameObject that collides with your GameObject
        if(health > 0)
        {
            if (collision.gameObject.name == "Grimis")
            {
                TakeDamage(1);
            }
            if (collision.gameObject.CompareTag("enemyProjectile"))
            {

                TakeDamage(1);
            }
            if (collision.gameObject.CompareTag("Enemy1"))
            {
                TakeDamage(1);
            }
        }
        
        if (collision.gameObject.CompareTag("GrimisAtk"))
        {

            benis = true;
            
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Heart"))
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            HealDamage(1);
        }
    }


}
