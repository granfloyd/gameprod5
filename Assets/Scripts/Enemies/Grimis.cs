using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grimis : MonoBehaviour
{
    private Player playerRef;

    public GameObject grimisProjectilePrefab;

    public GameObject keyObject;

    private Rigidbody2D rb;

    public float grimisProjectileSpeed = 4.0f;
   
    public float speed = 0.3f;
    
    private float ticker = 0;

    private float delay = 0.0f;

    private int counter = 0;

    private bool reset = false;

    public bool bHasLOS = false;

    public bool bHasCollided = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRef = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
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
    }

    void ShootPlayer()
    {
        //clac vector from roboguy - player
        Vector2 direction = (Vector2)(playerRef.transform.position - transform.position);
        direction.Normalize();

        GameObject grimisprojectile = Instantiate(grimisProjectilePrefab, transform.position, Quaternion.identity);

        grimisprojectile.GetComponent<Rigidbody2D>().velocity = direction * grimisProjectileSpeed;

        // Destroy the gameobject 2 seconds after creation
        Destroy(grimisprojectile, 2.0f);
    }
    void Update()
    {
        ticker += Time.deltaTime;
        if (bHasLOS)
        {
            if (ticker > 3.0f)
            {
                delay += Time.deltaTime;
                if (delay > 0.5f)
                {
                    ShootPlayer();
                    counter += 1;
                    delay = 0;
                }
                if (counter >= 1)
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

    