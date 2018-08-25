using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSetter : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    private static string animatorTidyTrigger = "tidyTrigger";
    private static string animatorResetTrigger = "resetTrigger";

    public GameObject pinSet;

    private Animator animator;
    private PinCounter pinCounter;


    // ===================================================================
    // Methods
    // ===================================================================


    // Raise standing pins by distanceToRaise
    public void RaisePins()
    {
        Debug.Log("Raising pins.");

        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding()) { pin.Raise();  }
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
                this.pinCounter.Reset();
                break;
            case ActionMaster.Action.Reset:
            case ActionMaster.Action.EndTurn:
                this.Reset();
                this.pinCounter.Reset(endTurn: true);
                break;
        }
    }

    private void Tidy()
    {
        this.animator.SetTrigger(PinSetter.animatorTidyTrigger);
    }

    private void Reset()
    {
        this.animator.SetTrigger(PinSetter.animatorResetTrigger);
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject enteredObj = collision.gameObject;
        if (enteredObj.GetComponent<Ball>())
        {
            this.pinCounter.SetBallOutOfPlay(true);
            Debug.Log("Ball entered pin setter.");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        GameObject exitedObj = collision.gameObject;
        if (exitedObj.GetComponent<Pin>())
        {
            Destroy(exitedObj.GetComponent<Pin>().gameObject);
            Debug.Log("Destroy " + exitedObj);
        }
    }

    private void Start()
    {
        this.animator = GetComponent<Animator>();
        this.pinCounter = GameObject.FindObjectOfType<PinCounter>();
    }
}
