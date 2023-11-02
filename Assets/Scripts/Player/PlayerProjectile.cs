using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Grimis")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy1")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Blackguy")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "RoboGuy")
        {
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
