using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBox : MonoBehaviour
{
    private PinSetter pinSetter;

    private void Start()
    {
        this.pinSetter = GameObject.FindObjectOfType<PinSetter>();
    }

    private void OnTriggerExit(Collider other)
    {
        this.pinSetter.BallFellInGutter();
    }
}
