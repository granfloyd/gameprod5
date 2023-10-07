using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackguy : MonoBehaviour
{
    private Player playerRef;
    private float speed = 0.8f;
    private Rigidbody2D rb;
    public bool bHasLOS = false;
    public bool bHasCollided = false;
    float ticker = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRef = GameObject.Find("Player").GetComponent<Player>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.name == "Player")
        {
            Debug.Log("Blackguy HAS los on player");
            bHasLOS = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.name == "Player")
        {
            Debug.Log("Blackguy has LOST los on player");
            bHasLOS = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Blackguy collided with player");
            bHasCollided = true;

        }
        if (collision.gameObject.CompareTag("playerProjectile"))
        {
            Debug.Log("blackguy died");
            Destroy(gameObject);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Blackguy EXIT");
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

            if(bHasCollided)
            {
                ticker = 0;
            }
            if(ticker > 3.0f)
            {
                FollowPlayer();
            }

        }
    }
}