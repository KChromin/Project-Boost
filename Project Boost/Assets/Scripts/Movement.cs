using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip thrustSound;

    [SerializeField]
    private float thrustPower;

    [SerializeField]
    private float rotationPower;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.AddRelativeForce(0, thrustPower, 0, ForceMode.Acceleration);
            SoundThrust(true);
        }
        else
        {
            SoundThrust(false);
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotationPower);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotationPower);
        }
    }

    private void ApplyRotation(float rotatePower)
    {
        playerRigidbody.AddRelativeTorque(Vector3.forward * rotatePower, ForceMode.Acceleration);
    }

    private void SoundThrust(bool soundShouldBePlayed)
    {
        if (soundShouldBePlayed)
        {
            if (audioSource.isPlaying) return;
            audioSource.clip = thrustSound;
            audioSource.Play();
            return;
        }

        audioSource.Stop();
    }
}