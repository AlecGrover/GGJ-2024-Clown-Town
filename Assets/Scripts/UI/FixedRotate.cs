using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotate : MonoBehaviour
{
    public Vector3 FixedRotationVector = new Vector3(0, 0, 0);
    private Quaternion FixedRotation = Quaternion.identity;
    
    // Start is called before the first frame update
    void Start()
    {
        FixedRotation = Quaternion.Euler(FixedRotationVector);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = FixedRotation;
    }
}
