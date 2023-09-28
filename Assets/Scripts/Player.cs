using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float ticker = 0;

    public int maxHealth = 100;
    public int currentHealth;
    private Vector3 offset = new Vector3(0,0.2f,0);
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        healthBar.SetHealth(currentHealth);
    }
    // Update is called once per frame
    void Update()
    {
        ticker += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if(ticker >=1)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (Vector2)((mousePos - transform.position ));
                direction.Normalize();
                GameObject projectile = Instantiate(projectilePrefab, transform.position + offset, Quaternion.identity);
                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                // Destroy the gameobject 5 seconds after creation
                Destroy(projectile, 5.0f);
                // Resets ticker
                ticker = 0;
            } 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Grimis")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Grimis collided with player");
            TakeDamage(30);
        }

    }


}
