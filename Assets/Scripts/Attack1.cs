using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    public Transform spawnPoint;
    public SpriteRenderer attackPrefab;

    public Sprite attackSprite;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAttack();
    }
    public void SpawnAttack()
    {
        // Spawn a copy of the attack prefab at your desired location/orientation.
        SpriteRenderer instance = Instantiate(attackPrefab,
            spawnPoint.transform.position,
            Quaternion.identity);
        instance.sprite = attackSprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
