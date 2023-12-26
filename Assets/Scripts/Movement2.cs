using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking;
public class Movement2 : NetworkBehaviour
{
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        float hinput = Input.GetAxisRaw("Horizontal");
        float vinput = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(hinput, 0, vinput);
        moveDir.Normalize();

        transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
    }
}
