using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // <- make sure to import this namespace
using TMPro;

namespace Assets.SuperGoalie.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Ball : MonoBehaviour
    {
        [Tooltip("The gravity acting on the ball")]
        public float gravity = 9f;

        [Tooltip("The target object that the ball will aim for")]
        public Transform targetObject;

        [Tooltip("The maximum power that can be applied to the ball")]
        public float maxPower = 50f;

        [Tooltip("The text object that will display the ball's speed")]
        public TextMeshProUGUI speedText;  // <- declare the Text field

        [Tooltip("The sound that will play when the ball is hit")]
        public AudioClip hitSound;  // <- declare the AudioClip field

        [Tooltip("A Gauge that will display the power of the shot")]
        public Slider powerGauge;  // <- declare the Slider field

        public delegate void BallLaunched(float flightTime, float velocity, Vector3 initial, Vector3 target);
        public event BallLaunched OnBallLaunched;

        public Rigidbody Rigidbody { get; private set; }
        public SphereCollider SphereCollider { get; private set; }
        private AudioSource audioSource;  // <- declare the AudioSource

        private float shootHoldTime = 0f;
        private bool isShooting = false;

        private void Awake()
        {
            //get the components
            Rigidbody = GetComponent<Rigidbody>();
            SphereCollider = GetComponent<SphereCollider>();
            audioSource = GetComponent<AudioSource>();  // <- get the AudioSource component

            // set the gravity of the ball
            Physics.gravity = new Vector3(0f, -gravity, -0f);
        }

        private void OnCollisionEnter(Collision collision)  // <- OnCollisionEnter method
        {
            if (audioSource != null && hitSound != null)  // make sure the AudioSource and AudioClip are assigned
            {
                audioSource.PlayOneShot(hitSound);
            }
        }

        private void Update()
        {
            if (isShooting)
            {
                // Increase the shoot hold time
                shootHoldTime += Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space)) // or Input.GetButtonDown("Fire1") depending on your input settings
            {
                isShooting = true;
    
            }

            if (Input.GetKeyUp(KeyCode.Space)) // or Input.GetButtonUp("Fire1") depending on your input settings
            {
                isShooting = false;

                // Calculate the power to apply to the ball
                float power = Mathf.Clamp01(shootHoldTime) * maxPower;

                // Launch the ball
                Launch(power, targetObject.position);

                // Reset the shoot hold time
                shootHoldTime = 0f;

                audioSource.PlayOneShot(hitSound);
            }

           if (speedText != null) // Make sure the speed text is assigned
            {
                speedText.text = "Speed: " + Rigidbody.velocity.magnitude.ToString("F2");
            }
            if (powerGauge != null) // Update the gauge slider value
            {
                powerGauge.value = shootHoldTime * maxPower;
            }

        }

        public void Stop()
        {
            Rigidbody.angularVelocity = Vector3.zero;
            Rigidbody.velocity = Vector3.zero;
        }

        
    public Vector3 FuturePosition(float time)
    {
        //get the velocities
        Vector3 velocity = Rigidbody.velocity;
        Vector3 velocityXZ = velocity;
        velocityXZ.y = 0f;

        //find the future position on the different axis
        float futurePositionY = transform.position.y + (velocity.y * time + 0.5f * -gravity * Mathf.Pow(time, 2));
        Vector3 futurePositionXZ = Vector3.zero;

        //get the ball future position
        futurePositionXZ = transform.position + velocityXZ.normalized * velocityXZ.magnitude * time;

        //bundle the future positions to together
        Vector3 futurePosition = futurePositionXZ;
        futurePosition.y = futurePositionY;

        //return the future position
        return futurePosition;
    }


        public void Launch(float power, Vector3 target)
        {
            //set the initial position
            Vector3 initial = Position;

            //find the direction vectors
            Vector3 toTarget = target - initial;
            Vector3 toTargetXZ = toTarget;
            toTargetXZ.y = 0;

            //find the time to target
            float time = toTargetXZ.magnitude / power;

            // calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
            // where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
            // so xz = v0xz * t => v0xz = xz / t
            // and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
            toTargetXZ = toTargetXZ.normalized * toTargetXZ.magnitude / time;

            //set the y-velocity
            Vector3 velocity = toTargetXZ;
            velocity.y = toTarget.y / time + (0.5f * gravity * time);

            //set the rigidbody velocity
            Rigidbody.velocity = velocity;

            //set the launch direction
            LaunchDirection = Rigidbody.velocity.normalized;

            //invoke the event
            OnBallLaunched?.Invoke(time, power, initial, target);
        }

        // we recommend adding this property for convenience
        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        public Vector3 LaunchDirection { get; private set; }
        
    }
}