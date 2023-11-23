using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("hitSFX").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "GrimisAtk")
        {
            audioSource.Play();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "enemyProjectile")
        {
            audioSource.Play();
            Destroy(gameObject);
        }
       

    }
}
