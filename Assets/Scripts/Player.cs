using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speedMax;
    [SerializeField] float groundNormalYMin = 0.7f;
    [SerializeField] float groundDamping = 8f;
    [SerializeField] float airDamping = 0.5f;
    [SerializeField] float jumpSpeed;

    bool isGrounded;

    private Rigidbody rb;

    PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb.sleepThreshold = -1;
    }

    void Update()
    {
        var moveVec = playerInput.actions["Move"].ReadValue<Vector2>();

        var cameraDir = playerInput.camera.transform.forward;
        cameraDir.y = 0;
        cameraDir = cameraDir.normalized;

        var cameraRight = playerInput.camera.transform.right;

        var moveVec3D =
            cameraDir * moveVec.y * speedMax
            + cameraRight * moveVec.x * speedMax;
        transform.position = transform.position + moveVec3D * Time.deltaTime;

        if (playerInput.actions["Jump"].WasPressedThisFrame()
   && isGrounded)
        {
            Vector3 jumpVec = new Vector3(0, jumpSpeed, 0);
            rb.AddForce(jumpVec, ForceMode.VelocityChange);
        }
    }
    private void FixedUpdate()
    {
        if (isGrounded)
        {
            rb.linearDamping = groundDamping;
        }
        else
        {
            rb.linearDamping = airDamping;
        }
        isGrounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts) 
        {
            if (contact.normal.y >= groundNormalYMin)
            {
                isGrounded = true;
            }
        }
    }
}
