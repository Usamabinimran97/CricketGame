using System;
using _CricketGame.Scripts;
using UnityEngine;

public class BatsmanController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bowl"))
        {
            SwipeInput.Instance.ballRigidbody = other.collider.GetComponent<Rigidbody>();
        }
    }
}
