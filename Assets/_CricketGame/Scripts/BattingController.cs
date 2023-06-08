using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattingController : MonoBehaviour
{
    public float timingWindow = 0.2f; // Timing window in seconds
    public float accuracyThreshold = 0.1f; // Accuracy threshold for a successful hit

    private bool ballInContact = false;
    private bool ballHit = false;

    // Called when the ball enters the bat's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !ballHit)
        {
            ballInContact = true;
        }
    }

    // Called when the ball exits the bat's trigger collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballInContact = false;
        }
    }

    // Called when the player attempts to swing the bat
    public void SwingBat()
    {
        if (ballInContact && !ballHit)
        {
            /*float timing = Mathf.Abs(Time.time - GameController.Instance.BallReleaseTime);
            if (timing <= timingWindow)
            {
                // Timing is within the window, calculate accuracy
                float accuracy = CalculateAccuracy();
                if (accuracy <= accuracyThreshold)
                {
                    // Successful hit!
                    ballHit = true;
                    // Perform hit animation, scoring, and other effects
                    GameController.Instance.OnSuccessfulHit();
                }
            }*/
        }
    }

    // Calculate the accuracy of the shot based on the point of contact
    /*private float CalculateAccuracy()
    {
        // Assuming you have a reference to the ball and bat objects
        Vector3 contactPoint = ball.transform.position; // Get the point of contact
        Vector3 batCenter = transform.position; // Get the center of the bat

        float distance = Vector3.Distance(contactPoint, batCenter);
        float accuracy = distance / batWidth; // Adjust batWidth to your actual bat width

        return accuracy;
    }*/
}
