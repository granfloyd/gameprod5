using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //plr movement
    public float speed = 1.0f;
    private Rigidbody2D rb;
    public Vector2 movement;

    //dash stuff
    private float dashCD = 10;
    private float dashTimer = 0;
    private float dash = 2.0f;
    private bool isDashing = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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
                
            }
        }
        if(!isDashing)
        {
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
        //Debug.Log("dash CD" + dashCD);


    }
    
    

}
