using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyProjectile : NetworkBehaviour
{
    public AudioSource audioSource69;
    // Start is called before the first frame update
    void Start()
    {
        audioSource69 = GameObject.Find("hitSFX").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Shield2.0")
        {
            audioSource69.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "playerProjectile")
        {
            audioSource69.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "GrimisAtk")
        {
            audioSource69.Play();
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
