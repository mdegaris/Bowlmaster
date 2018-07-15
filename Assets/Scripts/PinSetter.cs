using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    public Text pinCounter;

    private bool ballEnteredBox;


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

    }

    private void Update()
    {
        if (this.ballEnteredBox)
        {
            this.pinCounter.color = Color.red;
            this.pinCounter.text = this.CountStanding().ToString();
        }
        else
        {
            this.pinCounter.color = Color.black;
        }        
    }

    private void OnTriggerEnter(Collider collision)
    { 
        if (collision.GetComponent<Ball>())
        {            
            this.ballEnteredBox = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.GetComponent<Ball>())
        {            
            this.ballEnteredBox = false;
        }
        else if (collision.GetComponent<Pin>())
        {
            Destroy(collision.gameObject);
        }
    }
}
