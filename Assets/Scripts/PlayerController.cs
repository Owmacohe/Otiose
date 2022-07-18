using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 0.5f;
    [SerializeField]
    float rotationSpeed = 0.2f;

    float startSpeed;
    Vector2 direction;
    bool isWalking, isRunning, isJumping;
    Animator anim;
    Transform camera, cameraOffset;
    Rigidbody rb;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        camera = Camera.main.transform;
        cameraOffset = camera.parent;
        rb = GetComponent<Rigidbody>();

        startSpeed = movementSpeed;
    }

    void OnMove(InputValue input)
    {
        direction = input.Get<Vector2>();
        
        if (!direction.Equals(Vector2.zero))
        {
            isWalking = true;
            anim.SetBool("isWalking", true);
        }
        else
        {
            isWalking = false;
            anim.SetBool("isWalking", false);
        }
    }

    void OnRun(InputValue input)
    {
        if (input.isPressed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    void OnJump(InputValue input)
    {
        Invoke(nameof(StopJump), 1);
        
        isJumping = true;
        anim.SetBool("isJumping", true);
    }

    void StopJump()
    {
        isJumping = false;
        anim.SetBool("isJumping", false);
    }

    void OnLookX(InputValue input)
    {
        cameraOffset.Rotate(Vector3.up, input.Get<float>() * rotationSpeed);
    }
    
    void OnLookY(InputValue input)
    {
        if (camera.eulerAngles.x <= 60)
        {
            camera.transform.Rotate(Vector3.right, input.Get<float>() * -rotationSpeed, Space.Self);   
        }

        if (camera.eulerAngles.x > 180)
        {
            camera.localEulerAngles = Vector3.right * 0.01f;
        }
        else if (camera.eulerAngles.x > 60 && camera.eulerAngles.x < 180)
        {
            camera.localEulerAngles = Vector3.right * 59.9f;
        }
    }

    void Update()
    {
        cameraOffset.position = transform.position;
    }

    void FixedUpdate()
    {
        if (isWalking)
        {
            transform.rotation = cameraOffset.rotation;
            
            if (isRunning && !isJumping)
            {
                movementSpeed = 2 * startSpeed;
                anim.SetBool("isRunning", true);
            }
            else
            {
                movementSpeed = startSpeed;
                anim.SetBool("isRunning", false);
            }
            
            rb.transform.position += (transform.forward * direction.y + transform.right * direction.x) * movementSpeed;
        }
        else
        {
            movementSpeed = startSpeed;
            anim.SetBool("isRunning", false);
        }
    }
}
