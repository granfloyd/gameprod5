using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SmoothCam : NetworkBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    private Transform target;
    private Vector3 vel = Vector3.zero;

    void Start()
    {
        GameObject playerGO = GameObject.FindWithTag("Player");
        if (playerGO != null)
        {
            target = playerGO.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetpos = target.position + offset;
            targetpos.z = transform.position.z;
            Debug.Log(targetpos);
            transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref vel, damping);
        }
    }
}