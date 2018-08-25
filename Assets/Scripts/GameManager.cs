using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================
    

    public Text pinsStanding;

    private List<int> rolls = new List<int>();    
    private PinSetter pinSetter;
    private Ball ball;
    private ScoreDisplay scoreDisplay;



    // ===================================================================
    // Methods
    // ===================================================================

    public void Bowl(int pinFall)
    {
        this.rolls.Add(pinFall);

        ActionMaster.Action nextAction = ActionMaster.NextAction(this.rolls);
        this.pinSetter.DoAction(nextAction);


        if (nextAction == ActionMaster.Action.EndGame)
        {
            Debug.Log("Game Over");
            this.rolls = new List<int>();
            this.scoreDisplay.Reset();
        }
        else
        {
            this.scoreDisplay.FillRolls(this.rolls);
            this.scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(this.rolls));
        }
        
        this.ball.Reset();
    }


    // Use this for initialization
    private void Start()
    {        
        this.pinSetter = GameObject.FindObjectOfType<PinSetter>();
        this.ball = GameObject.FindObjectOfType<Ball>();
        this.scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
    }
}
