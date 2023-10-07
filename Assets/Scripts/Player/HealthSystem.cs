using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    //position stuff
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    // Start is called before the first frame update
    void Start()
    {

    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }
    // Update is called once per frame
    void Update()
    {
        if (health > numOfHearts)
            health = numOfHearts;

        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Grimis")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Grimis collided with player");
            TakeDamage(1);
        }

    }
}
