using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ScoreMaster
{
    private enum BowlType { Normal, Spare, Strike };

    public static List<int> ScoreCumulative(List<int> bowls)
    {
        List<int> cumulativeScores = new List<int>();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames(bowls))
        {
            runningTotal += frameScore;
        }

        return cumulativeScores;
    }


    public static List<int> ScoreFrames(List<int> bowls)
    {
        List<int> frameList = new List<int>();

        int frameScore = 0;        
        int nextTallyBowl = 1;
        for (int bi = 0; bi < bowls.Count; bi++)
        {
            int pins = bowls[bi];

            // Strike
            if (pins == 10)
            {
                nextTallyBowl = bi + 2;
            }
            // Spare
            else if ((bi % 2 != 0) && (bowls[bi] + bowls[bi-1] == 10))
            {
                nextTallyBowl = bi + 1;
            }

            frameScore += pins;
            if (nextTallyBowl == bi)
            {
                frameList.Add(frameScore);
                frameScore = 0;
                nextTallyBowl += (bi % 2 == 0) ? 1 : 2;
            }
            else
            {

            }
            /*
            else
            {
                tallyCountdown = tallyCountdown > 0 ? tallyCountdown -= 1 : tallyCountdown;
            }*/
        }

        return frameList;
    }
}