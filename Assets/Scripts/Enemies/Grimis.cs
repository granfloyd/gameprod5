using UnityEngine;

public class Grimis : MonoBehaviour
{
    public Player playerRef;
    private BasicEnemyLOS belos;
    private SpriteRenderer spriteRenderer;
    public GameObject grimisProjectilePrefab;
    public float grimisProjectileSpeed = 4.0f;
   
    public float speed = 0.3f;    
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
    }
    
    void Update()
    {
        belos.OnDeath(drop, spriteRenderer);
        belos.EnemyTakeDamage( playerRef.playerDamage);
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

    