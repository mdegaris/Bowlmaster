using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public float standingThreshold;

    // private Rigidbody pinRigidbody;


    public bool IsStanding()
    {
        return (transform.up.y > 0.97);
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(name + " " + IsStanding());
    }
}

