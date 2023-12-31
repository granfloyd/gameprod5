using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    //private ScoreManager scoreManagerRef; // The ScoreManager
    public AudioSource audioSourceHit;

    public Player playerRef;
    public GameObject playerGO;


    // Start is called before the first frame update
    void Start()
    {
        audioSourceHit = GameObject.Find("ehitSFX").GetComponent<AudioSource>();
        playerRef = GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Grimis")
        {
            audioSourceHit.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy1")
        {
            audioSourceHit.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Blackguy")
        {
            audioSourceHit.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "RoboGuy")
        {
            audioSourceHit.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {   
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "enemyProjectile")
        {
            audioSourceHit.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "bossProjectile")
        {
            audioSourceHit.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "GrimisAtk")
        {
            audioSourceHit.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "boss")
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
