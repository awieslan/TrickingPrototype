using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // [Header("Movement Settings")]
    // public float moveSpeed = 5f;

    void Update()
    {
        //HandleMovement();
    }

    /// <summary>
    /// Handles player movement based on WASD input using Unity's new Input System
    /// W = +Z, S = -Z, A = -X, D = +X
    /// </summary>
    // void HandleMovement()
    // {
    //     // Get keyboard input using Unity's new Input System
    //     Keyboard keyboard = Keyboard.current;
    //     if (keyboard == null) return;

    //     // Get input from WASD keys
    //     float horizontalInput = 0f; // X-axis (A/D)
    //     float verticalInput = 0f;   // Z-axis (W/S)

    //     if (keyboard.wKey.isPressed)
    //     {
    //         verticalInput = 1f; // Move forward (+Z)
    //     }
    //     else if (keyboard.sKey.isPressed)
    //     {
    //         verticalInput = -1f; // Move backward (-Z)
    //     }

    //     if (keyboard.aKey.isPressed)
    //     {
    //         horizontalInput = -1f; // Move left (-X)
    //     }
    //     else if (keyboard.dKey.isPressed)
    //     {
    //         horizontalInput = 1f; // Move right (+X)
    //     }

    //     // Calculate movement direction
    //     Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        
    //     // Normalize to prevent faster diagonal movement
    //     if (movement.magnitude > 1f)
    //     {
    //         movement.Normalize();
    //     }

    //     // Rotate player to face movement direction if moving
    //     if (movement.magnitude > 0.1f)
    //     {
    //         Quaternion targetRotation = Quaternion.LookRotation(movement);
    //         transform.rotation = targetRotation;
    //     }

    //     // Move the player
    //     transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

    //     // Clamp position to keep player within 8 units from origin on X and Z axes
    //     Vector3 currentPosition = transform.position;
    //     currentPosition.x = Mathf.Clamp(currentPosition.x, -8f, 8f);
    //     currentPosition.z = Mathf.Clamp(currentPosition.z, -8f, 8f);
    //     transform.position = currentPosition;
    // }

    /// <summary>
    /// Called when the player collides with another object
    /// Destroys the player if it collides with a car
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has a CarMovement component (indicating it's a car)
        if (other.GetComponent<CarMovement>() != null)
        {
            // Destroy the player
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Alternative collision detection using OnCollisionEnter for non-trigger colliders
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a CarMovement component (indicating it's a car)
        if (collision.gameObject.GetComponent<CarMovement>() != null)
        {
            // Destroy the player
            Destroy(gameObject);
        }
    }
}
