using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grimis : MonoBehaviour
{
    public GameObject grimisProjectilePrefab;
    public float grimisProjectileSpeed = 4.0f;

    private Player playerRef;
    public float speed = 0.3f;
    private Rigidbody2D rb;

    public bool bHasLOS = false;
    public bool bHasCollided = false;

    private float ticker = 0;
    private float delay = 0.0f;
    private int counter = 0;
    private bool reset = false;


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
            Debug.Log("grimis HAS los on player");
            bHasLOS = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("grimis has LOST los on player");
            bHasLOS = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerProjectile")
        {
            Debug.Log("grimis got shot deleting grimis....");
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

    