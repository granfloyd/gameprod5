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
        if (collision.gameObject.name == "Grimis")
        {
            Debug.Log("Grimis got shot");
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Enemy1")
        {
            Debug.Log("Enemy1 got shot");
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Blackguy")
        {
            Debug.Log("Blackguy got shot");
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
