using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    public float standingThreshold;


    // ===================================================================
    // Methods
    // ===================================================================

    public bool IsStanding()
    {
        return (transform.forward.y > standingThreshold);
    }

    private void Update()
    {
        // this.IsStanding();
    }
}

