using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private ScoreManager scoreManagerRef; // The ScoreManager
    //private AudioSource audioSource;

    private Player playerRef;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GameObject.Find("hitSFX").GetComponent<AudioSource>();
        scoreManagerRef = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
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
            playerRef.dispair += 1;
        }
        if (collision.gameObject.tag == "Enemy1")
        {
            //audioSource.Play();
            Destroy(gameObject);
            scoreManagerRef.UpdateScore();
            playerRef.dispair += 1;
        }
        if (collision.gameObject.tag == "Blackguy")
        {
            //audioSource.Play();
            Destroy(gameObject);
            scoreManagerRef.UpdateScore();
            playerRef.dispair += 1;
        }
        if (collision.gameObject.tag == "RoboGuy")
        {
            //audioSource.Play();
            Destroy(gameObject);
            scoreManagerRef.UpdateScore();
            playerRef.dispair += 1;
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
