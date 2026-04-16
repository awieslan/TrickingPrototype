/*
 * PROMPT:
 * "Please write a script with a public method that smoothly moves an object from its current
 * transform to another which is passed as an input. It should include position, rotation, and
 * scale. It should move at a configurable and consistent speed.
 *
 * Also, please create another, separate script to be placed on an empty trigger area. When an
 * object with a certain tag enters the area, the script should call the transform method from
 * the previous script on an object that is assigned in the unity inspector. It should also pass
 * that method the transform parameters of an object that is defined in the Unity inspector.
 *
 * The desired final behavior should be as follows: There is an empty volume that acts as a
 * trigger area. When an object with the tag "Person" enters the trigger, the trigger script
 * should call the method on the predetermined moving object and pass it the predetermined
 * transform. The moving object should then smoothly move to the destination and remain there,
 * stationary."
 */

using UnityEngine;

public class SmoothMover : MonoBehaviour
{
    [Tooltip("Units per second for position movement.")]
    public float moveSpeed = 2f;

    [Tooltip("Degrees per second for rotation.")]
    public float rotateSpeed = 90f;

    [Tooltip("Scale units per second.")]
    public float scaleSpeed = 1f;

    private bool _isMoving;
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private Vector3 _targetScale;

    /// <summary>
    /// Begins smoothly interpolating this object's position, rotation, and scale
    /// toward the given target values at the configured speeds.
    /// </summary>
    public void MoveToTransform(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        _targetPosition = position;
        _targetRotation = rotation;
        _targetScale = scale;
        _isMoving = true;
    }

    /// <summary>
    /// Convenience overload that copies position, rotation, and scale from another Transform.
    /// </summary>
    public void MoveToTransform(Transform target)
    {
        MoveToTransform(target.position, target.rotation, target.localScale);
    }

    private void Update()
    {
        if (!_isMoving) return;

        float posStep = moveSpeed * Time.deltaTime;
        float rotStep = rotateSpeed * Time.deltaTime;
        float sclStep = scaleSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, posStep);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotStep);
        transform.localScale = Vector3.MoveTowards(transform.localScale, _targetScale, sclStep);

        bool positionReached = Vector3.Distance(transform.position, _targetPosition) < 0.001f;
        bool rotationReached = Quaternion.Angle(transform.rotation, _targetRotation) < 0.01f;
        bool scaleReached = Vector3.Distance(transform.localScale, _targetScale) < 0.001f;

        if (positionReached && rotationReached && scaleReached)
        {
            transform.position = _targetPosition;
            transform.rotation = _targetRotation;
            transform.localScale = _targetScale;
            _isMoving = false;
        }
    }
}
