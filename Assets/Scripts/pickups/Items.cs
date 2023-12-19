using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public bool isActive;
    public float ticker;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            ticker += Time.deltaTime;
            if (ticker > duration)
            {
                ticker = 0;
                isActive = false;
                PerformAction();
            }
        }
    }
    // This method will be overridden in each power-up's class
    protected virtual void PerformAction()
    {

    }
}

public class Shield : Items
{
    protected override void PerformAction()
    {
        Destroy(gameObject);
        if (gameObject != null)
            gameObject.transform.position = transform.position;
    }
}

public class Drink : Items
{
    protected override void PerformAction()
    {
        // Add any actions for drink here
    }
}

public class Powerup69 : Items
{
    protected override void PerformAction()
    {
        //hsRef.powerup69active.SetActive(false);
        movementRef.speed = 1.0f;
        //audioSource7.Stop();
    }
}

