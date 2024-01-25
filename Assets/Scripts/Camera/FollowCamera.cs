using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float smoothing = 0.1f;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, 0);
    [SerializeField][Range(0.01f,1000f)]
    private float deadzone = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 offsetToTarget = transform.position - targetPosition;

        if (offsetToTarget.magnitude > deadzone)
        {
            float relativeDistance = (offsetToTarget.magnitude - deadzone) / deadzone;
            float dampingFactor = Mathf.Lerp(0, 1, relativeDistance);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothing * dampingFactor);
            transform.position = smoothPosition;
        }
    }
}
