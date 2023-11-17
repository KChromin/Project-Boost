using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Variables

    private Rigidbody playerRigidbody;
    private AudioSource audioSource;

    [SerializeField]
    private ParticleSystem thrustParticles;

    [SerializeField]
    private ParticleSystem rightSideThrustParticles;

    [SerializeField]
    private ParticleSystem leftSideThrustParticles;

    [SerializeField]
    private AudioClip thrustSound;

    [SerializeField]
    private float thrustPower;

    [SerializeField]
    private float rotationPower;

    #endregion Variables

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

    #region Thrusting

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        playerRigidbody.AddRelativeForce(0, thrustPower, 0, ForceMode.Acceleration);
        SoundThrust(true);
        ParticlesThrust(true);
    }

    private void StopThrusting()
    {
        SoundThrust(false);
        ParticlesThrust(false);
    }

    #endregion Thrusting

    #region Rotation

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            StartRotationThrustingLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            StartRotationThrustingRight();
        }
        else
        {
            StopRotationThrustingParticles();
        }
    }

    private void StartRotationThrustingRight()
    {
        ApplyRotation(-rotationPower);
        ParticlesSideThrust();
    }

    private void StartRotationThrustingLeft()
    {
        ApplyRotation(rotationPower);
        ParticlesSideThrust(false);
    }

    private void StopRotationThrustingParticles()
    {
        ParticlesSideThrust(stopPlaying: true);
    }
    
    private void ApplyRotation(float rotatePower)
    {
        playerRigidbody.AddRelativeTorque(Vector3.forward * rotatePower, ForceMode.Acceleration);
    }

    #endregion Rotation

    #region Sound & Particles

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

    private void ParticlesThrust(bool soundShouldBePlayed)
    {
        if (soundShouldBePlayed)
        {
            if (thrustParticles.isPlaying) return;
            thrustParticles.Play();
            return;
        }

        thrustParticles.Stop();
    }

    private void ParticlesSideThrust(bool activeRightThruster = true, bool stopPlaying = false)
    {
        if (stopPlaying)
        {
            leftSideThrustParticles.Stop();
            rightSideThrustParticles.Stop();
            return;
        }

        if (activeRightThruster)
        {
            rightSideThrustParticles.Play();
            leftSideThrustParticles.Stop();
        }
        else
        {
            leftSideThrustParticles.Play();
            rightSideThrustParticles.Stop();
        }
    }

    #endregion Sound & Particles

}