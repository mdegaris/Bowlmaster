using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public float standingThreshold;


    public bool IsStanding()
    {
        return (transform.forward.y > standingThreshold);
    }

    private void Update()
    {
        // this.IsStanding();
    }
}

