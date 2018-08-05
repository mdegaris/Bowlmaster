using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinAnimator : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    private static string animatorTidyTrigger = "tidyTrigger";
    private static string animatorResetTrigger = "resetTrigger";

    public GameObject pinSet;

    private PinSetter pinSetter;
    private Animator animator;


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

    public void DoAction(ActionMaster.Action action)
    {
        Debug.Log("DoAction: " + action);

        switch (action)
        {
            case ActionMaster.Action.Tidy:
                this.Tidy();
                break;
            case ActionMaster.Action.Reset:
            case ActionMaster.Action.EndTurn:
                this.Reset();
                break;
        }
    }

    private void Tidy()
    {
        this.animator.SetTrigger(PinAnimator.animatorTidyTrigger);
    }

    private void Reset()
    {
        this.animator.SetTrigger(PinAnimator.animatorResetTrigger);
        this.pinSetter.UpdatePinStanding(10, Color.green);
    }

    private void Start()
    {
        this.animator = GetComponent<Animator>();
        this.pinSetter = GameObject.FindObjectOfType<PinSetter>();
    }
}
