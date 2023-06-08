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
        public float hitForce = 6f;
        public float minSwipeDistance = 50f;  // Minimum distance for a swipe gesture
        public float maxSwipeTime = 1f;  // Maximum time for a swipe gesture

        public Animator playerAnimator;
        public AnimationClip animationClip;

        private Vector2 _swipeStartPosition, _swipeDirection;
        private float _swipeStartTime;

        private BattingMode _currentBattingMode = BattingMode.Defensive;
        private static readonly int CoverDrive = Animator.StringToHash("CoverDrive");
        private static readonly int Defence = Animator.StringToHash("Defence");
        private static readonly int FlickShot = Animator.StringToHash("FlickShot");
        private static readonly int Hook = Animator.StringToHash("Hook");
        private static readonly int LegGlance = Animator.StringToHash("LegGlance");
        private float _animationTime, _animtime;
        private string _animationName;
        private AnimatorStateInfo _stateInfo;

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
                                _currentBattingMode = swipeAngle switch
                                {
                                    > 0.5f and < 0.6f => BattingMode.Defensive,
                                    > 0.6f and < 0.7f => BattingMode.Attacking,
                                    > 0.7f => BattingMode.Improvised,
                                    _ => _currentBattingMode
                                };
                                // Swipe to the right (play appropriate shot)
                                PlayRightShot();
                                break;
                            case < -0.5f:
                                _currentBattingMode = swipeAngle switch
                                {
                                    < -0.5f and > -0.6f => BattingMode.Defensive,
                                    < -0.6f and > -0.7f => BattingMode.Attacking,
                                    < -0.7f => BattingMode.Improvised,
                                    _ => _currentBattingMode
                                };
                                // Swipe to the left (play appropriate shot)
                                PlayLeftShot();
                                break;
                            default:
                                _currentBattingMode = swipeAngle switch
                                {
                                    < 0.5f and > 0.3f => BattingMode.Defensive,
                                    < 0.3f and > -0.3f => BattingMode.Attacking,
                                    < -0.3f => BattingMode.Improvised,
                                    _ => _currentBattingMode
                                };
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

            ballRigidbody.isKinematic = false;
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
                    playerAnimator.SetBool(Defence, true);
                    _stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
                    _animtime = _stateInfo.normalizedTime * _stateInfo.length;
                    _animationTime = animationClip.length;
                    _animationName = "Defence";
                    StartCoroutine(WaitToOffAnimation(_animtime));
                    Debug.Log("Defensive Right Shot Played");
                    break;
                case BattingMode.Attacking:
                {
                    BallCollide();
                    // Play an attacking shot to the right
                    playerAnimator.SetBool(CoverDrive, true);
                    _stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
                    _animtime = _stateInfo.normalizedTime * _stateInfo.length;
                    _animationTime = animationClip.length;
                    _animationName = "CoverDrive";
                    StartCoroutine(WaitToOffAnimation(_animtime));
                    Debug.Log("Attacking Right Shot Played");
                    break;
                }
                case BattingMode.Improvised:
                {
                    BallCollide();
                    // Play an improvised shot to the right
                    playerAnimator.SetBool(FlickShot, true);
                    _stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
                    _animtime = _stateInfo.normalizedTime * _stateInfo.length;
                    _animationTime = animationClip.length;
                    _animationName = "FlickShot";
                    StartCoroutine(WaitToOffAnimation(_animtime));
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
                    playerAnimator.SetBool(Defence, true);
                    _stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
                    _animtime = _stateInfo.normalizedTime * _stateInfo.length;
                    _animationTime = animationClip.length;
                    _animationName = "Defence";
                    StartCoroutine(WaitToOffAnimation(_animtime));
                    Debug.Log("Defensive Left Shot Played");
                    break;
                case BattingMode.Attacking:
                    BallCollide();
                    // Play an attacking shot to the left
                    playerAnimator.SetBool(FlickShot, true);
                    _stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
                    _animtime = _stateInfo.normalizedTime * _stateInfo.length;
                    _animationTime = animationClip.length;
                    _animationName = "FlickShot";
                    StartCoroutine(WaitToOffAnimation(_animtime));
                    Debug.Log("Attacking Left Shot Played");
                    break;
                case BattingMode.Improvised:
                    BallCollide();
                    // Play an improvised shot to the left
                    playerAnimator.SetBool(CoverDrive, true);
                    _stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
                    _animtime = _stateInfo.normalizedTime * _stateInfo.length;
                    _animationTime = animationClip.length;
                    _animationName = "CoverDrive";
                    StartCoroutine(WaitToOffAnimation(_animtime));
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
                    playerAnimator.SetBool(Defence, true);
                    _stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
                    _animtime = _stateInfo.normalizedTime * _stateInfo.length;
                    _animationTime = animationClip.length;
                    _animationName = "Defence";
                    StartCoroutine(WaitToOffAnimation(_animtime));
                    Debug.Log("Defensive Vertical Shot Played");
                    break;
                case BattingMode.Attacking:
                    BallCollide();
                    // Play an attacking shot to the Vertical
                    playerAnimator.SetBool(Hook, true);
                    _stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
                    _animtime = _stateInfo.normalizedTime * _stateInfo.length;
                    _animationTime = animationClip.length;
                    _animationName = "Hook";
                    StartCoroutine(WaitToOffAnimation(_animtime));
                    Debug.Log("Attacking Vertical Shot Played");
                    break;
                case BattingMode.Improvised:
                    BallCollide();
                    // Play an improvised shot to the Vertical
                    playerAnimator.SetBool(LegGlance, true);
                    _stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
                    _animtime = _stateInfo.normalizedTime * _stateInfo.length;
                    _animationTime = animationClip.length;
                    _animationName = "LegGlance";
                    StartCoroutine(WaitToOffAnimation(_animtime));
                    Debug.Log("Improvised Vertical Shot Played");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerator WaitToOffAnimation(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            playerAnimator.SetBool(_animationName, false);
            yield return new WaitForSecondsRealtime(2);
            ballRigidbody.isKinematic = true;
            AIBowler.Instance.ResetBall();
        }
    }
}