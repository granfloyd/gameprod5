using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damnping;

    public Transform target;

    private Vector3 vel = Vector3.zero;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetpos = target.position + offset;
        targetpos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref vel, damnping);
    }
}
