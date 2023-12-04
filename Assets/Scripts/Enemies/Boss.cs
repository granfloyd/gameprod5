using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private ScoreManager scoreManagerRef; // The ScoreManager

    private Player playerRef;

    public GameObject bossPrefab;

    private Rigidbody2D rb;

    private int hp = 10;

    public float speed = 0.3f;

    private float ticker = 0;

    public float fasterfaster = 0.5f;

    public bool bHasLOS = false;

    public bool bHasCollided = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRef = GameObject.Find("Player").GetComponent<Player>();
        scoreManagerRef = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Player")
        {
            bHasLOS = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Player")
        {
            bHasLOS = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            bHasCollided = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            bHasCollided = false;
        }
    }
    void FollowPlayer()
    {
        Vector2 pos = Vector2.MoveTowards(transform.position, playerRef.transform.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
    }

    //Attack one
    void AttackOne()
    {
        Vector3[] offsets = new Vector3[]
        {
        new Vector3(0, 1.5f, 10),
        new Vector3(0, -1.5f, 10),
        new Vector3(1.5f, 0, 10),
        new Vector3(-1.5f, 0, 10)
        };

        if (bHasLOS)
        {
            foreach (Vector3 offset in offsets)
            {
                Instantiate(bossPrefab, transform.position + offset, Quaternion.identity);
            }
            
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        ticker += Time.deltaTime;
        if (bHasLOS)
        {
            StartCoroutine("AttackOne");
            //AttackOne();
            speed += fasterfaster * Time.deltaTime;

            //if collided with player pause
            if (bHasCollided)
            {
                ticker = 0;
            }
            if (ticker > 3.0f)//grace period
            {
                //FollowPlayer();
            }

        }
        else
        {
            speed = 0.8f;
        }
        if (speed >= 5.0f)
        {
            speed = 0.8f;
        }
    }
}
