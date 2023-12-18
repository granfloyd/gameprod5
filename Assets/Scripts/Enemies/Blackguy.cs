using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackguy : MonoBehaviour
{
    private BasicEnemyLOS belos;
    private Rigidbody2D rb;

    //enemy stats
    public int HP = 2;
    public float speed = 0.3f;

    //cds
    private float ticker = 0;
    public float fasterfaster = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        belos = gameObject.GetComponent<BasicEnemyLOS>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        belos.EnemyTakeDamage(ref HP, Player.playerDamage);
        ticker += Time.deltaTime;
        if (belos.bHasLOS)
        {
            speed += fasterfaster * Time.deltaTime;
            //if collided with player pause
            if (belos.bHasCollided)
            {
                ticker = 0;
            }
            if(ticker > 3.0f)//grace period
            {
                belos.FollowPlayer(rb,speed);
            }
            
        }
        else
        {
            speed = 0.8f;
        }
        if (speed >= 5.0f)
        {
            speed = 0.8f;
        }
    }
}