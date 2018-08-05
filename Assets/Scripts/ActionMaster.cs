using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
    public enum Action {Tidy, Reset, EndTurn, EndGame, Undefined};
    private static int maxBowls = 21;

    private int currentBowl = 1, currentFrame = 1;
    private int[] bowls = new int[ActionMaster.maxBowls];
            

    public static Action NextAction(List<int> pinFalls)
    {
        ActionMaster actionMaster = new ActionMaster();

        Action currentAction = Action.Undefined;
        foreach (int pinFall in pinFalls)
        {
            currentAction = actionMaster.Bowl(pinFall);
        }

        return currentAction;
    }   

    private Action Bowl(int pins)
    {        
        // Defensive checks.
        if ((pins < 0) || (pins > 10)) {throw new UnityException("Invalid number of pins: " + pins);}
        if ((currentBowl < 1) || (currentBowl > ActionMaster.maxBowls))
        {
            throw new UnityException("Invalid number of bowls: " + currentBowl);
        }

        this.bowls[currentBowl - 1] = pins;

        // Last frame conditions
        if (currentBowl >= 19)
        {
            Debug.Log("Last frame detected.");

            Action lastFrameAction = Action.Undefined;

            // If end of game then EndGame immediatley.
            if (this.CheckEndGameCondition()) { return Action.EndGame; }

            // 20th bowl special condition: When 19th with a strike, but 20th was not, do a tidy.
            else if ((this.currentBowl == 20) && (this.bowls[18] == 10) && (this.bowls[19] < 10))
            {
                lastFrameAction = Action.Tidy;
            }
            // Strike
            else if (pins == 10)
            {
                lastFrameAction = Action.Reset;
            }            
            // Spare
            else if (this.FrameIsSpare())
            {
                lastFrameAction = Action.Reset;
            }
            // Non-special bowl
            else
            {
                lastFrameAction = Action.Tidy;
            }
                        
            this.currentBowl++;
            return lastFrameAction;
        }
        else
        {
            // Strike
            if (((currentBowl % 2) != 0) && (pins == 10))
            {
                this.currentFrame++;
                this.currentBowl += 2;
                return Action.EndTurn;
            }
            // Last bowl of frame.
            else if ((this.FrameIsSpare()  || (currentBowl % 2) == 0))
            {
                this.currentFrame++;
                this.currentBowl++;
                return Action.EndTurn;
            }
            else if ((currentBowl % 2) != 0)
            {
                this.currentBowl++;
                return Action.Tidy;
            }
        }

        // Other behaviour.
        throw new UnityException("Don't know what to return.");
    }

    private bool FrameIsSpare()
    {
        int frameStartIndex = (this.currentFrame * 2) - 2;

        int frameBowl1 = this.bowls[frameStartIndex];
        int frameBowl2 = this.bowls[frameStartIndex + 1];
        int pinsTotal = (frameBowl1 + frameBowl2);
        return ((pinsTotal == 10) && (frameBowl1 < 10));
    }

    private bool Award21stBowl()
    {
        // Can only determine on at least 20th bowl
        if (this.currentBowl >= 20)
        {
            return ((bowls[18] + bowls[19]) >= 10);
        }
        else
        {
            return false;
        }         
    }

    private bool CheckEndGameCondition()
    {
        // Check if 21st bowl.
        if (this.currentBowl == ActionMaster.maxBowls)
        {
            return true;
        }

        // Check if 20th bowl and not allowed 21st.
        if ((! this.Award21stBowl()) && (this.currentBowl == 20))
        { 
            return true;
        }
        
        return false;
    }
}
