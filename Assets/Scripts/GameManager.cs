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

    private List<int> bowls = new List<int>();    
    private PinSetter pinSetter;
    private Ball ball;



    // ===================================================================
    // Methods
    // ===================================================================

    public void Bowl(int pinFall)
    {
        this.bowls.Add(pinFall);
        ActionMaster.Action nextAction = ActionMaster.NextAction(this.bowls);
        this.pinSetter.DoAction(nextAction);
        this.ball.Reset();
    }


    // Use this for initialization
    private void Start()
    {        
        this.pinSetter = GameObject.FindObjectOfType<PinSetter>();
        this.ball = GameObject.FindObjectOfType<Ball>();
    }
}
