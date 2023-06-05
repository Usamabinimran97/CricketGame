using System.Collections;
using UnityEngine;

public enum BattingMode
{
    Defensive,
    Attacking,
    Improvised
}

public class SwipeInput : MonoBehaviour
{
    public float minSwipeDistance = 50f;  // Minimum distance for a swipe gesture
    public float maxSwipeTime = 1f;  // Maximum time for a swipe gesture

    public Animator playerAnimator;
    public AnimationClip animationClip;

    private Vector2 swipeStartPosition;
    private float swipeStartTime;

    private BattingMode currentBattingMode = BattingMode.Defensive;
    private static readonly int Shot = Animator.StringToHash("Shot");
    private float animationTime;

    private void Update()
    {
        // Check for swipe gesture
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check the phase of the touch input
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    swipeStartPosition = touch.position;
                    swipeStartTime = Time.time;
                    break;

                case TouchPhase.Ended:
                    float swipeEndTime = Time.time;
                    Vector2 swipeEndPosition = touch.position;
                    float swipeDuration = swipeEndTime - swipeStartTime;
                    float swipeDistance = Vector2.Distance(swipeStartPosition, swipeEndPosition);

                    // Check if the swipe meets the minimum requirements
                    if (swipeDuration < maxSwipeTime && swipeDistance > minSwipeDistance)
                    {
                        Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;
                        swipeDirection.Normalize();

                        // Determine the direction of the swipe (horizontal or vertical)
                        float swipeAngle = Vector2.Dot(swipeDirection, Vector2.right);

                        if (swipeAngle > 0.5f)
                        {
                            // Swipe to the right (play appropriate shot)
                            PlayRightShot();
                        }
                        else if (swipeAngle < -0.5f)
                        {
                            // Swipe to the left (play appropriate shot)
                            PlayLeftShot();
                        }
                        else
                        {
                            // Swipe vertically (play appropriate shot)
                            PlayVerticalShot();
                        }
                    }
                    break;
            }
        }
    }

    private void PlayRightShot()
    {
        // Check the current batting mode or state of the player (e.g., defensive, attacking, etc.)
        // Implement your own logic here based on the current state to determine the appropriate shot to play

        // Example logic:
        if (currentBattingMode == BattingMode.Defensive)
        {
            // Play a defensive shot to the right
            playerAnimator.SetBool(Shot, true);
            animationTime = animationClip.length;
            StartCoroutine(WaitToOffAnimation(animationTime));
            Debug.Log("Defensive Right Shot Played");
        }
        else if (currentBattingMode == BattingMode.Attacking)
        {
            // Play an attacking shot to the right
            playerAnimator.SetBool(Shot, true);
            animationTime = animationClip.length;
            StartCoroutine(WaitToOffAnimation(animationTime));
            Debug.Log("Attacking Right Shot Played");
        }
        else if (currentBattingMode == BattingMode.Improvised)
        {
            // Play an improvised shot to the right
            playerAnimator.SetBool(Shot, true);
            animationTime = animationClip.length;
            StartCoroutine(WaitToOffAnimation(animationTime));
            Debug.Log("Improvised Right Shot Played");
        }
        // Add more conditions or variations as needed
    }

    private void PlayLeftShot()
    {
        if (currentBattingMode == BattingMode.Defensive)
        {
            // Play a defensive shot to the left
            playerAnimator.SetBool(Shot, true);
            animationTime = animationClip.length;
            StartCoroutine(WaitToOffAnimation(animationTime));
            Debug.Log("Defensive Left Shot Played");
        }
        else if (currentBattingMode == BattingMode.Attacking)
        {
            // Play an attacking shot to the left
            playerAnimator.SetBool(Shot, true);
            animationTime = animationClip.length;
            StartCoroutine(WaitToOffAnimation(animationTime));
            Debug.Log("Attacking Left Shot Played");
        }
        else if (currentBattingMode == BattingMode.Improvised)
        {
            // Play an improvised shot to the left
            playerAnimator.SetBool(Shot, true);
            animationTime = animationClip.length;
            StartCoroutine(WaitToOffAnimation(animationTime));
            Debug.Log("Improvised Left Shot Played");
        }
    }

    private void PlayVerticalShot()
    {
        if (currentBattingMode == BattingMode.Defensive)
        {
            // Play a defensive shot to the Vertical
            playerAnimator.SetBool(Shot, true);
            animationTime = animationClip.length;
            StartCoroutine(WaitToOffAnimation(animationTime));
            Debug.Log("Defensive Vertical Shot Played");
        }
        else if (currentBattingMode == BattingMode.Attacking)
        {
            // Play an attacking shot to the Vertical
            playerAnimator.SetBool(Shot, true);
            animationTime = animationClip.length;
            StartCoroutine(WaitToOffAnimation(animationTime));
            Debug.Log("Attacking Vertical Shot Played");
        }
        else if (currentBattingMode == BattingMode.Improvised)
        {
            // Play an improvised shot to the Vertical
            playerAnimator.SetBool(Shot, true);
            animationTime = animationClip.length;
            StartCoroutine(WaitToOffAnimation(animationTime));
            Debug.Log("Improvised Vertical Shot Played");
        }
    }

    private IEnumerator WaitToOffAnimation(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        playerAnimator.SetBool(Shot, false);
    }
}
