using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Movement : MonoBehaviour
{
    [SerializeField]
    InputAction thrust;
    [SerializeField]
    InputAction rotation;
    Rigidbody rb;
    [SerializeField]
    float thrustForce = 1000f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        }
    }



    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        // Debug.Log("Rotation Input value: " + rotationInput);
        
    }
}
