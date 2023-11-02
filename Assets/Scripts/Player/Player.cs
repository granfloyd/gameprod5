using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject aimProjectile;
    public float projectileSpeed;
    public float ticker = 0;

    private Vector3 mousePos;
    private Vector2 aimDirection;

    public List<GameObject> keys = new List<GameObject>();

    void Start()
    {
        //old
        //currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
        
        aimProjectile = Instantiate(aimProjectile, aimDirection, Quaternion.identity);
    }
   
    // Update is called once per frame
    void Update()
    {
        RotateAim();
        ticker += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if(ticker >=1)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
                //calcs a vector from player -  mouse pos
                Vector2 direction = (Vector2)((mousePos - transform.position ));
                direction.Normalize();

                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
               
                //// Destroy the gameobject 5 seconds after creation
                Destroy(projectile, 5.0f);
                // Resets ticker
                ticker = 0;
                
            } 
        }
        


    }
    void RotateAim()
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (Vector2)((mousePos - transform.position));
        aimDirection.Normalize();
        // Update the position of aimProjectile to follow the player
        aimProjectile.transform.position = (Vector2)transform.position + aimDirection * 0.3f;

        //Calculates the angle of aimDirection, adjusting for a sprite that faces up
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;

        // Rotate aimProjectile to face towards the mouse cursor
        aimProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);

    }
    
}
