using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public int CountStanding()
    {
        int standingPins = 0;
        Pin[] lanePins = FindObjectsOfType<Pin>();
        foreach (Pin lanePin in lanePins)
        {
            standingPins = lanePin.IsStanding() ? ++standingPins : standingPins;
        }

        return standingPins;
    }

    // Use this for initialization
    private void Start()
    {

    }
}
