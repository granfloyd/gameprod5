using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{

    private Player playerRef;

    public GameObject keyObject;

    private Rigidbody2D rb;

    private Vector2 movement;

    private float speed = 0.5f;
    
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
            bHasLOS = true;
           
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Player")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            bHasLOS = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
        }
        if (collision.gameObject.tag == "playerProjectile")
        {
            GameObject spawnthis = Instantiate(keyObject, transform.position, Quaternion.identity);
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
