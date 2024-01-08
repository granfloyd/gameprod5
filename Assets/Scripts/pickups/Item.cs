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

    

    public AudioSource audioSourceDrink;//drink
    public AudioSource audioSourceShield;//shield
    public AudioSource audioSourcePowerup69;//heartbeat sfx
    public static bool ispp69Active = false;
    public bool isShieldActive = false;
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
        //float buff = 0.7f;        
        float newCD;
        float startTime = Time.time;
        audioSourceDrink.Play();
        newCD = GeneralUI.newShootCD;
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
        if(!isShieldActive)
        {
            isShieldActive = true;
            float startTime = Time.time;
            audioSourceShield.Play();
            shield = Instantiate(shield2Prefab, transform.position, Quaternion.identity);
            while (Time.time - startTime < duration)//time from when called
            {
                shield.transform.position = transform.position;
                yield return null; // Wait for the next frame
            }
            Destroy(shield);
            isShieldActive = false;
        }
        
    }
    public IEnumerator UsePowerup69(float duration)
    {
        ispp69Active = true;
        float startTime = Time.time;
        Debug.Log(movementRef.speed);
        if (movementRef.speed > PlayerMovement.originalSpeed)
        {
            Debug.Log("no dups allowed");
            yield break;
        }
        audioSourcePowerup69.Play();
        //hsRef.powerup69active.SetActive(true);
        movementRef.speed = GeneralUI.crackSpeed;
        animator.SetTrigger("pp69");
        while (Time.time - startTime < duration)//time from when called
        {
            
            movementRef.speed = GeneralUI.crackSpeed;
            yield return null; // Wait for the next frame
        }
        audioSourcePowerup69.Stop();
        animator.SetTrigger("idle");
        //hsRef.powerup69active.SetActive(false);  
        movementRef.speed = PlayerMovement.originalSpeed;
        ispp69Active = false;
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


