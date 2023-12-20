using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public HealthSystem hsRef;
    public PlayerMovement movementRef;
    public Player playerRef;

    public Animator animator;
    public Animator animator2;
    public GameObject shieldPrefab;
    public GameObject shield2Prefab;
    public GameObject grimisDrinkPrefab;
    public GameObject heartPrefab;
    public GameObject powerup69Prefab;
    public GameObject shield;

    public float crackSpeed = 3.0f;

    public AudioSource audioSource2;//drink
    public AudioSource audioSource3;//shield
    public AudioSource audioSource7;//heartbeat sfx

    void Start()
    {
        animator = GameObject.Find("pp69Ani").GetComponent<Animator>();
        animator2 = GameObject.Find("drinkAni").GetComponent<Animator>();
        hsRef = GetComponent<HealthSystem>();
        movementRef = GetComponent<PlayerMovement>();
        playerRef = GetComponent<Player>();
    }
    public IEnumerator UseDrink(float duration)
    {
        float buff = 0.5f;
        float newCD;
        float startTime = Time.time;
        audioSource2.Play();
        newCD = playerRef.shootCD * buff;
        animator2.SetTrigger("drink");
        while (Time.time - startTime < duration)//time from when called
        {
            playerRef.shootCD = newCD;
            yield return null; // Wait for the next frame
        }
        animator2.SetTrigger("idle");
        playerRef.shootCD = 1f;
    }
    public IEnumerator UseShield(float duration)
    {
        float startTime = Time.time;
        audioSource3.Play();
        shield = Instantiate(shield2Prefab, transform.position, Quaternion.identity);
        while (Time.time - startTime < duration)//time from when called
        {
            shield.transform.position = transform.position;
            yield return null; // Wait for the next frame
        }
        Destroy(shield);
    }
    public IEnumerator UsePowerup69(float duration)
    {
        float startTime = Time.time;
        Debug.Log(movementRef.speed);
        if (movementRef.speed > PlayerMovement.originalSpeed)
        {
            Debug.Log("no dups allowed");
            yield break;
        }
        audioSource7.Play();
        //hsRef.powerup69active.SetActive(true);
        movementRef.speed = crackSpeed;
        animator.SetTrigger("pp69");
        while (Time.time - startTime < duration)//time from when called
        {
            
            movementRef.speed = crackSpeed;
            yield return null; // Wait for the next frame
        }
        audioSource7.Stop();
        animator.SetTrigger("idle");
        //hsRef.powerup69active.SetActive(false);  
        movementRef.speed = PlayerMovement.originalSpeed;
    }
    public void Useheart()
    {
        hsRef.HealDamage(1);
    }
    public delegate void UseThisItem();

    public void Ability(UseThisItem usethisitem)
    {
        usethisitem();
    }

}


