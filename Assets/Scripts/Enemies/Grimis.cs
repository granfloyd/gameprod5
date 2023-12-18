using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grimis : MonoBehaviour
{
    private BasicEnemyLOS belos;
    public GameObject grimisProjectilePrefab;
    private Rigidbody2D rb;
    public int HP = 2;
    public float grimisProjectileSpeed = 4.0f;
   
    public float speed = 0.3f;    
    private float ticker = 0;
    private float delay = 0.0f;
    private int counter = 0;
    private bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        belos = gameObject.GetComponent<BasicEnemyLOS>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        belos.EnemyTakeDamage(ref HP, Player.playerDamage);
        ticker += Time.deltaTime;
        if (belos.bHasLOS)
        {
            if (ticker > 3.0f)
            {
                delay += Time.deltaTime;
                if (delay > 0.5f)
                {
                    belos.ShootPlayer(grimisProjectilePrefab,grimisProjectileSpeed);
                    counter += 1;
                    delay = 0;
                }
                if (counter >= 1)
                {
                    reset = true;
                }
            }
        }
        // Resets 
        if (reset)
        {
            ticker = 0;
            delay = 0;
            counter = 0;
            reset = false;
        }
    }



}

    