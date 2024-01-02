using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int HP = 4;
    public bool isDead = false;
    public GameObject[] dropList;
    public AudioSource audioSourceHit;
    public Player playerRef;
    public Renderer rend;
    public int MaxHP;
    void Start()
    {
        rend = GetComponent<Renderer>();
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioSourceHit = GameObject.Find("ehitSFX").GetComponent<AudioSource>();
        MaxHP = HP;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerProjectile")
        {
            audioSourceHit.Play();
            HP -= playerRef.playerDamage;
            Destroy(collision.gameObject);
        }

    }
    public void OnDeath()
    {
        int randIndex = Random.Range(0, dropList.Length);
        GameObject randDrop = dropList[randIndex];
        Instantiate(randDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if (HP < MaxHP)//for when half hp 
        {
            Debug.Log("Hallo");
            rend.material.color = new Color(0.5f, 0, 0);
        }
        if (HP <= 0)
        {
            isDead = true;
        }
        if(isDead)
        {
            OnDeath();
        }
    }
}
