using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Boss : MonoBehaviour
{
    private ScoreManager scoreManagerRef; // The ScoreManager

    private Player playerRef;

    private Rigidbody2D rb;

    public GameObject bossPrefab;

    public GameObject bossAttack2Prefab;

    public GameObject eProjectilePrefab;

    private int hp = 10;

    public float speed = 0.3f;

    public float fasterfaster = 0.5f;

    public float eProjectileSpeed = 6.0f;

    private float ticker = 0;   //timer for picking rand attack

    private float ticker2 = 0;   //timer/cd to shoot player

    private float attack1Ticker = 0;

    private float attack2Ticker = 0;

    private float delay = 0.0f;     //for attack1 shoot delay for each bullet

    private int counter = 0;        //for attack1 shoot counter for each bullet

    private bool reset = false;     //for attack1 shoot timer 

    public bool bHasLOS = false;

    public bool bHasCollided = false;

    private bool bIsAttack1Active = false;

    private bool bIsAttack2Active = false;

    private List<GameObject> attackOneGameObjectList = new List<GameObject>();

    private List<GameObject> attackTwoGameObjectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRef = GameObject.Find("Player").GetComponent<Player>();
        scoreManagerRef = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }
    
    void FollowPlayer()
    {
        Vector2 pos = Vector2.MoveTowards(transform.position, playerRef.transform.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
    }

    void ShootPlayer(GameObject go)
    {
        // Calculate vector from bossInstance to player
        Vector2 direction = (Vector2)(playerRef.transform.position - go.transform.position);
        direction.Normalize();

        GameObject eprojectile = Instantiate(eProjectilePrefab, go.transform.position, Quaternion.identity);

        eprojectile.GetComponent<Rigidbody2D>().velocity = direction * eProjectileSpeed;

        // Destroy the gameobject 2 seconds after creation
        Destroy(eprojectile, 2.0f);
    }

    //Attack one
    void AttackOne()
    {
        bIsAttack1Active = true;
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
                GameObject attack1GO = Instantiate(bossPrefab, transform.position + offset, Quaternion.identity);
                attackOneGameObjectList.Add(attack1GO);
                
            }            
        }

    }
    void AttackTwo()
    {
        bIsAttack2Active = true;
        float randPosY = Random.Range(-0.5f, 0.5f);
        float randPosX = (UnityEngine.Random.value > 0.5f) ? 3 : -3;

        Vector3 offset = new Vector3(randPosX, randPosY, 10);
        if (bHasLOS)
        {
            GameObject attack2GO = Instantiate(bossAttack2Prefab, playerRef.transform.position + offset, Quaternion.identity);

            if(attack2GO.transform.position.x < -1)
            {
                attack2GO.transform.Rotate(0, 0, -90);
            }            
            if (attack2GO.transform.position.x > 1)
            {
                attack2GO.transform.Rotate(0, 0, 90);
            }
            attackTwoGameObjectList.Add(attack2GO);

        }
    }
    private void RandAttack()
    {
        //range 1-2
        int randAttack = Random.Range(2, 2);
        Debug.Log(randAttack);
        switch(randAttack)
        {
            case 1:
                AttackOne();
                break;
            case 2:
                AttackTwo();
               break;

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bHasLOS)
           FollowPlayer();
        //pick attack cd stuff
        ticker += Time.deltaTime;
        if(ticker >= 6.0f)
        {
            RandAttack();
            ticker = 0;
        }

        //attack 1 stuff
        if(bIsAttack1Active)
        {
            //timer begin for attack1
            attack1Ticker += Time.deltaTime;

            //shoot time for attack1 begins 
            ticker2 += Time.deltaTime;

            //clean up after attack1 is over
            if (attack1Ticker >= 5)
            {
                bIsAttack1Active = false;
                attack1Ticker = 0;
                foreach (GameObject attack1GO in attackOneGameObjectList)
                {
                    Destroy(attack1GO);
                }
                attackOneGameObjectList.Clear();

            }
            else
            {
                foreach (GameObject attack1GO in attackOneGameObjectList)
                {
                    float randShootTimer = Random.Range(1, 4);

                    if (ticker2 > randShootTimer)
                    {
                        ShootPlayer(attack1GO);
                        ticker2 = 0;
                    }
                }
                
            }            
        }

        //attack 2 stuff
        if (bIsAttack2Active)
        {
            //timer begin for attack1
            attack2Ticker += Time.deltaTime;

            //clean up after attack2 is over
            if (attack2Ticker >= 5)
            {
                bIsAttack2Active = false;
                attack2Ticker = 0;
                foreach (GameObject attack2GO in attackTwoGameObjectList)
                {
                    Destroy(attack2GO);
                }
                attackTwoGameObjectList.Clear();

            }
            else
            {
                foreach (GameObject attack2GO in attackTwoGameObjectList)
                {
                    float movement = 0;
                    float speed = 1f;
                    if (Mathf.Approximately(attack2GO.transform.eulerAngles.z, 90))
                    {
                        movement = -1.0f;
                    }
                    else if (Mathf.Approximately(attack2GO.transform.eulerAngles.z, 270))
                    {
                        movement = 1.0f;
                    }

                    //get the rb of the attack2 prefab and move it 
                    attack2GO.GetComponent<Rigidbody2D>().
                        MovePosition(attack2GO.GetComponent<Rigidbody2D>().
                        position + new Vector2(movement * speed * Time.deltaTime, 0));

                    Vector3 initialPosition = attack2GO.transform.position;
                    float distanceMoved = Vector3.Distance(initialPosition, attack2GO.transform.position);
                    if (distanceMoved >= 6)
                    {
                        Destroy(attack2GO);
                    }
                }


            }

        }
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

}
