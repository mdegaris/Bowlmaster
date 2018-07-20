using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    public Ball ball;

    private Vector3 offset;
    private float headPinPosZ;

    // ===================================================================
    // Methods
    // ===================================================================

    // Use this for initialization
    private void Start()
    {
        // Head pin z position
        this.headPinPosZ = 1829;
        this.offset = this.gameObject.transform.position - this.ball.transform.position;       
    }

    private void LateUpdate()
    {
        // Stop camera motion when it reaches the head pin position.
        if (this.ball.transform.position.z <= this.headPinPosZ)
        {
            Vector3 newCameraPosition = this.ball.transform.position + this.offset;
            this.gameObject.transform.position = newCameraPosition;
        }
    }
}
