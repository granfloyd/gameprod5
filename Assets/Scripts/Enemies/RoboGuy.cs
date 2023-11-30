using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RoboGuy : MonoBehaviour
{
    private Player playerRef;

    public GameObject keyObject;

    public GameObject eProjectilePrefab;

    private Rigidbody2D rb;

    public float eProjectileSpeed = 4.0f;
    
    public float speed = 0.3f;

    private float ticker = 0;

    private float delay = 0.0f;

    private int counter = 0;

    private bool reset = false;

    public bool bHasLOS = false;

    public bool bHasCollided = false;

    public AudioSource audioSource69;
    private ScoreManager scoreManagerRef; // The ScoreManager
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            if (Random.value < 0.5f)
            {
                GameObject spawnthis = Instantiate(keyObject, transform.position, Quaternion.identity);
                
            }
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Shield2.0")
        {
            audioSource69.Play();
            scoreManagerRef.UpdateScore();
            Destroy(gameObject);
        }
    }

    void FollowPlayer()
    {
        Vector2 pos = Vector2.MoveTowards(transform.position, playerRef.transform.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
    }

    void ShootPlayer()
    {
        //clac vector from roboguy - player
        Vector2 direction = (Vector2)(playerRef.transform.position - transform.position);
        direction.Normalize();

        GameObject eprojectile = Instantiate(eProjectilePrefab, transform.position, Quaternion.identity);

        eprojectile.GetComponent<Rigidbody2D>().velocity = direction * eProjectileSpeed;
        
        // Destroy the gameobject 2 seconds after creation
        Destroy(eprojectile, 2.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ticker += Time.deltaTime;
        if (bHasLOS)
        {
            FollowPlayer();
            if (ticker > 3.0f)
            {
                delay += Time.deltaTime;
                if (delay > 0.5f)
                {
                    ShootPlayer();
                    counter += 1;
                    delay = 0;
                }
                if(counter >= 3)
                {
                    reset = true;
                }                
            }
        }
        // Resets 
        if (reset)
        {
            ticker = 0;
            delay = 0;
            counter = 0;
            reset = false;
        }

    }
}