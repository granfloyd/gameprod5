using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //plr movement
    public float speed = 1.0f;
    private Rigidbody2D rb;
    public Vector2 movement;

    //slow stuff
    private Blackguy blackguyRef;
    private float slow = 0.2f;
    private float slowTimer = 0;
    private bool isSlowed = false;
    private float slowDuration = 0;

    //dash stuff
    private float dashCD = 10;
    private float dashTimer = 0;
    private float dash = 2.0f;
    private bool isDashing = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        blackguyRef = GameObject.FindObjectOfType<Blackguy>();
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (blackguyRef.bHasCollided)
        {
            if (!isSlowed)
            {
                slowTimer += Time.deltaTime;
                if (slowTimer >= 0.2f)
                {
                    isSlowed = true;
                    speed = slow;
                    slowTimer = 0;
                }
            }
            else
            {
                slowDuration += Time.deltaTime;
                if (slowDuration >= 2)
                {
                    slowDuration = 0;
                    isSlowed = false;
                    speed = 1.0f;
                }
            }
        }
        dashCD += Time.deltaTime;
        if(dashCD > 5.0f)
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
                dashCD = 0;
                
            }
        }
        if(!isDashing)
        {
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
        //Debug.Log("dash CD" + dashCD);


    }
    
    

}
