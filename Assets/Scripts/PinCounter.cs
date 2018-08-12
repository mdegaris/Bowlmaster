using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    private static float settleTimeInSecs = 5f;

    public Text pinsStandingFeedback; 

    private GameManager gameManager;
    private int lastStandingCount = -1;
    private int lastSettledPinCount = 10;
    private float lastChangedTime;
    private Ball ball;
    // Indicates if the ball is rolling and is possibly going to hit pins.
    private bool ballOutOfPlay = false;


    // Reset the state of the pin counter.
    public void Reset()
    {
        this.lastStandingCount = -1;
        this.lastSettledPinCount = 10;
        this.ballOutOfPlay = false;        
    }


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


    private void EndRoll()
    {              
        int currentStanding = this.CountStanding();
        int fallenCount = (this.lastSettledPinCount - currentStanding);
        this.gameManager.Bowl(fallenCount);

        this.lastSettledPinCount = currentStanding;
        this.UpdatePinStanding(currentStanding, Color.green);
    }


    public void UpdatePinStanding(int standingCount, Color color)
    {
        this.pinsStandingFeedback.text = standingCount.ToString();
        this.pinsStandingFeedback.color = color;
    }


    public void SetBallOutOfPlay(bool ballInPlay)
    {
        this.ballOutOfPlay = ballInPlay;
    }


    private void OnTriggerExit(Collider exitingObj)
    {
        if (exitingObj.GetComponent<Ball>()) { this.ballOutOfPlay = true; }
    }


    private void Start()
    {
        this.gameManager = GameObject.FindObjectOfType<GameManager>();
        this.ball = GameObject.FindObjectOfType<Ball>();
    }


    private void Update()
    {
        if (this.ballOutOfPlay)
        {
            this.UpdatePinStanding(this.CountStanding(), Color.red);

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
        else if ((currentTime - this.lastChangedTime) > PinCounter.settleTimeInSecs)
        {
            this.lastSettledPinCount = currentStandingCount;
            return true;
        }

        return false;
    }
}
