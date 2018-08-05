using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    private static float settleTimeInSecs = 5f;

    public Text pinsStanding;

    private int lastStandingCount = -1;
    private ActionMaster actionMaster;
    private PinAnimator pinAnimator;
    private Ball ball;
    private float lastChangedTime;
    private int pinsStandingOnBowlStart;
    private bool ballOutOfPlay = false;
    


    // ===================================================================
    // Methods
    // ===================================================================

    public void StartBowl()
    {
        this.pinsStandingOnBowlStart = this.CountStanding();
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

    public void UpdatePinStanding(int standingCount, Color color)
    {
        this.pinsStanding.text = standingCount.ToString();
        this.pinsStanding.color = color;
    }

    public void BallFellInGutter()
    {
        this.ballOutOfPlay = true;
    }

    // Use this for initialization
    private void Start()
    {
        actionMaster = new ActionMaster();
        this.pinAnimator = GameObject.FindObjectOfType<PinAnimator>();
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
        else if ((currentTime - this.lastChangedTime) > PinSetter.settleTimeInSecs)
        {
            return true;
        }

        return false;
    }

    private void EndRoll()
    {        
        this.lastStandingCount = -1;
        this.ballOutOfPlay = false;
        this.pinsStanding.color = Color.green;
        ball.Reset();

        int fallenCount = this.pinsStandingOnBowlStart - this.CountStanding();
        //ActionMaster.Action action = ActionMaster.NextAction(fallenCount);        
        //this.pinAnimator.DoAction(action);
    }

    private void OnTriggerEnter(Collider collision)
    {        
        GameObject enteredObj = collision.gameObject;
        if (enteredObj.GetComponent<Ball>())
        {            
            this.ballOutOfPlay = true;
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
