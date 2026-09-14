using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [Header("Forces")]
    [SerializeField] float rotationForce = 100f;
    [SerializeField] float thrustForce = 3000f;
    [Header("Audio Clips")]
    [SerializeField] AudioClip mainEngine;
    [Header("Engine Particles")]
    [SerializeField] public ParticleSystem mainEngineParticles; // Used by CollisionHandler.cs also
    [SerializeField] ParticleSystem leftSideEngineParticles;
    [SerializeField] ParticleSystem rightSideEngineParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
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
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying) mainEngineParticles.Play();
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        // Debug.Log("Rotation Input value: " + rotationInput);
        if (rotationInput != 0)
        {
            StartRotation(rotationInput);
        }
        else
        {
            StopRotation();
        }
    }

    private void StartRotation(float rotationInput)
    {
        // left turn particles (player's perspective)
        if (rotationInput > 0)
        {
            if (!rightSideEngineParticles.isPlaying)
            {
                leftSideEngineParticles.Stop();
                rightSideEngineParticles.Play();
            }
        }
        // right turn particles (player's perspective)
        else if (rotationInput < 0)
        {
            if (!leftSideEngineParticles.isPlaying)
            {
                rightSideEngineParticles.Stop();
                leftSideEngineParticles.Play();
            }
        }

        // physical rotation process
        rb.freezeRotation = true;
        transform.Rotate(-Vector3.forward * rotationForce * rotationInput * Time.fixedDeltaTime);
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }

    private void StopRotation()
    {
        rightSideEngineParticles.Stop();
        leftSideEngineParticles.Stop();
    }
}
