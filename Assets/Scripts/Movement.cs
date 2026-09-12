using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] float thrustForce = 1000f; 
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
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }



    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        // Debug.Log("Rotation Input value: " + rotationInput);
        if(rotationInput != 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(-Vector3.forward * rotationStrength * rotationInput * Time.fixedDeltaTime);
            rb.freezeRotation = false;
        }
    }
}
