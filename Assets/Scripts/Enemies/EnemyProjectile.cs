using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public AudioSource audioSourceHit;
    // Start is called before the first frame update
    void Start()
    {

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
            audioSourceHit.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "playerProjectile")
        {
            audioSourceHit.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "GrimisAtk")
        {
            audioSourceHit.Play();
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
