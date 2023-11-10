using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Blackguy blackguyRef;

    private Rigidbody2D rb;

    public Vector2 movement;

    public float speed = 1.0f;
    
    //dash stuff
    private float dashCD = 10;

    private float dashTimer = 0;

    private float dash = 2.0f;

    private bool isDashing = false;

    //slow stuff 
    private float slow = 0.2f;

    public float slowDuration = 0;

    public bool isSlowed = false;

    public bool bHasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        blackguyRef = GameObject.Find("Blackguy").GetComponent<Blackguy>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Blackguy")
        {
            bHasCollided = true;
        }
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Blackguy")
        {
            bHasCollided = false;
        }
    }
    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        dashCD += Time.deltaTime;

        if (bHasCollided && !isSlowed)
        {
            isSlowed = true;
            speed = slow;
        }

        if (isSlowed)
        {
            slowDuration += Time.deltaTime;
            if (slowDuration > 2.0f)
            {
                slowDuration = 0;
                isSlowed = false;
                speed = 1.0f;
            }
        }
      
        if (dashCD > 5.0f)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                isDashing = true;
                dashTimer = 0;
                dashCD = 0;
            }            
        }
        
        if(isDashing)
        {
            dashTimer += Time.deltaTime;
            rb.MovePosition(rb.position + movement * speed * dash * Time.deltaTime);

            Debug.Log("Zoom");
            if(dashTimer > 0.5f)
            {
                isDashing = false;
                dashTimer = 0;
                
            }
        }
        if(!isDashing)
        {
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
        //Debug.Log("dash CD" + dashCD);


    }
    
    

}
