using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 movement;

    public float speed = 1.0f;
    public const int originalSpeed = 1;

    //dash stuff
    private float dashCD = 2;
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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Blackguy")
        {
            bHasCollided = true;
        }
        if (collision.gameObject.tag == "bossProjectile")
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
        if (collision.gameObject.tag == "bossProjectile")
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
            bHasCollided = false;
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
        
        if (dashCD > 2.0f)
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
    }
    
    

}
