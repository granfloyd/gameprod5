using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackguy : MonoBehaviour
{
    private Player playerRef;
    public float speed = 0.8f;
    private Rigidbody2D rb;
    public bool bHasLOS = false;
    public bool bHasCollided = false;
    private float ticker = 0;
    public float fasterfaster = 0.1f;

    //slow stuff
    private PlayerMovement playerMovementRef;
    private float slow = 0.2f;
    private bool isSlowed = false;
    private float slowDuration = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRef = GameObject.Find("Player").GetComponent<Player>();
        playerMovementRef = GameObject.Find("Player").GetComponent<PlayerMovement>();

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Player")
        {
            bHasLOS = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Player")
        {
            bHasLOS = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            bHasCollided = true;

        }
        if (collision.gameObject.tag == "playerProjectile")
        {
            Destroy(gameObject);
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
    void FollowPlayer()
    {
        Vector2 pos = Vector2.MoveTowards(transform.position, playerRef.transform.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (bHasCollided && !isSlowed)
        {
            isSlowed = true;
            playerMovementRef.speed = slow;
        }

        if (isSlowed)
        {
            slowDuration += Time.deltaTime;
            if (slowDuration > 2.0f)
            {
                slowDuration = 0;
                isSlowed = false;
                playerMovementRef.speed = 1.0f;
            }
        }

        ticker += Time.deltaTime;
        if (bHasLOS)
        {
            speed += fasterfaster * Time.deltaTime;
            
            //if collided with player pause
            if(bHasCollided)
            {
                ticker = 0;
            }
            if(ticker > 3.0f)//grace period
            {
                FollowPlayer();
            }

        }
        else
        {
            speed = 0.8f;
        }
        if(speed >= 2.0f)
        {
            speed = 0.8f;
        }
    }
}