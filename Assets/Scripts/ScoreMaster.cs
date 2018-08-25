using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class ScoreMaster
{

    public static List<int> ScoreCumulative(List<int> bowls)
    {
        List<int> cumulativeScores = new List<int>();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames(bowls))
        {
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }

        return cumulativeScores;
    }


    public static List<int> ScoreFrames(List<int> rolls)
    {
        List<int> frameList = new List<int>();

        int frameRoll1 = 0, frameRoll2 = 0;

        // Calculate each frame score.
        // secondRoll is the index of the 2nd roll in the frame.
        // End loop when no more rolls and number of frames is not over max frames.
        for (int secondRoll = 1; (secondRoll < rolls.Count) && (frameList.Count < 10); secondRoll += 2)
        {            
            // Frame rolls (for typical 2 frame roll).
            frameRoll1 = rolls[secondRoll - 1];
            frameRoll2 =  rolls[secondRoll];

            // Frame isn't a spare or strike.
            if (frameRoll1 + frameRoll2 < 10)
            {
                frameList.Add(frameRoll1 + frameRoll2);
            }

            // Ensure we can have at least one bonus roll available.
            if (rolls.Count - secondRoll <= 1) { break; }

            // Strike condition check.
            if (frameRoll1 == 10)
            {
                secondRoll--; // Strike frame is 1 roll.
                frameList.Add(10 + rolls[secondRoll + 1] + rolls[secondRoll + 2]);
            }
            // Spare condition check.
            else if (frameRoll1 + frameRoll2 == 10)
            {
                frameList.Add(10 + rolls[secondRoll + 1]);
            }                                 
        }

        return frameList;
    }

    /* THIS WORKS also
    public static List<int> ScoreFrames(List<int> bowls)
    {
        List<int> frameList = new List<int>();

        int freeRolls = 0, bowlIndex = 0, frameScore = 0;

        // The frame number that we'll calculate the score for.
        int frameNumber = 1;

        // Setup first frame start and end index.
        int frameStartIndex = 0, frameEndIndex = 1;

        // Calculate each frame score.
        while ((bowlIndex < bowls.Count) && (frameNumber <= 10))
        {
            // Reset score and free rolls.
            freeRolls = frameScore = 0;

            // Detect a special bowl condition and set the number of free rolls and frame end index.
            // Strike condition check.
            if (bowls[bowlIndex] == 10)
            {
                // Last frame has 3 rolls.
                if (frameNumber == 10) { frameEndIndex = frameStartIndex + 2; }
                // If not, normal frame is only 1 roll in length.
                else { frameEndIndex = frameStartIndex; }

                freeRolls = 2;
            }
            // Spare condition check.
            else if (((bowlIndex + 1) < bowls.Count) && (bowls[bowlIndex] + bowls[bowlIndex + 1] == 10))
            {
                // Last frame has 3 rolls.
                if (frameNumber == 10) { frameEndIndex = frameStartIndex + 2; }

                freeRolls = 1;
            }

            // Calculate the score's tally end index
            int tallyEndIndex = (frameEndIndex + freeRolls);
            if (frameNumber == 10) { tallyEndIndex = frameEndIndex; }

            // If the required score tally exceeds the current number of bowls, we can't add-up a score.
            if (tallyEndIndex >= bowls.Count) { break; }

            // Calculate the frame score and add to list.
            for (int ti = frameStartIndex; ti <= tallyEndIndex; ti++) { frameScore += bowls[ti]; }
            frameList.Add(frameScore);

            // Setup frame indexes and number next iteration.
            bowlIndex = frameStartIndex = (frameEndIndex + 1);
            frameEndIndex = (frameStartIndex + 1);
            frameNumber = (frameList.Count + 1);
        }

        return frameList;
    }
    */
}