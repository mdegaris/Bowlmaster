using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public float distanceToRaise;



    // Raise standing pins by distanceToRaise
    public void RaisePins()
    {
        Debug.Log("Raising pins.");
    }

    // Lower standing pins by distanceToRaise
    public void LowerPins()
    {
        Debug.Log("Lowering pins.");
    }

    // Renew standing pins by distanceToRaise   
    public void RenewPins()
    {
        Debug.Log("Renew pins.");
    }
}
