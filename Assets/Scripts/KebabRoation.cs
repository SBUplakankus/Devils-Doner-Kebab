using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KebabRoation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10f;  // Adjust this value to control the speed of rotation

    // Update is called once per frame
    void Update()
    {
        // Rotate around the Y-axis at the specified speed
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }
    
    
}
