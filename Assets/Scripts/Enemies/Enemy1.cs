using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Player playerRef;
    private BasicEnemyLOS belos;
    private Rigidbody2D rb;
    public int HP = 2;
    private float speed = 0.5f;
    public GameObject drop;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        belos = gameObject.GetComponent<BasicEnemyLOS>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        belos.OnDeath(drop);
        belos.EnemyTakeDamage(ref HP, playerRef.playerDamage);
        if (belos.bHasLOS)
        {
            belos.FollowPlayer(rb, speed);
        }
        
    }
}
