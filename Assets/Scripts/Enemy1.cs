using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Transform target; 
    private float speed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    bool bHasLOS = false;
    
        
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.name == "Player")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("enemy1 has los on player");
            bHasLOS = true;
           
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.name == "Player")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("enemy1 has LOST los on player");
            bHasLOS = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Player")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Enemy1 collided with player");
        }

    }
    void FollowPlayer()
    {
        Vector2 pos = Vector2.MoveTowards(transform.position,target.position, speed * Time.deltaTime);
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
