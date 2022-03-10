using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    public float push_force = 0;
    public Vector3 pushDir = new Vector3(0,1,0).normalized;
    private Vector3 prevPosition;

    private Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        prevPosition = rb.transform.position;
        rb.AddForce(pushDir * push_force);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(prevPosition, rb.transform.position, Color.white, 1200f);
        prevPosition = rb.transform.position;
    }
}
