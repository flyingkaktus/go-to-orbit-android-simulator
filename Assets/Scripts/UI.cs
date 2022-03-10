using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
   public Button changeViewButton;
   Rigidbody player;
   public float push_force = 0;
   public Vector3 pushDir;
   float nowspeed;

   public ParticleSystem thrustDown1, thrustDown2, thrustDown3, thrustDown4;

   SpacecraftController newController;

    void Start(){
        //changeViewButton = GameObject.Find("ChangeViewButton").GetComponent<Button>();
      player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

    }

    void FixedUpdate() {
       pushDir = player.transform.up;
       newController =  GameObject.FindGameObjectWithTag("Player").GetComponent<SpacecraftController>();
    }

   public void OnButtonPress(){
      if (newController.Speed <= 2f) {

      thrustDown1.startLifetime = 0.3f;
      thrustDown2.startLifetime = 0.3f;
      thrustDown3.startLifetime = 0.3f;
      thrustDown4.startLifetime = 0.3f;
      player.AddForce(push_force*pushDir);
      print("Lift off!");
      } else {
            thrustDown1.startLifetime = 0.06f;
            thrustDown2.startLifetime = 0.06f;
            thrustDown3.startLifetime = 0.06f;
            thrustDown4.startLifetime = 0.06f;
      }
   }
}
