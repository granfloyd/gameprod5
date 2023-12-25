using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SmoothCam : NetworkBehaviour
{
    public GameObject playerGO;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    //public Transform target;
    private Vector3 vel = Vector3.zero;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = playerGO.transform.position + offset;
        //Vector3 targetpos = target.position + offset;
        


        //transform.position = Vector3.SmoothDamp(transform.position, playerGO., ref vel, damping);
    }
}
