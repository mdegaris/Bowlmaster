using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private const string SkipSymbol = " ";
    private const string ZeroSymbol = "-";
    private const string StrikeSymbol = "X";
    private const string SpareSymbol = "/";

    private List<Frame> frames;
    private List<Bowl> bowls;


    public static string FormatRolls(List<int> rolls)
    {
        string scores = "";
        for (int bi = 0; bi < rolls.Count; bi++)
        {
            int boxNo = scores.Length + 1;    // Current score box number.
            string previousBox = (scores.Length > 0 ? scores.Substring(scores.Length - 1) : null);   // Keep track of previous box.
            bool possibleSpareBox = (boxNo % 2 == 0 || boxNo == 21);    // Indicates score box can be a spare symbol.

            // Handle zero/gutter rolls.
            if (rolls[bi] == 0)
            {
                scores += ZeroSymbol;
            }
            // Insert spare symbol.
            // rolls[bi] + rolls[bi - 1] == 10 : This roll and previous add to 10.
            // previousBox != SpareSymbol : Previous box wasn't already a spare (applies to box 21).
            else if (possibleSpareBox && (rolls[bi] + rolls[bi - 1] == 10) && (previousBox != SpareSymbol))
            {
                scores += SpareSymbol;
            }
            // Insert strike symbol.
            else if (rolls[bi] == 10)
            {
                if (boxNo < 19) { scores += SkipSymbol; }
                scores += StrikeSymbol;
            }
            else
            {
                scores += rolls[bi].ToString();
            }
        }

        return scores;
    }

    //// Ben's solution - not right when all spares!
    //public static string FormatRolls(List<int> rolls)
    //{
    //    string scores = "";

    //    for (int i = 0; i < rolls.Count; i++)
    //    {
    //        int box = scores.Length + 1;

    //        if (rolls[i] == 0)
    //        {
    //            scores += ZeroSymbol;
    //        }
    //        else if ((box % 2 == 0 || box == 21) && rolls[i - 1] + rolls[i] == 10)
    //        {
    //            scores += SpareSymbol;
    //        }
    //        else if (box >= 19 && rolls[i] == 10)
    //        {
    //            scores += StrikeSymbol;
    //        }
    //        else if (rolls[i] == 10)
    //        {
    //            scores += SkipSymbol + StrikeSymbol;
    //        }
    //        else
    //        {
    //            scores += rolls[i].ToString();
    //        }
    //    }

    //    return scores;
    //}


    public void FillFrames(List<int> frames)
    {
        for (int fi = 0; fi < frames.Count; fi++)
        {
            this.frames[fi].SetScore(frames[fi]);
        }
    }

    public void FillRolls(List<int> rolls)
    {
        string scores = ScoreDisplay.FormatRolls(rolls);
        for (int bi = 0; bi < scores.Length; bi++)
        {
            this.bowls[bi].SetPinsFallen(scores.Substring(bi, 1));
        }
    }

    public void Reset()
    {
        foreach (Frame f in this.frames)
        {
            f.Reset();
        }

        foreach (Bowl b in this.bowls)
        {
            b.Reset();
        }
    }

    private void Start()
    {
        this.frames = this.gameObject.GetComponentsInChildren<Frame>().ToList<Frame>();
        this.frames = this.frames.OrderBy(f => f.frameNumber).ToList<Frame>();

        this.bowls = new List<Bowl>();
        foreach (Frame f in this.frames)
        {
            this.bowls.AddRange(f.GetBowls());
        }
    }
}