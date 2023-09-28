using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grimis : MonoBehaviour
{
   
    //private float speed = 0.5f;
    private Rigidbody2D rb;
    //private Vector2 movement;
    //bool bHasLOS = false;

    //public Player player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.CompareTag("playerProjectile"))
        {
            Debug.Log("Grimis died");
            Destroy(gameObject);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
