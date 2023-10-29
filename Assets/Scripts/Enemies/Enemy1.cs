using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private Player playerRef;
    private float speed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    bool bHasLOS = false;
    //public Player player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRef = GameObject.Find("Player").GetComponent<Player>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Player")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("enemy1 has los on player");
            bHasLOS = true;
           
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Player")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("enemy1 has LOST los on player");
            bHasLOS = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Enemy1 collided with player");
        }
        if (collision.gameObject.tag == "playerProjectile")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("enemy1 died");
            Destroy(gameObject);
        }

    }
    void FollowPlayer()
    {
        Vector2 pos = Vector2.MoveTowards(transform.position,playerRef.transform.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (bHasLOS)
        {
            FollowPlayer();
        }
    }
}
