
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

    public AudioSource audioSource;//grimis
    public AudioSource audioSource2;//take dmg
    public AudioSource audioSource3;//heal dmg
    public GameObject pannel;
    public GameObject powerup69active;
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
            audioSource2.Play();
        }
    }

    public void HealDamage(int amount)
    {
        if (health < numOfHearts)
        {
            audioSource3.Play();
            health += amount;
            PlayerPrefs.SetInt("PlayerHealth", health); // Save health to PlayerPrefs            
        }
    }

    void Update()
    {
        if (health > numOfHearts)
            health = numOfHearts;

        UpdateHearts();

        //grimis attack
            if (benis)
            {
                audioSource.Play();
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
        if(health <= 0)
        {
            SceneManager.LoadScene("GameOver");
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
        if (health >= 0)
        {
            string collisionTag = collision.gameObject.tag;

            if (collisionTag == "enemyProjectile" || collisionTag == "Enemy1" || collisionTag == "bossProjectile")
            {
                TakeDamage(1);
            }
            else if (collisionTag == "Blackguy")
            {
                TakeDamage(2);
            }
        }

        if (collision.gameObject.tag == "GrimisAtk")
        {
            benis = true;
            TakeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string colliderTag = collider.gameObject.tag;

        if (colliderTag == "bossAttack2")
        {
            TakeDamage(1);
        }
        //else if (colliderTag == "Heart")
        //{
        //    HealDamage(1);
        //}
    }
}
