using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Player playerRef;
    private BasicEnemyLOS belos;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float speed = 0.5f;
    public GameObject drop;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        belos = gameObject.GetComponent<BasicEnemyLOS>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        belos.OnDeath(drop, spriteRenderer);
        belos.EnemyTakeDamage( playerRef.playerDamage);
        if (belos.bHasLOS)
        {
            belos.FollowPlayer(rb, speed);
        }
        
    }
}
