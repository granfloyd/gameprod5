using System.Collections;
using TMPro;
using UnityEngine;

public class BasicEnemyLOS : MonoBehaviour
{
    private Player playerRef;
    public GameObject playerGO;
    public AudioSource audioSourceHit;
    public AudioSource audioSourceCritHit;
    public AudioSource audioSourceBossDeath;
    private GeneralUI genUIRef; // The ScoreManager
    public GameObject damageTextPrefab;
    public GameObject keyObject;
    public GameObject genUIGO;
    public float Speed;
    public float ff;
    public bool bHasLOS = false;
    public bool bHasCollided = false;
    public bool isHit = false;
    public bool isDead = false;
    public bool isCrit = false;
    public float textmovespeed = 0.5f; // Speed of the text moving upwards
    public float fadeTime = 1f; // Time for the text to fade out
    public float destroyTime = 1f; // Time before the text is destroyed
    [SerializeField] private bool isBoss;
    public FirstStartManager fs;
    public int HP = 2;
    public int scale;
    void Start()
    {
        
        fs = GameObject.Find("firststartmanager").GetComponent<FirstStartManager>();
        GameObject[] sameNameObjs = fs.FindGameObjectsWithSameName("firststartmanager");
        scale = sameNameObjs.Length;
        Debug.Log(scale);
        if (!FirstStartManager.isFirstStart)
        {
            for (int i = 0; i < scale; i++)
            {
                HP += 1;
            }
        }
        
            playerRef = GameObject.Find("Player").GetComponent<Player>();

        genUIRef = genUIGO.GetComponent<GeneralUI>();
        if(isBoss)
        {
            audioSourceBossDeath = GameObject.Find("BossDeath").GetComponent<AudioSource>();
        }
    
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            bHasLOS = true;
            if(isBoss)
            {
                playerRef.thing += 1;
            }
                
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            bHasLOS = false;
            if(isBoss)
            {
                playerRef.thing -= 1;
            }
                
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerProjectile")
        {
            isHit = true;
            bHasCollided = true;
            bHasCollided = false;
        }
        if (collision.gameObject.tag == "Shield2.0")
        {
            isHit = true;
        }
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
        if (collision.gameObject.tag == "playerProjectile")
        {
            bHasCollided = false;
        }
    }

    public void EnemyTakeDamage(int howmuch)
    {
        if (isHit)
        {
            Debug.Log("Aye");
            int critHit = howmuch;
            int randNumber = Random.Range(1, 5);
            if (critHit == randNumber || Item.ispp69Active)
            {
                isCrit = true;
                howmuch = howmuch * 2;
            }
            HP -= howmuch;
            GameObject damageTextObject = Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform);
            TextMeshPro damageText = damageTextObject.GetComponent<TextMeshPro>();
            damageText.text = "-" + howmuch.ToString();
            if(isCrit)
            {
                audioSourceHit.Stop();//dont play both sounds
                audioSourceCritHit.Play();
                damageText.fontStyle = FontStyles.Bold;
                damageText.fontSize += 0.3f;
                damageText.color = new Color32(200, 190, 40, 255);
            }
            StartCoroutine(FadeAndDestroy(damageTextObject, damageText));
            isCrit = false;
            isHit = false;
            if (HP <= 0)
                isDead = true;
        }
        
    }

    IEnumerator FadeAndDestroy(GameObject damageTextObject, TextMeshPro damageText)
    {
        float fadeTime = 1f;
        float speed = 0.5f;
        // Fade out
        Color originalColor = damageText.color;
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            damageTextObject.transform.position += Vector3.up * speed * Time.deltaTime;
            float normalizedTime = t / fadeTime;
            // Set the alpha
            damageText.color = Color.Lerp(originalColor, Color.clear, normalizedTime);
            yield return null;
        }
        Destroy(damageTextObject);
    }
    IEnumerator FadeAndDestroySprite(GameObject spriteObject, SpriteRenderer spriteRenderer)
    {
        float fadeTime = 0.1f;
        // Fade out
        Color originalColor = spriteRenderer.color;
        if (isBoss)
        {
            audioSourceBossDeath.Play();
        }
            
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;
            // Set the alpha
            spriteRenderer.color = Color.Lerp(originalColor, Color.clear, normalizedTime);
            yield return null;
        }
        Destroy(spriteObject);
    }

    public void OnDeath(GameObject drops,SpriteRenderer spriteRenderer)
    {
        if(isDead)
        {
            genUIRef.UpdateScore();
            if (drops != null)
            {
                Instantiate(keyObject, transform.position, Quaternion.identity);
            }
            else if (Random.value < 0.5f)    
            {
                Instantiate(drops, transform.position, Quaternion.identity);
            } 
            
            StartCoroutine(FadeAndDestroySprite(gameObject, spriteRenderer));
            isDead = false;
        }
    }
    public void ShootPlayer(GameObject enemyProjectile, float speed)
    {
        Vector2 direction = (Vector2)(playerRef.transform.position - transform.position);
        direction.Normalize();
        GameObject eprojectile = Instantiate(enemyProjectile, transform.position, Quaternion.identity);
        eprojectile.GetComponent<Rigidbody2D>().velocity = direction * speed;
        Destroy(eprojectile, 2.0f);
    }

    public void FollowPlayer(Rigidbody2D rb, float speed)
    {
        Vector2 pos = Vector2.MoveTowards(transform.position, playerRef.transform.position, speed * Time.deltaTime);
        rb.MovePosition(pos);
    }

    void Update()
    { 
        if(isHit)//sfx to play when hit
        {
            audioSourceHit.Play();
        }
    }

}
