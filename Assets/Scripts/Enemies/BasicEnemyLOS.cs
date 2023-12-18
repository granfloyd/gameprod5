using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class BasicEnemyLOS : MonoBehaviour
{
    private Player playerRef;
    public AudioSource audioSource69;
    private ScoreManager scoreManagerRef; // The ScoreManager

    public GameObject keyObject;
    public float Speed;
    public float ff;
    public bool bHasLOS = false;
    public bool bHasCollided = false;
    public bool isHit = false;
    public bool isDead = false;

    void Start()
    {
        playerRef = GameObject.Find("Player").GetComponent<Player>();
        scoreManagerRef = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        audioSource69 = GameObject.Find("hitSFX").GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            bHasLOS = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            bHasLOS = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerProjectile")
        {
            isHit = true;
            bHasCollided = true;
            //Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Shield2.0")
        {
            isHit = true;
            audioSource69.Play();
           
        }
        if (collision.gameObject.tag == "Player")
        {
            bHasCollided = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            bHasCollided = false;
        }
    }

    public void EnemyTakeDamage(ref int enemyhp,int howmuch)    //to pervent dup methods in enemy scripts 
    {
        if(isHit)
        {
            Debug.Log("enemy hit");
            enemyhp -= howmuch;
            isHit = false;
        }   
        if(enemyhp <= 0)
        {
            Debug.Log("deleteing enemy");
            isDead = true;
        }
    }
    public void OnDeath(GameObject drops)
    {
        if(isDead)
        {
            Debug.Log("on death called!");
            scoreManagerRef.UpdateScore();
            if (Random.value < 0.5f)    
            {
                GameObject spawnthis = Instantiate(keyObject, transform.position, Quaternion.identity);
            }
            if(drops != null)
            {
                GameObject spawnthis2 = Instantiate(keyObject, transform.position, Quaternion.identity);
            }
            
            //play deathsound here 
            Destroy(gameObject);
            isDead = false;
        }
    }
    public void ShootPlayer(GameObject enemyProjectile,float speed)
    {
        Vector2 direction = (Vector2)(playerRef.transform.position - transform.position);
        direction.Normalize();
        GameObject eprojectile = Instantiate(enemyProjectile, transform.position, Quaternion.identity);
        eprojectile.GetComponent<Rigidbody2D>().velocity = direction * speed;
        Destroy(eprojectile, 2.0f);
    }

    public void FollowPlayer(Rigidbody2D rb, float speed)
    {
        Vector2 pos = Vector2.MoveTowards(transform.position, playerRef.transform.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
    }

    // Update is called once per frame
    void Update()
    {
        OnDeath(null);
    }
}
