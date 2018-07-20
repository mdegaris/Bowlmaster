using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinDetector : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    private static float settleTimeInSecs = 3f;

    public int lastStandingCount = -1;
    public Text pinsStanding;

    private Ball ball;
    private float lastChangedTime;
    private bool ballEnteredBox = false;
  

    // ===================================================================
    // Methods
    // ===================================================================

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
        this.ball = GameObject.FindObjectOfType<Ball>();
    }

    private void Update()
    {
        if (this.ballEnteredBox)
        {
            this.pinsStanding.color = Color.red;
            this.pinsStanding.text = this.CountStanding().ToString();

            CheckStanding();
        }      
    }

    private void CheckStanding()
    {
        // Update lastStandingCount
        int currentStandingCount = this.CountStanding();
        float currentTime = Time.realtimeSinceStartup;

        // Call PinsHaveSettled() when they have
        if ((this.lastStandingCount == -1) || (this.lastStandingCount != currentStandingCount))
        {
            this.lastStandingCount = currentStandingCount;
            this.lastChangedTime = currentTime;
        }
        else if ((currentTime - this.lastChangedTime) > PinDetector.settleTimeInSecs)
        {
            this.PinsHaveSettled();
        }        
    }

    private void PinsHaveSettled()
    {
        Debug.Log("Pins have settled.");
        
        this.lastStandingCount = -1;
        this.ballEnteredBox = false;
        this.pinsStanding.color = Color.green;

        ball.Reset();
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject enteredObj = collision.gameObject;
        if (enteredObj.GetComponent<Ball>())
        {            
            this.ballEnteredBox = true;
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


}
