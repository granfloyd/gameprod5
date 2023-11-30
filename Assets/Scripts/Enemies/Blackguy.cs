using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackguy : MonoBehaviour
{

    private Player playerRef;

    private PlayerMovement playerMovementRef;

    private Rigidbody2D rb;

    public GameObject keyObject;

    public float speed = 0.3f;
   
    private float ticker = 0;

    public float fasterfaster = 0.5f;

    public bool bHasLOS = false;

    public bool bHasCollided = false;

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
            GameObject spawnthis = Instantiate(keyObject, transform.position, Quaternion.identity);
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
        if(speed >= 5.0f)
        {
            speed = 0.8f;
        }
    }
}