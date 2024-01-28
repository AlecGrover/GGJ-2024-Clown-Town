using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    
    // Adjustable parameters
    [SerializeField]
    private float speed = 5.0f;

    // Components
    private Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        
        Vector3 movementInput = new Vector3(inputX + inputY, 0.0f, inputY - inputX).normalized;
        playerRigidbody.MovePosition(playerRigidbody.position + movementInput * (speed * Time.deltaTime));
    }
}
