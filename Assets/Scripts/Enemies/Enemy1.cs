using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private BasicEnemyLOS belos;
    private Rigidbody2D rb;
    public int HP = 2;
    private float speed = 0.5f;
    public GameObject drop;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        belos = gameObject.GetComponent<BasicEnemyLOS>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        belos.EnemyTakeDamage(ref HP, Player.playerDamage);
        if (belos.bHasLOS)
        {
            belos.FollowPlayer(rb, speed);
        }
        
    }
}
