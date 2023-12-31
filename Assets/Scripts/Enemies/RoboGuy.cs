using UnityEngine;

public class RoboGuy : MonoBehaviour
{
    public Player playerRef;
    private BasicEnemyLOS belos;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public GameObject eProjectilePrefab;   

    //enemy stats
    public float eProjectileSpeed = 4;    
    public float speed = 0.3f;

    //cds
    private float ticker = 0;
    private float delay = 0.0f;
    private int counter = 0;
    private bool reset = false;

    public GameObject drop;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        belos = gameObject.GetComponent<BasicEnemyLOS>();
        rb = GetComponent<Rigidbody2D>();
    }   

    // Update is called once per frame
    void FixedUpdate()
    {
        belos.OnDeath(drop, spriteRenderer);
        belos.EnemyTakeDamage(playerRef.playerDamage);
        ticker += Time.deltaTime;
        if (belos.bHasLOS)
        {
            belos.FollowPlayer(rb,speed);
            if (ticker > 3.0f)
            {
                delay += Time.deltaTime;
                if (delay > 0.5f)
                {
                    belos.ShootPlayer(eProjectilePrefab, eProjectileSpeed);
                    counter += 1;
                    delay = 0;
                }
                if(counter >= 3)
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