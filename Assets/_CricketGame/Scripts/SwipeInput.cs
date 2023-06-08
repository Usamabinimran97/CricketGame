using System;
using System.Collections;
using UnityEngine;

namespace _CricketGame.Scripts
{
    public enum BattingMode
    {
        Defensive,
        Attacking,
        Improvised
    }

    public class SwipeInput : MonoBehaviour
    {
        public float hitForce = 100f;
        public float minSwipeDistance = 50f;  // Minimum distance for a swipe gesture
        public float maxSwipeTime = 1f;  // Maximum time for a swipe gesture

        public Animator playerAnimator;
        public AnimationClip animationClip;

        private Vector2 _swipeStartPosition, _swipeDirection;
        private float _swipeStartTime;

        private readonly BattingMode _currentBattingMode = BattingMode.Defensive;
        private static readonly int Shot = Animator.StringToHash("CoverDrive");
        private float _animationTime;
        public Rigidbody ballRigidbody;

        public static SwipeInput Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Update()
        {
            // Check for swipe gesture
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);

            // Check the phase of the touch input
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _swipeStartPosition = touch.position;
                    _swipeStartTime = Time.time;
                    break;

                case TouchPhase.Ended:
                    var swipeEndTime = Time.time;
                    var swipeEndPosition = touch.position;
                    var swipeDuration = swipeEndTime - _swipeStartTime;
                    var swipeDistance = Vector2.Distance(_swipeStartPosition, swipeEndPosition);

                    // Check if the swipe meets the minimum requirements
                    if (swipeDuration < maxSwipeTime && swipeDistance > minSwipeDistance)
                    {
                        _swipeDirection = swipeEndPosition - _swipeStartPosition;
                        _swipeDirection.Normalize();

                        // Determine the direction of the swipe (horizontal or vertical)
                        var swipeAngle = Vector2.Dot(_swipeDirection, Vector2.right);

                        switch (swipeAngle)
                        {
                            case > 0.5f:
                                // Swipe to the right (play appropriate shot)
                                PlayRightShot();
                                break;
                            case < -0.5f:
                                // Swipe to the left (play appropriate shot)
                                PlayLeftShot();
                                break;
                            default:
                                // Swipe vertically (play appropriate shot)
                                PlayVerticalShot();
                                break;
                        }
                    }
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void BallCollide()
        {
            // Calculate the direction based on the swipe direction
            var direction = new Vector3(_swipeDirection.x, 0f, _swipeDirection.y);
            direction.Normalize();

            // Apply the hit force to the ball
            ballRigidbody.AddForce(direction * hitForce, ForceMode.Impulse);
        }
    
        private void PlayRightShot()
        {
            // Check the current batting mode or state of the player (e.g., defensive, attacking, etc.)
            // Implement your own logic here based on the current state to determine the appropriate shot to play

            switch (_currentBattingMode)
            {
                // Example logic:
                case BattingMode.Defensive:
                    BallCollide();
                    // Play a defensive shot to the right
                    playerAnimator.SetBool(Shot, true);
                    _animationTime = animationClip.length;
                    StartCoroutine(WaitToOffAnimation(_animationTime));
                    Debug.Log("Defensive Right Shot Played");
                    break;
                case BattingMode.Attacking:
                {
                    BallCollide();
                    // Play an attacking shot to the right
                    playerAnimator.SetBool(Shot, true);
                    _animationTime = animationClip.length;
                    StartCoroutine(WaitToOffAnimation(_animationTime));
                    Debug.Log("Attacking Right Shot Played");
                    break;
                }
                case BattingMode.Improvised:
                {
                    BallCollide();
                    // Play an improvised shot to the right
                    playerAnimator.SetBool(Shot, true);
                    _animationTime = animationClip.length;
                    StartCoroutine(WaitToOffAnimation(_animationTime));
                    Debug.Log("Improvised Right Shot Played");
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // Add more conditions or variations as needed
        }

        private void PlayLeftShot()
        {
            switch (_currentBattingMode)
            {
                case BattingMode.Defensive:
                    BallCollide();
                    // Play a defensive shot to the left
                    playerAnimator.SetBool(Shot, true);
                    _animationTime = animationClip.length;
                    StartCoroutine(WaitToOffAnimation(_animationTime));
                    Debug.Log("Defensive Left Shot Played");
                    break;
                case BattingMode.Attacking:
                    BallCollide();
                    // Play an attacking shot to the left
                    playerAnimator.SetBool(Shot, true);
                    _animationTime = animationClip.length;
                    StartCoroutine(WaitToOffAnimation(_animationTime));
                    Debug.Log("Attacking Left Shot Played");
                    break;
                case BattingMode.Improvised:
                    BallCollide();
                    // Play an improvised shot to the left
                    playerAnimator.SetBool(Shot, true);
                    _animationTime = animationClip.length;
                    StartCoroutine(WaitToOffAnimation(_animationTime));
                    Debug.Log("Improvised Left Shot Played");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PlayVerticalShot()
        {
            switch (_currentBattingMode)
            {
                case BattingMode.Defensive:
                    BallCollide();
                    // Play a defensive shot to the Vertical
                    playerAnimator.SetBool(Shot, true);
                    _animationTime = animationClip.length;
                    StartCoroutine(WaitToOffAnimation(_animationTime));
                    Debug.Log("Defensive Vertical Shot Played");
                    break;
                case BattingMode.Attacking:
                    BallCollide();
                    // Play an attacking shot to the Vertical
                    playerAnimator.SetBool(Shot, true);
                    _animationTime = animationClip.length;
                    StartCoroutine(WaitToOffAnimation(_animationTime));
                    Debug.Log("Attacking Vertical Shot Played");
                    break;
                case BattingMode.Improvised:
                    BallCollide();
                    // Play an improvised shot to the Vertical
                    playerAnimator.SetBool(Shot, true);
                    _animationTime = animationClip.length;
                    StartCoroutine(WaitToOffAnimation(_animationTime));
                    Debug.Log("Improvised Vertical Shot Played");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerator WaitToOffAnimation(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            playerAnimator.SetBool(Shot, false);
        }
    }
}