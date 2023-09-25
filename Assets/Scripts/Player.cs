using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public GameObject player;
    //public Vector2 playerPosition = player.transform.position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    //void OnCollisionEnter(Collision


    void OnTriggerEnter2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.name == "Enemy1")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("enemy1 has los on player");
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "MyGameObjectTag")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.name == "Enemy1")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("enemy1 has LOST los on player");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Enemy1")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("player collided with Enemy1");
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "MyGameObjectTag")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
    }

}
