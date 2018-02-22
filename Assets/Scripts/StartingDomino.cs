using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingDomino : MonoBehaviour
{
    public Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FallDownByButtonPush()
    {
        Vector3 Extents = gameObject.GetComponent<Collider>().bounds.extents;
        rb.AddForceAtPosition(100 * transform.forward.normalized, transform.position + 0.8f * Extents);
    }
}