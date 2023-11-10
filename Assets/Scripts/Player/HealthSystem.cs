
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    //position stuff
    public int health = 10;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject pannel;
    public bool benis = false;
    private float ticker = 0;

    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    public void HealDamage(int amount)
    {
        health += amount;
    }
    // Update is called once per frame
    void Update()
    {
        if (health > numOfHearts)
            health = numOfHearts;

        UpdateHearts();

            if (benis)
            {
                pannel.SetActive(true);
                ticker += Time.deltaTime;
                if (ticker > 3.0f)
                {
                    benis = false;
                }
            }
            if (!benis)
            {
                ticker = 0;
                pannel.SetActive(false);
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
