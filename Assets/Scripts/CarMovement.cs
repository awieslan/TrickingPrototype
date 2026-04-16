using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private Vector3 movementDirection;
    private float speed;

    /// <summary>
    /// Initializes the car movement with direction and speed
    /// </summary>
    public void Initialize(Vector3 direction, float movementSpeed)
    {
        movementDirection = direction.normalized;
        speed = movementSpeed;
    }

    void Update()
    {
        // Move the car continuously in the specified direction
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        // Check if the car is out of bounds
    if (Mathf.Abs(transform.position.x) > 30f || Mathf.Abs(transform.position.z) > 30f)
    {
        Destroy(gameObject);
    }
    }
}