using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpacecraftController : MonoBehaviour
{
    Transform m_MainCamera;
    public Joystick joystick1;
    public Joystick joystick2;
    bool grounded = false;
    Quaternion targetRot;
    Quaternion smoothedRot;
    Vector3 prevPosition;
    Vector3 thrusterInput;
    Vector3 cameraRotation;
    Vector3 newPosition;
    Rigidbody rb;
    public ParticleSystem thrusterMain1, thrusterMain2;
    public ParticleSystem thrusterBoxDown1, thrusterBoxDown2, thrusterBoxDown3, thrusterBoxDown4;
    public ParticleSystem thrusterBoxUp1, thrusterBoxUp2, thrusterBoxUp3, thrusterBoxUp4;
    float time = 0.0f;
    public float Speed = 0f;
    Vector3 PreviousFramePosition = Vector3.zero;
    float yawCamera;
    bool OrbitDone = false;
    public Text SpeedText;
    public Text DistanceText;
    public Text NextTaskText;
    string outputText = "land on the planet..";

    [Header ("Handling")]
    public float thrustStrength = 20f;
    public float rotSpeed = 10f;
    // public float rotSmoothSpeed = 10;
    public Object objectForOrbit;
    public Transform distancePlanet;
     
    void Start()
    {
        InitRigidbody();
        targetRot = transform.rotation;
        smoothedRot = transform.rotation;
        prevPosition = rb.transform.position;
        m_MainCamera = Camera.main.transform;
        Vector3 PreviousFramePosition = this.GetComponent<Rigidbody>().transform.position;
        InvokeRepeating("CheckForOrbit", 15.0f, 45.0f);
    }

    
    void Update()
    {
       Debug.DrawLine(prevPosition, rb.transform.position, Color.red, 1200f);
       prevPosition = rb.transform.position;
       float dist = Vector3.Distance(distancePlanet.position, transform.position);
       if (OrbitDone == true) clearOrbit();
       SpeedText.text = "Speed: " + Mathf.Round(Speed * 10.0f) * 0.1f;
       DistanceText.text = "Distance to Planet: " + (Mathf.Round((dist * 10.0f) * 0.1f) - 25f);
       NextTaskText.text = "Next task: " + outputText;
    }

    void FixedUpdate() 
    {
        time += Time.deltaTime;
        HandleMovement();
        Vector3 thrustDir = transform.TransformVector (thrusterInput);
        rb.AddForce(thrustDir * thrustStrength, ForceMode.Acceleration);
        m_MainCamera.transform.eulerAngles = new Vector3(0.0f, yawCamera, 0.0f);

        float movementPerFrame = Vector3.Distance(PreviousFramePosition, transform.position) ;
        Speed = movementPerFrame / Time.fixedDeltaTime;
        print("Speed: " + Speed);
        PreviousFramePosition = transform.position;

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hitGround;
        // Does the ray intersect any objects excluding the player layer
            
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitGround, 1, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hitGround.distance, Color.yellow);
            Debug.Log("Did hit ground");
            OrbitDone = false;
            grounded = true;
            outputText = "Try to enter the orbit carefully";
            //Debug.Break();
        }      else    {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1, Color.cyan);
            Debug.Log("Did not Hit Ground");
            grounded = false;
            //Debug.Break();
        }

        
    }

        void CheckForOrbit(){
            if (grounded == false && Speed >= 6.0f && OrbitDone == false) {
                newPosition = transform.position;
                Invoke("CreateObjectInOrbit", 5); 
            }
    }

        void CreateObjectInOrbit() {
            Instantiate(objectForOrbit, newPosition, Quaternion.identity);
        }


        void clearOrbit() {
            GameObject enemies = GameObject.Find("Cube(Clone)"); 
            GameObject.Destroy(enemies); 
        }
        
       void HandleMovement () {

        // Thruster input
        float thrustInputX = joystick1.Horizontal;
        float thrustInputY = joystick1.Vertical*-1;
        float thrustInputZ = joystick2.Vertical*-1;
        print("thrustInputX: " + thrustInputX);
        print("thrustInputY: " + thrustInputY);
        thrusterInput = new Vector3 (thrustInputX, thrustInputY, thrustInputZ);

        if (thrustInputZ > 0) {
            thrusterMain1.startLifetime = 0.6f;
            thrusterMain2.startLifetime = 0.6f;
        }  else {
            thrusterMain1.startLifetime = 0.06f;
            thrusterMain2.startLifetime = 0.06f;
        }

        if (thrustInputY > 0) {
            thrusterBoxDown1.startLifetime = 0.2f;
            thrusterBoxDown2.startLifetime = 0.2f;
            thrusterBoxDown3.startLifetime = 0.2f;
            thrusterBoxDown4.startLifetime = 0.2f;
        }  else {
            thrusterBoxDown1.startLifetime = 0.06f;
            thrusterBoxDown2.startLifetime = 0.06f;
            thrusterBoxDown3.startLifetime = 0.06f;
            thrusterBoxDown4.startLifetime = 0.06f;
        }

            if (thrustInputY < 0) {
            thrusterBoxUp1.startLifetime = 0.2f;
            thrusterBoxUp2.startLifetime = 0.2f;
            thrusterBoxUp3.startLifetime = 0.2f;
            thrusterBoxUp4.startLifetime = 0.2f;
        }  else {
            thrusterBoxUp1.startLifetime = 0.06f;
            thrusterBoxUp2.startLifetime = 0.06f;
            thrusterBoxUp3.startLifetime = 0.06f;
            thrusterBoxUp4.startLifetime = 0.06f;
        }
        // Rotation input
        float rotaInputX = joystick2.Horizontal;
        //var yaw = Quaternion.AngleAxis (rotaInputX, transform.up);
        //targetRot = yaw *  targetRot;
        //smoothedRot = Quaternion.Slerp (transform.rotation, targetRot, Time.deltaTime * rotSmoothSpeed);

        yawCamera +=  rotaInputX * rotSpeed;
    }    

        void InitRigidbody () {
        rb = GetComponent<Rigidbody> ();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.centerOfMass = Vector3.zero;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }
    void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.gameObject.CompareTag("ObjectInOrbit") && grounded == false) {
            Score.scoreValue ++;
            OrbitDone = true;
            GameObject.Destroy(other);
            outputText = "Well done! Now try to land again!";
        }
    }
}
