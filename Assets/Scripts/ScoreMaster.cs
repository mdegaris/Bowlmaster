using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ScoreMaster
{
    private enum Bonus { None, Spare, Strike };

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
        //   0   1   2  3
        // { 10, 10, 2, 3 };
        // { 22, 15, 5 };
        List<int> frameList = new List<int>();

        int tally = 0;
        int tallyStart = 0;
        int tallyEnd = 1;
        Queue<Bonus> bonusQueue= new Queue<Bonus>();
        for (int bi = 0; bi < bowls.Count; bi++)
        {
            int pins = bowls[bi];

            // Strike
            if (pins == 10)
            {
                tallyEnd++;
                bonusQueue.Enqueue(Bonus.Strike);
            }
            // Spare
            else if ((bi % 2 != 0) && (bowls[bi] + bowls[bi-1] == 10))
            {
                tallyEnd++;
                bonusQueue.Enqueue(Bonus.Spare);
            }

            if (tallyEnd == bi)
            {
                for (int ti = tallyStart; ti <= tallyEnd; ti++)
                {
                    tally += bowls[ti];
                }

                frameList.Add(tally);

                Bonus currentBonus = Bonus.None;
                if (bonusQueue.Count > 0)
                {
                    currentBonus = bonusQueue.Dequeue();
                }

                if (currentBonus == Bonus.Spare)
                {
                    tallyStart = bi;
                }
                else if (currentBonus == Bonus.Strike)
                {
                    tallyStart = --bi;
                }
                else
                {
                    tallyStart = (bi + 1);
                }
                
                tallyEnd = (tallyStart + 1);
                tally = 0;

            }
        }

        return frameList;
    }
}