using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private ScoreManager scoreManagerRef; // The ScoreManager
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("hitSFX").GetComponent<AudioSource>();
        scoreManagerRef = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Grimis")
        {
            audioSource.Play();
            Destroy(gameObject);
            scoreManagerRef.UpdateScore();
        }
        if (collision.gameObject.tag == "Enemy1")
        {
            audioSource.Play();
            Destroy(gameObject);
            scoreManagerRef.UpdateScore();
        }
        if (collision.gameObject.tag == "Blackguy")
        {
            audioSource.Play();
            Destroy(gameObject);
            scoreManagerRef.UpdateScore();
        }
        if (collision.gameObject.tag == "RoboGuy")
        {
            audioSource.Play();
            Destroy(gameObject);
            scoreManagerRef.UpdateScore();
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
