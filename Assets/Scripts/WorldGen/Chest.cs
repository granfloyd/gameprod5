using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] drops;
    private Player playerRef;
    private Rigidbody2D rb;
    private bool haskey = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRef = GameObject.Find("Player").GetComponent<Player>();
        if(playerRef.keys.Count > 0 )
        {
            haskey = true;
        }

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Player")
        {
            if(haskey)
            {
                if (Input.GetKey(KeyCode.E))
                    //If the GameObject's name matches the one you suggest, output this message in the console
                    Debug.Log("player is within range to open chest");
            }
            

        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.tag == "Player")
        {

            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("player walked away from chest ");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
