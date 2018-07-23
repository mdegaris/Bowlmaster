using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    public GameObject pinSet;


    // ===================================================================
    // Methods
    // ===================================================================

    // Raise standing pins by distanceToRaise
    public void RaisePins()
    {
        Debug.Log("Raising pins.");

        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.RaiseIfStanding();
        }
    }

    // Lower standing pins by distanceToRaise
    public void LowerPins()
    {
        Debug.Log("Lowering pins.");

        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    // Renew standing pins by distanceToRaise   
    public void RenewPins()
    {
        Debug.Log("Renew pins.");
        Instantiate(this.pinSet, new Vector3(0, 0, 1829), Quaternion.identity);
    }
}
