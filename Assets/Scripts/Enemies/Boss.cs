using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
public class Boss : MonoBehaviour
{
    public AudioSource audioSourceMainTheme;
    public AudioSource audioSourceTP;
    public AudioSource audioSourceBossSoundTrack;
    private BasicEnemyLOS belos;
    private Player playerRef;
    private SpriteRenderer spriteRenderer;
    public GameObject bossPrefab;
    public GameObject bossAttack2Prefab;
    public GameObject eProjectilePrefab;
    public GameObject bossProjectilePrefab;
    public GameObject Enemy1Prefab;
    public GameObject drop;
    public Renderer rend;
    public int HP;
    public int maxHP = 10;
    public float speed = 0.3f;
    public float fasterfaster = 0.5f;
    public float eProjectileSpeed = 6.0f;
    private float qs = 0;   //timer/cd for boss main slow attack
    private float ticker = 0;   //timer for picking rand attack
    private float ticker2 = 0;   //timer/cd to shoot player
    private float attack1Ticker = 0;
    private float attack2Ticker = 0;
    private float attack3Ticker = 0;
    private bool bIsAttack1Active = false;
    private bool bIsAttack2Active = false;
    private bool bIsAttack3Active = false;
    private List<GameObject> attackOneGameObjectList = new List<GameObject>();
    private List<GameObject> attackTwoGameObjectList = new List<GameObject>();
    private List<GameObject> attackThreeGameObjectList = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        HP = maxHP;
        rend = GetComponent<Renderer>();
        belos = gameObject.GetComponent<BasicEnemyLOS>();
        playerRef = GameObject.Find("Player").GetComponent<Player>();
        //scoreManagerRef = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        audioSourceBossSoundTrack = GameObject.Find("BossTheme").GetComponent<AudioSource>();
        audioSourceMainTheme = GameObject.Find("MainTheme").GetComponent<AudioSource>();
    }

    void FollowPlayer()
    {
        float randPosX = (UnityEngine.Random.value > 0.5f) ? 1 : -1;
        float randPosY = (UnityEngine.Random.value > 0.5f) ? 1 : -1;
        float distance = Vector3.Distance(playerRef.transform.position, this.transform.position);
        if (belos.isHit)
        {
            Debug.Log("bhit");
            audioSourceTP.Play();
            Vector3 movehere = new Vector3(playerRef.transform.position.x, playerRef.transform.position.y + randPosY, playerRef.transform.position.z);
            this.transform.position = movehere;
            belos.isHit = false;
        }
        else if (distance > 2)
        {
            if (Random.value < 0.5f)
            {
                audioSourceTP.Play();
                Vector3 movehere = new Vector3(playerRef.transform.position.x + randPosX, playerRef.transform.position.y, playerRef.transform.position.z);
                this.transform.position = movehere;
            }
        }
    }



    //attack 1 method that shoots projectile at player 
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

    //shoots projectile that slows player
    void ShootPlayer2()
    {
        //clac vector from roboguy - player
        Vector2 direction = (Vector2)(playerRef.transform.position - transform.position);
        direction.Normalize();
        GameObject bossprojectile = Instantiate(bossProjectilePrefab, transform.position, Quaternion.identity);
        bossprojectile.GetComponent<Rigidbody2D>().velocity = direction * eProjectileSpeed;
        // Destroy the gameobject 2 seconds after creation
        Destroy(bossprojectile, 2.0f);
    }
    //Attack one
    void AttackOne()
    {
        bIsAttack1Active = true;
        Vector3[] offsets = new Vector3[]
        {
        new Vector3(0, 1.5f, 0),
        new Vector3(0, -1.5f, 0),
        new Vector3(1.5f, 0, 0),
        new Vector3(-1.5f, 0, 0)
        };
        if (belos.bHasLOS)
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

        Vector3 offset = new Vector3(randPosX, randPosY, 0);
        if (belos.bHasLOS)
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

    void AttackThree()
    {
        bIsAttack3Active = true;
        int spawnCount = Random.Range(1,10);
        if (belos.bHasLOS)
        {
            for(int i = 0; i < spawnCount;i++)
            {
                float randPosY = Random.Range(-0.5f, 0.5f);
                float randPosX = Random.Range(-0.5f, 0.5f);
                Vector3 randPos = new Vector3(randPosX, randPosY, 0);
                GameObject attack3GO = Instantiate(Enemy1Prefab, transform.position + randPos, Quaternion.identity);
                attackThreeGameObjectList.Add(attack3GO);
            }
        }
    }
    private void RandAttack()
    {
        //range 1-3
        int randAttack = Random.Range(1, 4);
        Debug.Log(randAttack);
        switch(randAttack)
        {
            case 1:
                AttackOne();
                break;
            case 2:
                AttackTwo();
               break;
            case 3:
                AttackThree();
                break;

        }

    }
    void SoundControl()
    {
        if (audioSourceMainTheme.isPlaying && belos.bHasLOS)
        {
            audioSourceMainTheme.Pause();
        }
        if (belos.bHasLOS)
        {
            if (!audioSourceBossSoundTrack.isPlaying)
            {
                audioSourceBossSoundTrack.Play();
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        belos.EnemyTakeDamage(ref HP, playerRef.playerDamage);
        SoundControl();
        
        if (HP < 5)
        {
            rend.material.color = new Color(0.5f, 0, 0);
        }
        if (HP <= 0)
        {
            foreach (GameObject attack1GO in attackOneGameObjectList)
            {
                Destroy(attack1GO);
            }
            foreach (GameObject attack2GO in attackTwoGameObjectList)
            {
                Destroy(attack2GO);
            }
            foreach (GameObject attack3GO in attackThreeGameObjectList)
            {
                Destroy(attack3GO);
            }
            audioSourceMainTheme.Play();
            //scoreManagerRef.UpdateScore();
            attackOneGameObjectList.Clear();
            attackTwoGameObjectList.Clear();
            attackThreeGameObjectList.Clear();
            belos.OnDeath(drop, spriteRenderer);
        }
            

        if (belos.bHasLOS)
        {
            if (playerRef.thing <=1)
            {
                FollowPlayer();                
            }
            
            //pick attack cd stuff
            ticker += Time.deltaTime;
            qs += Time.deltaTime;

            if (ticker >= 5.0f)
            {
                RandAttack();
                ticker = 0;
            }

            if (qs > 4.5f)
            {
                ShootPlayer2();
                qs = 0;
            }
        }

        //attack 1 stuff
        if (bIsAttack1Active)
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
                    float speed = 2f;
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
        //attack 3 stuff
        if (bIsAttack3Active)
        {
            //timer begin for attack1
            attack3Ticker += Time.deltaTime;

            //clean up after attack3 is over
            if (attack3Ticker >= 6)
            {
                bIsAttack3Active = false;
                attack3Ticker = 0;
                foreach (GameObject attack3GO in attackThreeGameObjectList)
                {
                    Destroy(attack3GO);
                }
                attackThreeGameObjectList.Clear();

            }
        }
        
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Player")
        {
            if(!audioSourceMainTheme.isPlaying)
            audioSourceMainTheme.Play();
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerProjectile")
        {
            if(belos.bHasLOS)
            {
                belos.isHit = true;
                FollowPlayer();
                
            }     
        }
    }
}
