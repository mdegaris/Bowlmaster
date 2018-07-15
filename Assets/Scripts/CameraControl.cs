using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Ball ball;

    private Vector3 offset;
    private float headPinPosZ;

    // Use this for initialization
    private void Start()
    {
        // Head pin z position.
        this.headPinPosZ = 1829;
        this.offset = this.gameObject.transform.position - this.ball.transform.position;
        
        Debug.Log("Camera offset vector: " + this.offset);
    }

    private void LateUpdate()
    {
        // Stop camera motion when it reaches the head pin position.
        if (this.gameObject.transform.position.z <= this.headPinPosZ)
        {
            Vector3 newCameraPosition = this.ball.transform.position + this.offset;
            this.gameObject.transform.position = newCameraPosition;
        }                
    }
}
