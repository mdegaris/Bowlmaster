using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    public float standingThreshold;
    public float distanceToRaise;

    private Rigidbody pinRigidbody;


    // ===================================================================
    // Methods
    // ===================================================================

    public bool IsStanding()
    {
        return (transform.forward.y > standingThreshold);
    }

    public void RaiseIfStanding()
    {
        if (this.IsStanding()) { this.Raise(); }
    }

    public void Raise()
    {
        // Ensure the pins are in an upright position.
        this.pinRigidbody.useGravity = false;
        this.pinRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        this.transform.Translate(new Vector3(0, this.distanceToRaise, 0), Space.World);
        this.transform.rotation = Quaternion.Euler(270, 0, 0);
    }

    public void Lower()
    {
        // Lower and reset after ensuring uprightness when raising pins.
        this.transform.Translate(new Vector3(0, 0, -this.distanceToRaise));
        this.pinRigidbody.constraints = RigidbodyConstraints.None;
        this.GetComponent<Rigidbody>().useGravity = true;        
    }


    private void Start()
    {
        this.pinRigidbody = GetComponent<Rigidbody>();
    }
}

