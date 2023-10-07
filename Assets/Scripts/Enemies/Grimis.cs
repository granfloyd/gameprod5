using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Grimis : MonoBehaviour
{
    private Rigidbody2D rb;
    private float ticker = 0;
    private float speed = 3.0f;


    //bool bHasLOS = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
       
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


}

    