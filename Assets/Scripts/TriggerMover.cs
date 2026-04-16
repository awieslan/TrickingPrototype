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

[RequireComponent(typeof(Collider))]
public class TriggerMover : MonoBehaviour
{
    [Tooltip("The object that should move when the trigger is activated. Must have a SmoothMover component.")]
    public SmoothMover objectToMove;

    [Tooltip("The transform whose position, rotation, and scale define the destination.")]
    public Transform destination;

    [Tooltip("Minimum delay (seconds) before movement starts after trigger.")]
    public float minDelay = 0.5f;

    [Tooltip("Maximum delay (seconds) before movement starts after trigger.")]
    public float maxDelay = 2f;

    [Tooltip("Only trigger once. Disable to allow re-triggering.")]
    public bool triggerOnce = true;

    private bool _hasTriggered;
    private bool _waiting;
    private float _moveTime;

    private void OnTriggerEnter(Collider other)
    {
        if (_waiting || (_hasTriggered && triggerOnce)) return;

        if (!other.CompareTag("Person")) return;

        if (objectToMove == null || destination == null)
        {
            Debug.LogWarning("TriggerMover: objectToMove or destination is not assigned.", this);
            return;
        }

        _waiting = true;
        _hasTriggered = true;
        _moveTime = Time.time + Random.Range(minDelay, maxDelay);
    }

    private void Update()
    {
        if (!_waiting) return;

        if (Time.time >= _moveTime)
        {
            _waiting = false;
            objectToMove.MoveToTransform(destination);
        }
    }
}
