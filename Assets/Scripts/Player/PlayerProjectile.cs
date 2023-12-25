using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerProjectile : NetworkBehaviour
{
    private ScoreManager scoreManagerRef; // The ScoreManager
    //private AudioSource audioSource;

    public Player playerRef;
    public GameObject playerGO;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GameObject.Find("hitSFX").GetComponent<AudioSource>();
        scoreManagerRef = playerGO.GetComponent<ScoreManager>();
        playerRef = GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Grimis")
        {
            //audioSource.Play();
            Destroy(gameObject);
            scoreManagerRef.UpdateScore();
            //playerRef.dispair += 1;
        }
        if (collision.gameObject.tag == "Enemy1")
        {
            //audioSource.Play();
            Destroy(gameObject);
            scoreManagerRef.UpdateScore();
            //playerRef.dispair += 1;
        }
        if (collision.gameObject.tag == "Blackguy")
        {
            //audioSource.Play();
            Destroy(gameObject);
            scoreManagerRef.UpdateScore();
            //playerRef.dispair += 1;
        }
        if (collision.gameObject.tag == "RoboGuy")
        {
            //audioSource.Play();
            Destroy(gameObject);
            scoreManagerRef.UpdateScore();
            //playerRef.dispair += 1;
        }
        if (collision.gameObject.tag == "Wall")
        {   
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "enemyProjectile")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "bossProjectile")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "GrimisAtk")
        {
            Destroy(gameObject);
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
