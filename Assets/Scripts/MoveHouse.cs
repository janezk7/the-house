using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHouse : MonoBehaviour
{
    public float acceleration = 50; // how fast you accelerate
    public float accSprintMultiplier = 4; // how much faster you go when "sprinting"
    public float dampingCoefficient = 5; // how quickly you break to a halt after you stop your input
    public float rotationSpeed = 2.0f;
    Vector3 velocity; // current velocity

    Vector3 InitPosition;


    public void ResetHouse()
    {
        gameObject.transform.position = InitPosition;
        velocity = Vector3.zero;
        gameObject.transform.eulerAngles = Vector3.zero;
    }

    private void Awake()
    {
        InitPosition = gameObject.transform.position;
    }

    void Update()
    {
        //Reset position
        if(Input.GetKey(KeyCode.R))
        {
            ResetHouse();
            return;
        }

        // Input
        UpdateInput();

        // Physics
        velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;
    }

    void UpdateInput()
    {
        // Position
        velocity += GetAccelerationVector() * Time.deltaTime;

        // Rotate
        var speedUpCoeff = Input.GetKey(KeyCode.LeftShift) ? accSprintMultiplier : 1.0f;
        if(Input.GetKey(KeyCode.O))
            transform.Rotate(Vector3.up, 15 * Time.deltaTime * rotationSpeed * speedUpCoeff, Space.World);
        if(Input.GetKey(KeyCode.U))
            transform.Rotate(Vector3.up, -15 * Time.deltaTime * rotationSpeed * speedUpCoeff, Space.World);
    }

    Vector3 GetAccelerationVector()
    {
        Vector3 moveInput = default;

        void AddMovement(KeyCode key, Vector3 dir)
        {
            if (Input.GetKey(key))
                moveInput += dir;
        }

        AddMovement(KeyCode.I, Vector3.forward);
        AddMovement(KeyCode.K, Vector3.back);
        AddMovement(KeyCode.L, Vector3.right);
        AddMovement(KeyCode.J, Vector3.left);
        //AddMovement(KeyCode.Keypad9, Vector3.up);
        //AddMovement(KeyCode.Keypad7, Vector3.down);

        //Vector3 direction = transform.TransformVector(moveInput.normalized);
        Vector3 direction = moveInput.normalized; // move in global space

        if (Input.GetKey(KeyCode.LeftShift))
            return direction * (acceleration * accSprintMultiplier); // "sprinting"
        return direction * acceleration; // "walking"
    }
}
