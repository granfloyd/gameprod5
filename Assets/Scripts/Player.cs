using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float ticker = 0;
    void Start()
    {
       
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
                //Vector3 offset = new Vector3(0, 0.2f, 0);
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                // Destroy the gameobject 5 seconds after creation
                Destroy(projectile, 5.0f);
                // Resets ticker
                ticker = 0;
            }         

        }
        //Debug.Log(ticker);        

    }



}
