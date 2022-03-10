using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider))]
public class GravityBody : MonoBehaviour
{
public float massOfObject = 1;
GravityAttractor planet;
Universe newUniverse;
private float distanceToPlanet;
private float expandedRadius;
private float gravition_force = 0;

void Awake(){
    planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
    newUniverse = GameObject.FindGameObjectWithTag("Universe").GetComponent<Universe>();
    this.GetComponent<Rigidbody>().useGravity = false;
    //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    expandedRadius = planet.GetComponent<SphereCollider>().radius + planet.GetComponent<SphereCollider>().radius;
    }

void FixedUpdate() {
    distanceToPlanet = Vector3.Distance(this.transform.position, planet.transform.position);
    gravitionforce_calc();
    planet.Attract(transform, gravition_force);

    //print("Planet ist " + distanceToPlanet + " m entfernt.");
    //print("Planet hat ein Radius von " + expandedRadius + ".");
    //print("Es wirkt eine Kraft von " + gravition_force + " Newton.");
    }

void gravitionforce_calc() {
    distanceToPlanet = Vector3.Distance (this.transform.position, planet.transform.position);
    gravition_force = newUniverse.gravity_const * ((this.massOfObject*planet.massOfObject) / (distanceToPlanet*distanceToPlanet));
    }

}