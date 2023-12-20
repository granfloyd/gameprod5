using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class BasicEnemyLOS : MonoBehaviour
{
    private Player playerRef;
    public AudioSource audioSource69;
    private ScoreManager scoreManagerRef; // The ScoreManager
    public GameObject damageTextPrefab;
    public GameObject keyObject;
    public float Speed;
    public float ff;
    public bool bHasLOS = false;
    public bool bHasCollided = false;
    public bool isHit = false;
    public bool isDead = false;

    public float textmovespeed = 0.5f; // Speed of the text moving upwards
    public float fadeTime = 1f; // Time for the text to fade out
    public float destroyTime = 1f; // Time before the text is destroyed
    void Start()
    {
        playerRef = GameObject.Find("Player").GetComponent<Player>();
        scoreManagerRef = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        audioSource69 = GameObject.Find("hitSFX").GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            bHasLOS = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            bHasLOS = false;
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
            audioSource69.Play();
           
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
            Debug.Log("LLLL");
            bHasCollided = false;
        }
    }

    public void EnemyTakeDamage(ref int enemyhp, int howmuch)
    {
        if (isHit)
        {
            enemyhp -= howmuch;
            bool isCrit = false;
            int critHit = howmuch;
            int randNumber = Random.Range(1, 5);
            if (critHit == randNumber)
            {
                isCrit = true;
                howmuch = howmuch += critHit;
            }
            GameObject damageTextObject = Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform);
            TextMeshPro damageText = damageTextObject.GetComponent<TextMeshPro>();
            damageText.text = "-" + howmuch.ToString();
            if(isCrit)
            {
                damageText.fontStyle = FontStyles.Bold;
                damageText.fontSize += 0.3f;
                damageText.color = new Color32(200, 190, 40, 255);
            }
            StartCoroutine(FadeAndDestroy(damageTextObject, damageText));
            isHit = false;
            if (enemyhp <= 0)
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

    public void OnDeath(GameObject drops)
    {
        if(isDead)
        {
            Debug.Log("on death called!");
            scoreManagerRef.UpdateScore();
            if (Random.value < 0.5f)    
            {
                GameObject spawnthis = Instantiate(keyObject, transform.position, Quaternion.identity);
            }
            if(drops != null)
            {
                GameObject spawnthis2 = Instantiate(keyObject, transform.position, Quaternion.identity);
            }
            
            //play deathsound here 
            Destroy(gameObject);
            isDead = false;
        }
    }
    public void ShootPlayer(GameObject enemyProjectile,float speed)
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

    // Update is called once per frame
    void Update()
    {
        OnDeath(null);
    }
}
