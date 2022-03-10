using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityAttractor : MonoBehaviour
{
    public float massOfObject = 1;

    void Awake(){
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

    public void Attract(Transform body, float gravition_force) {
        Vector3 targetDir = (body.position -transform.position).normalized;
        Vector3 bodyUp = body.up;

        //body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
        body.GetComponent<Rigidbody>().AddForce(targetDir * gravition_force);
        }
}