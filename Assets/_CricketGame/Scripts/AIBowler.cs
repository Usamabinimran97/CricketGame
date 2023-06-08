using System;
using Unity.VisualScripting;
using UnityEngine;

public class AIBowler : MonoBehaviour
{
    public Transform target; // Reference to the target (e.g., batsman)

    public float ballSpeed = 20f; // Speed of the ball
    public float ballSpin = 500f; // Spin of the ball

    private bool isBalling = false; // Flag to track if the ball is being bowled

    public static AIBowler Instance;

    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (isBalling)
        {
            // Move the ball towards the target at a constant speed
            Vector3 targetDirection = (target.position - transform.position).normalized;
            transform.position += targetDirection * ballSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBalling)
        {
            // Check for collision with the bat or other objects
            if (collision.gameObject.CompareTag("Bat"))
            {
                // Handle the collision with the bat
                Debug.Log("Ball hit by the bat!");
            }
            else
            {
                // Handle collision with other objects
                Debug.Log("Ball collided with something!");
            }

            // Reset ball state after collision
            isBalling = false;
            ResetBall();
        }
    }

    public void Bowl()
    {
        if (!isBalling)
        {
            transform.AddComponent<Rigidbody>();
            // Calculate the direction towards the target
            Vector3 targetDirection = (target.position - transform.position).normalized;

            // Apply force to the ball to simulate the bowling action
            GetComponent<Rigidbody>().AddForce(targetDirection * ballSpeed, ForceMode.Impulse);

            // Apply spin to the ball
            GetComponent<Rigidbody>().AddTorque(Vector3.up * ballSpin);

            // Set the ball as "balling"
            isBalling = true;
        }
    }

    public void ResetBall()
    {
        isBalling = false;
        // Reset the ball's position and rotation
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;
        // Reset any other ball-related variables or states
    }
}
