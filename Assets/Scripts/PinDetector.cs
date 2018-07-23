using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinDetector : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    private static float settleTimeInSecs = 5f;

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

            if (CheckIfPinsSettled())
            {
                this.EndRoll();
            }
        }      
    }

    private bool CheckIfPinsSettled()
    {
        // Update lastStandingCount
        int currentStandingCount = this.CountStanding();
        float currentTime = Time.realtimeSinceStartup;

        // Check if the pins have settled, i.e. the standing count hasn't changed for settleTimeInSecs
        if ((this.lastStandingCount == -1) || (this.lastStandingCount != currentStandingCount))
        {
            this.lastStandingCount = currentStandingCount;
            this.lastChangedTime = currentTime;
        }
        else if ((currentTime - this.lastChangedTime) > PinDetector.settleTimeInSecs)
        {
            return true;            
        }

        return false;
    }

    private void EndRoll()
    {
        Debug.Log("Pins have settled so end turn and reset.");
        
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
